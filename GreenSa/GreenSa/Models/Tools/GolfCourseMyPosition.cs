using GreenSa.Models.GolfModel;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools
{
    class GolfCourseMyPosition
    {
        [ForeignKey(typeof(GolfCourse))]
        public int Id { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public int IdPos { get; set; }

        public override string ToString()
        {
            return Id+" : "+IdPos;
        }
    }
}
