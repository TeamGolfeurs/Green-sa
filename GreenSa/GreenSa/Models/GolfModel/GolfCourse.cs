using GreenSa.Models.Tools;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GreenSa.Models.GolfModel
{
    public class GolfCourse
    {
        public string Name { get; set; }
        public string NameCourse { get; set; }

        // Id
        private int id;
        public int Id { get => id; set => id = value; }

        // Holes
        private List<MyPosition> holes;

        public List<MyPosition> Holes { get => holes; set => holes = value; }

    

        public GolfCourse(string name,string nameCourse,List<MyPosition> holes)
        {
            this.Name = name;
            this.holes = holes;
            this.NameCourse = nameCourse;

        }

        internal List<MyPosition>.Enumerator GetHoleEnumerator()
        {
            return holes.GetEnumerator();
        }

      
       
    }
}