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
        public string Name { get; set; }
        public string NameCourse { get; set; }

        // Id
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ManyToMany(typeof(GolfCourseMyPosition), CascadeOperations = CascadeOperation.CascadeInsert)]
        public List<MyPosition> Holes {
            get { return holes; }
            set{
                holes = value;
                Debug.WriteLine("SETTING HOLE " +value);
            }
        }

        // Holes
        private List<MyPosition> holes;


        public GolfCourse()
        {

        }
        public GolfCourse(string name,string nameCourse,List<MyPosition> holes)
        {

            this.Name = name;
            this.Holes = holes;
            this.NameCourse = nameCourse;

        }

        public List<MyPosition>.Enumerator GetHoleEnumerator()
        {
            return Holes.GetEnumerator();
        }

        public override string ToString()
        {
            String str= Id+" - "+Name+", "+ NameCourse + " { "+Holes.Count+"\n";
            foreach (MyPosition m in Holes)
                str += m.ToString() + " \n";
            str += "}";
            return str;
        }


    }
}