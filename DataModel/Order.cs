using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Order
    {
        public Order(double amount, DateTime arrivalTime)
        {
            this.Amount = amount;
            this.ArrivalTime = arrivalTime;
        }
        public double Amount { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
