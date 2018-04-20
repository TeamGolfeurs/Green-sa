using GreenSa.Models.GolfModel;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools
{
    class GolfCourseHole
    {
        [ForeignKey(typeof(GolfCourse))]
        public String Id { get; set; }

        [ForeignKey(typeof(Hole))]
        public int IdPos { get; set; }

        public override string ToString()
        {
            return Id+" : "+IdPos;
        }
    }
}
