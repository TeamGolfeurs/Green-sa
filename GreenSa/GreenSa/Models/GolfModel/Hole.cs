using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Hole
    {
        public int Id { get; set; }
        public MyPosition position { get; set; }
        public int Par { get; set; }

        public override string ToString()
        {
            return Id+" "+position+" Par "+Par ;
        }
    }
}
