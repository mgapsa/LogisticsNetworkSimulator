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

        public DateTime NextOrderTime { get; set; }
        public Double NextOrderAmount { get; set; }

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
            this.Lambda = buyer.Lambda;
            this.MeanOptionA = buyer.MeanOptionA;
            this.DeviationOptionA = buyer.DeviationOptionA;


            this.OptionB = buyer.OptionB;
            this.Minutes = buyer.Minutes;
            this.MeanOptionB = buyer.MeanOptionB;
            this.DeviationOptionB = buyer.DeviationOptionB;
        }

        public bool MakeOrder(DateTime currentTime)
        {
            return (NextOrderTime <= currentTime);
        }

        public void SetNextOrderIfNeeded(DateTime currentTime)
        {
            if(NextOrderTime == new DateTime() || NextOrderTime <= currentTime)
            {
                switch(OptionB)
                {
                    case EnumTypes.BuyerBOptions.Static:
                        NextOrderTime = currentTime.AddMinutes(this.Minutes);
                        SetNextOrderAmount();
                        break;
                    case EnumTypes.BuyerBOptions.Gauss:

                        break;
                }
            }
        }

        private void SetNextOrderAmount()
        {
            switch(OptionA)
            {
                case EnumTypes.BuyerAOptions.Static:
                    NextOrderAmount = this.Amount;
                    break;
                case EnumTypes.BuyerAOptions.Random:
                    break;
                case EnumTypes.BuyerAOptions.Poisson:
                    break;
                case EnumTypes.BuyerAOptions.Gauss:
                    break;
            }
        }
    }
}
