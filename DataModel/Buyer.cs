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
        public int OptionA { get; set; }
        public double ParamA { get; set; }
        public double ParamB { get; set; }
        public double ParamC { get; set; }
        public int OptionB { get; set; }
        public double ParamD { get; set; }
        public double ParamE { get; set; }
        public double ParamF { get; set; }

        public Buyer()
        {
            this.OptionA = 0;
            this.ParamA = 1;
            this.ParamB = 1;
            this.ParamC = 1;
            this.OptionB = 0;
            this.ParamD = 1;
            this.ParamE = 1;
            this.ParamF = 1;
        }

        //copy consttructor
        public Buyer(Buyer buyer, SimulationModel model) : base(buyer, model)
        {
            this.OptionA = buyer.OptionA;
            this.ParamA = buyer.ParamA;
            this.ParamB = buyer.ParamB;
            this.ParamC = buyer.ParamC;
            this.OptionB = buyer.OptionB;
            this.ParamD = buyer.ParamD;
            this.ParamE = buyer.ParamE;
            this.ParamF = buyer.ParamF;
        }
    }
}
