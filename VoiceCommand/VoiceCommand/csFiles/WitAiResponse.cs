using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServ
{
    // generated via Json2csharp.com
    public class Accessory
    {
        public double confidence { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public bool suggested { get; set; }
    }

    public class Location
    {
        public double confidence { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public bool suggested { get; set; }
    }

    public class OnOff
    {
        public double confidence { get; set; }
        public string value { get; set; }
    }

    public class Entities
    {
        public List<Accessory> device { get; set; }
        public List<Location> location { get; set; }
        public List<OnOff> on_off { get; set; }
    }

    public class RootObject
    {
        public string msg_id { get; set; }
        public string _text { get; set; }
        public Entities entities { get; set; }
    }

    public struct ResponseContent
    {
        public string Device { get; set; }
        public string Location { get; set; }
        public string Action { get; set; }
    }
}
