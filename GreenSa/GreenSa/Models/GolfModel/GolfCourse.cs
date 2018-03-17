using GreenSa.Models.Tools;
using System.Collections;
using System.Collections.Generic;

namespace GreenSa.Models.GolfModel
{
    public class GolfCourse
    {
        public string Name;

        // Id
        private int id;
        public int Id { get => id; set => id = value; }

        // Holes
        private List<MyPosition> holes;
        public List<MyPosition> Holes { get => holes; set => holes = value; }

        public GolfCourse(string name)
        {
            Name = name;
            holes = new List<MyPosition>();
        }

        //methode pour remplir à partir d'un XML ?
    }
}