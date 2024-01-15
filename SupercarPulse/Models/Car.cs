using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SupercarPulse.Models
{
    public class Car
    {
        public int Id { get; set; }
        public byte[] CarLogo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double Zero_To_Sixty { get; set; }
        public string Engine_Type { get; set; }
        public int Horsepower { get; set; }
        public double Engine_Liter { get; set; }
        public byte[] ImageSrc { get; set; }
        public byte[] EngineImg { get; set; }
        public int Price { get; set; }
    }
}
