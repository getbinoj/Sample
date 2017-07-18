using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreditionGame
{
    /// <summary>
    /// The gate class that leads to a container
    /// </summary>
    public class Gate
    {
        public int GateId { get; set; }
        public int Level { get; set; }
        public bool IsGateOpenLeft { get; set; }
        public Gate ParentGate { get; set; }
        public bool IsLeftGate { get; set; }
    }
}

