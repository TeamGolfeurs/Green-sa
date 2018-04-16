using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools
{
    public class MyPosition
    {
        [PrimaryKey, AutoIncrement]
        public int IdPos { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public MyPosition() { }

        public MyPosition(Double x, Double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return IdPos+" : ("+X+","+Y+")";
        }
    }
}
