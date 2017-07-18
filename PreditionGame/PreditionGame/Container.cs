using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreditionGame
{
    /// <summary>
    /// Container class that will hold the ball.
    /// </summary>
    public class Container
    {
        public int ID { get; set; }       
        public bool IsBallIn { get; set; }       
        public Gate ParentGate { get; set; }
        public bool IsLeftContainer { get; set; }
    }
}
