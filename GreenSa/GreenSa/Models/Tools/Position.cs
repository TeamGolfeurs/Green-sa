using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools
{
    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Position(float x,float y )
        {
            X = x;
            Y = y;
        }
    }
}
