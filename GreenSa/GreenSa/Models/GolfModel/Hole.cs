using GreenSa.Models.Tools;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Hole
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(GolfCourse))]
        public string IdGolfC { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public GolfCourse GolfCourse { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public String IdPos { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public MyPosition Position { get; set; }

        public int Par { get; set; }

        public Hole()
        {

        }


        public Hole(MyPosition p, int par)
        {
            this.Position = p;
            this.Par = par;
        }
        public Hole(MyPosition p,int par,GolfCourse golfCourse)
        {
            this.Position = p;
            this.Par = par;
            GolfCourse = golfCourse;
        }

        public override string ToString()
        {
            return Id+" "+Position+" Par "+Par ;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Hole))
                return false;
            Hole h = (Hole)obj;
            return h.Position.Equals(Position) && h.Par==Par;
        }
    }
}
