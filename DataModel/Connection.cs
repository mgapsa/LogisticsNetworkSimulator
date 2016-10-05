using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Connection
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ActorAId { get; set; }
        public int ActorBId { get; set; }
        public int ActorA { get; set; }
        public int ActorB { get; set; }
        public double OrderCost { get; set; }
        public double Usage { get; set; }
        public double MinDelay { get; set; }
        public double MaxDelay { get; set; }
    }
}
