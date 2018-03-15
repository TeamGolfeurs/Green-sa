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
        private List<Position> holes;
        public List<Position> Holes { get => holes; set => holes = value; }

        public GolfCourse(string name)
        {
            Name = name;
            holes = new List<Position>();
        }

        //methode pour remplir à partir d'un XML ?
    }
}