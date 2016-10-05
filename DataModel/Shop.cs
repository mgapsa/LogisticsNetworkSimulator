using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Shop
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public double BasicAmount { get; set; }
        public double MinAmount { get; set; }
        public double InitialAmount { get; set; }
        public double StorageCost { get; set; }
    }
}
