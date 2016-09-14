using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServ
{
    public class Alternative
    {
        public double confidence { get; set; }
        public string transcript { get; set; }
    }

    public class Result
    {
        public List<Alternative> alternatives { get; set; }
        public bool final { get; set; }
    }

    public class RootObjectIBM
    {
        public List<Result> results { get; set; }
        public int result_index { get; set; }
    }
}
