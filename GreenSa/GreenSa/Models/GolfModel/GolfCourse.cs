using GreenSa.Models.Tools;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace GreenSa.Models.GolfModel
{
    public class GolfCourse
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string NameCourse { get; set; }
        

        [ManyToMany(typeof(GolfCourseHole), CascadeOperations = CascadeOperation.All)]
        public List<Hole> Holes { get;   set;       }
        


        public GolfCourse()
        {

        }
        public GolfCourse(string name,string nameCourse,List<Hole> holes)
        {

            this.Name = name;
            this.Holes = holes;
            this.NameCourse = nameCourse;

        }

        public List<Hole>.Enumerator GetHoleEnumerator()
        {
            return Holes.GetEnumerator();
        }

        public override string ToString()
        {
            String str= " - "+Name+", "+ NameCourse + " { "+Holes.Count+"\n";
            foreach (Hole m in Holes)
                str += m.ToString() + " \n";
            str += "}";
            return str;
        }


    }
}