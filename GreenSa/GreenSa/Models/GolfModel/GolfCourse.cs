using GreenSa.Models.Tools;
using System.Collections;
using System.Collections.Generic;
using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GreenSa.Models.GolfModel
{
    public class GolfCourse
    {
        public string Name { get; set; }
        public string NameCourse { get; set; }

        // Id
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        // Holes
        [OneToMany]
        public List<MyPosition> Holes { get; set; }


        public GolfCourse()
        {

        }
        public GolfCourse(string name,string nameCourse,List<MyPosition> holes)
        {
            this.Name = name;
            this.Holes = holes;
            this.NameCourse = nameCourse;

        }

        internal List<MyPosition>.Enumerator GetHoleEnumerator()
        {
            return Holes.GetEnumerator();
        }

      
       
    }
}