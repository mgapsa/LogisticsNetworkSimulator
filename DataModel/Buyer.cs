using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    //sprawdzic jakie dane były w starym
    public class Buyer : Actor
    {
        //zmienic nazwy tutaj pol -> zmienic nazwy w mapperze/dapperze
        //zmienic pola w bazie
        public EnumTypes.BuyerAOptions OptionA { get; set; }
        public double Amount { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public double Lambda { get; set; }
        public double MeanOptionA { get; set; }
        public double DeviationOptionA { get; set; }
        public EnumTypes.BuyerBOptions OptionB { get; set; }
        public double Minutes { get; set; }
        public double MeanOptionB { get; set; }
        public double DeviationOptionB { get; set; }

        public Buyer()
        {
            this.OptionA = EnumTypes.BuyerAOptions.Static;
            this.Amount = 1;
            this.MinAmount = 1;
            this.MaxAmount = 1;
            this.Lambda = 1;
            this.MeanOptionA = 1;
            this.DeviationOptionA = 1;
            this.OptionB = EnumTypes.BuyerBOptions.Static;
            this.Minutes = 2;
            this.MeanOptionB = 1;
            this.DeviationOptionB = 1;
        }

        //copy consttructor
        public Buyer(Buyer buyer) : base(buyer)
        {
            this.OptionA = buyer.OptionA;
            this.Amount = buyer.Amount;
            this.MinAmount = buyer.MinAmount;
            this.MaxAmount = buyer.MaxAmount;
            this.OptionB = buyer.OptionB;
            this.Lambda = buyer.Lambda;
            this.MeanOptionA = buyer.MeanOptionA;
            this.DeviationOptionA = buyer.DeviationOptionA;
        }
    }
}
