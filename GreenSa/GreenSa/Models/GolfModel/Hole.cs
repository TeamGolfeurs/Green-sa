using GreenSa.Models.Tools;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Hole
    {
        [PrimaryKey]
        public int Id { get; set; }
        public MyPosition Position { get; set; }
        public int Par { get; set; }

        public Hole()
        {

        }

        public Hole(MyPosition p,int par)
        {
            this.Position = p;
            this.Par = par;
        }

        public override string ToString()
        {
            return Id+" "+Position+" Par "+Par ;
        }
    }
}
