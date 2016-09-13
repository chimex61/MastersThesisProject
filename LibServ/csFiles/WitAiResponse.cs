using System.Collections.Generic;
using System.Drawing;

namespace LibServ
{
    // generated via Json2csharp.com

    public class Accessory
{
    public double confidence { get; set; }
    public string type { get; set; }
    public string value { get; set; }
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

    public class Value
    {
        public double confidence { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public bool suggested { get; set; }
    }

    public class Attribute
    {
        public double confidence { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Number
    {
        public int confidence { get; set; }
        public string type { get; set; }
        public int value { get; set; }
    }

    public class Color
    {
        public double confidence { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public bool suggested { get; set; }
    }

    public class Entities
    {
        public List<Accessory> device { get; set; }
        public List<Location> location { get; set; }
        public List<OnOff> on_off { get; set; }
        public List<Attribute> attribute { get; set; }
        public List<Number> number { get; set; }
        public List<Color> color { get; set; }
    }

    public class RootObject
    {
        public string msg_id { get; set; }
        public string _text { get; set; }
        public Entities entities { get; set; }
    }

    public struct ResponseContent
    {
        public string Device { get; set; } // light, socket...
        public string Location { get; set; } // kitchen, living room...
        public string Action { get; set; } // on, off
        public string Color { get; set; } // red, yelow...
        public string Attribute { get; set; } // brightness, color
        public int Number { get; set; } // value of brightness
    }
}
