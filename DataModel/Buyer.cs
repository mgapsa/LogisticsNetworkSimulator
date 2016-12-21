using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace DataModel
{
    //sprawdzic jakie dane były w starym
    public class Buyer : Actor
    {
        //zmienic nazwy tutaj pol -> zmienic nazwy w mapperze/dapperze
        //zmienic pola w bazie
        //Option A for time
        //Option b for amount
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
                        break;
                    case EnumTypes.BuyerBOptions.Gauss:
                        MathNet.Numerics.Distributions.Normal normal = new MathNet.Numerics.Distributions.Normal(this.MeanOptionB, this.DeviationOptionB);
                        NextOrderTime = currentTime.AddMinutes(normal.Sample());
                        break;
                }

                SetNextOrderAmount();
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
                    Random rnd = new Random();
                    int rand = rnd.Next(Convert.ToInt32(this.MinAmount), Convert.ToInt32(this.MaxAmount) + 1);
                    NextOrderAmount = rand;
                    break;
                case EnumTypes.BuyerAOptions.Poisson:
                    MathNet.Numerics.Distributions.Poisson poisson = new MathNet.Numerics.Distributions.Poisson(this.Lambda);
                    NextOrderAmount = poisson.Sample();
                    break;
                case EnumTypes.BuyerAOptions.Gauss:
                    MathNet.Numerics.Distributions.Normal normal = new MathNet.Numerics.Distributions.Normal(this.MeanOptionA, this.DeviationOptionA);
                    NextOrderAmount = normal.Sample();
                    break;
            }
        }
    }
}
