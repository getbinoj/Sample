using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreditionGame
{
    public class BallGame
    {
        private readonly List<Gate> _gates = new List<Gate>();
        private readonly List<Container> _containers = new List<Container>();
        private readonly Random _rand=new Random();
        int _level = 1;
        public int InitializeGame(int Level)
        {
            _level = Level;           
            CreateGate(Level);
            return _containers.Count;

        }
        public int Play()
        {
            int _containerNo = 1;
            Gate _parent = _gates.FirstOrDefault(g => g.ParentGate == null);
            //for(int _counter=1;_counter<_containers.Count;_counter++)
            int _Counter = 1;
            while (_containers.FindAll(c => c.IsBallIn == false).Count>1)
            {
                PutBall(_parent);
                _parent.IsGateOpenLeft = !_parent.IsGateOpenLeft;
                _Counter++;
            }
            _containerNo = _containers.FirstOrDefault(c => c.IsBallIn == false).ID;
            return _containerNo;
        }
        #region Creating The gates and containers private methods
        private void CreateGate(int Level)
        {
            //Create parent gate
            Gate _parent = new Gate() { GateId = 1, Level = 0 ,ParentGate=null};
            _parent.IsGateOpenLeft = _rand.Next(1, 10) <= 5 ? true : false;
            _gates.Add(_parent);
            //for (int _Counter=1; _Counter<=Level;_Counter++)
            //{
                int _gateId = 1;
                int _containerId = 1;
                CreateGateLevel(1, _parent, ref _gateId, ref _containerId);
           // }
        }
        private void CreateGateLevel(int Level,Gate Parent,ref int GateId , ref int ContainerId )
        {
            Gate _left=new Gate();
            Gate _right=new Gate();
            if (Level < _level)
            {
                //Create Left gate
                _left = GateCreation(Level+1, Parent, ref GateId, true);
                CreateGateLevel(Level + 1, _left, ref GateId,ref ContainerId);
                //Create right gate
                _right = GateCreation(Level+1, Parent, ref GateId, false);
                CreateGateLevel(Level + 1, _right, ref GateId,ref ContainerId);
            }
            if (Level+1 == _level)
            {
                CreateContainer( ref ContainerId, _left);
                CreateContainer(ref ContainerId, _right);

            }
        }

        private void CreateContainer(ref int ContainerId, Gate _gate)
        {
            //Create Containers
            Container _leftContainer = new Container() { ID = ContainerId, IsBallIn = false, ParentGate = _gate, IsLeftContainer = true };
            _containers.Add(_leftContainer);
            ContainerId++;
            Container _rightContainer = new Container() { ID = ContainerId, IsBallIn = false, ParentGate = _gate,IsLeftContainer=false  };
            _containers.Add(_rightContainer);
            ContainerId++;

        }

        private  Gate GateCreation(int Level, Gate Parent,ref int GateId,bool IsLeftGate)
        {
            Gate _gate = new Gate();
            GateId++;
            _gate.Level = Level;
            _gate.IsGateOpenLeft = _rand.Next(1, 10) <= 5 ? true : false;
            _gate.GateId = GateId;
            _gate.IsLeftGate = IsLeftGate;
            _gate.ParentGate = Parent;
            _gates.Add(_gate);
            return _gate;
        }

        #endregion

        #region Playing game private methods
        private void PutBall(Gate Parent)
        {
            Gate _openedGate = new Gate();
            if(Parent.IsGateOpenLeft)
                _openedGate = _gates.FirstOrDefault(g => g.ParentGate!=null && g.ParentGate.GateId == Parent.GateId && g.IsLeftGate);
            else
                _openedGate = _gates.FirstOrDefault(g => g.ParentGate != null && g.ParentGate.GateId == Parent.GateId && !g.IsLeftGate);
            //No more child Gate.. Need to check for leaf node
            if(_gates.FindAll(g => g.ParentGate != null && g.ParentGate.GateId==_openedGate.GateId).Count==0)
            {
                //If left gate is opened put the ball inside left container
                if(_openedGate.IsGateOpenLeft)
                {
                    Container _container = _containers.FirstOrDefault(c => c.ParentGate == _openedGate && c.IsLeftContainer);
                    if (!_container.IsBallIn)
                    {
                        _container.IsBallIn = true;
                        _openedGate.IsGateOpenLeft = !_openedGate.IsGateOpenLeft;
                        return;
                    }                  

                }
                // right gate is opened, put the ball inside right container
                else
                {
                    Container _container = _containers.FirstOrDefault(c => c.ParentGate == _openedGate && !c.IsLeftContainer);
                    if (!_container.IsBallIn)
                    {
                        _container.IsBallIn = true;
                        _openedGate.IsGateOpenLeft = !_openedGate.IsGateOpenLeft;
                        return;
                    }
                    
                }
                //Open the gate to Opposite side
               
            }
            else //iterate to the next level
            {
                PutBall(_openedGate);
                _openedGate.IsGateOpenLeft = !_openedGate.IsGateOpenLeft;
            }
        }
#endregion

    }
}
