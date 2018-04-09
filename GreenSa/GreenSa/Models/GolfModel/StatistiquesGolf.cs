using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    /*
     * Classe donnant les stats de golf
     * 
     * */
    public class StatistiquesGolf
    {

        /**
         * Get the average distance for all clubs
         * */
        public List<Tuple<Club, int>> getAverageDistanceForClubs(Filter<Club>.Filtre filtre)
        {
            List<Tuple<Club, int>> l = new List<Tuple<Club, int>>();
            l.Add(new Tuple<Club, int>(new Club("Fer1",TypeClub.FER), 4));
            l.Add(new Tuple<Club, int>(new Club("Fer2", TypeClub.FER), 8));
            return l;
        }
        //Tuple<Terrain,AverageScore,BestScore,WorstScore,nbFoisJouée
        //ordered by nbFoisJouée
        public List<Tuple<GolfCourse, int, int, int, int>> getScoreForGolfCourses(Filter<GolfCourse>.Filtre filtre)
        {
            List<Tuple<GolfCourse, int, int, int, int>> l = new List<Tuple<GolfCourse, int, int, int, int>>();
            l.Add(new Tuple<GolfCourse, int, int, int, int> (new GolfCourse("StJacques9trousEN DUR","StJac",new List<MyPosition>()), 4,2,1,2));
            l.Add(new Tuple<GolfCourse, int, int, int, int>(new GolfCourse("StJacques9trous EN DUR", "StJac", new List<MyPosition>()), 8, 5, 1, 2));
            return l;
        }

        //get the list
        public List<Tuple<DateTime, int>> getListScoresWithDates(GolfCourse golfCourse)
        {
            List<Tuple<DateTime, int>>  l = new List<Tuple<DateTime, int>>();
            l.Add(new Tuple<DateTime, int>(new DateTime(), 4));
            l.Add(new Tuple<DateTime, int>(new DateTime(7), 8));
            return l;
        }
    }
}
