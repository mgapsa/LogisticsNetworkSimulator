using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Shop : Actor
    {
        //ile chce mieć, taka wartosc bazowa
        public double BasicAmount { get; set; }
        //minimalna dopuszczalna
        public double MinAmount { get; set; }
        public double InitialAmount { get; set; }
        public double StorageCost { get; set; }
    }
}
