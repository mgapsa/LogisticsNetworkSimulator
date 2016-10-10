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
    }
}
