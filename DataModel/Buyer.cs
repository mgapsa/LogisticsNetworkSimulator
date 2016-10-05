using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    //sprawdzic jakie dane były w starym
    public class Buyer
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
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
