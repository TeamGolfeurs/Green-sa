using GreenSa.Models.Tools;
using GreenSa.Models.Tools.GPS_Maps;
using GreenSa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        public static IEnumerable<Tuple<Club, double>> getAverageDistanceForClubs(Filter<Club>.Filtre filtre)
        {
            IEnumerable<Tuple<Club, double>> res=new List<Tuple<Club, double>>();
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Dictionary<Club, double> sommesEachClubs = new Dictionary<Club, double>();
            Dictionary<Club, int> nombreDeShotParClub = new Dictionary<Club, int>();

            try
            {
                List<Shot> shots = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection));
                foreach (Shot s in shots)
                {
                    if (!sommesEachClubs.ContainsKey(s.Club))
                    {
                        sommesEachClubs.Add(s.Club, 0);
                        nombreDeShotParClub.Add(s.Club, 0);
                    }
                    sommesEachClubs[s.Club]+= (CustomMap.DistanceTo(s.InitPlace.X, s.InitPlace.Y, s.RealShot.X, s.RealShot.Y, "M"));
                    nombreDeShotParClub[s.Club] += 1;
                }

                 res = sommesEachClubs.Select(k => new Tuple<Club, double>(k.Key, k.Value / nombreDeShotParClub[k.Key]));//petit map pour calculer la moyenne
            }
            catch (Exception ex)
            {
               // "Table not exist";

            }
            return res;
        }
        //Tuple<Terrain,AverageScore,BestScore,WorstScore,nbFoisJouée
        //ordered by nbFoisJouée
        public static List<Tuple<GolfCourse, Tuple<Club, int, int, int>>> getScoreForGolfCourses(Filter<GolfCourse>.Filtre filtre)
        {
            List<Tuple<GolfCourse, Tuple<Club, int, int, int>>> l = new List<Tuple<GolfCourse, Tuple<Club, int, int, int>>>();
           // l.Add(new Tuple<GolfCourse, int, int, int, int> (new GolfCourse("StJacques9trousEN DUR","StJac",new List<MyPosition>()), 4,2,1,2));
            //l.Add(new Tuple<GolfCourse, int, int, int, int>(new GolfCourse("StJacques9trous EN DUR", "StJac", new List<MyPosition>()), 8, 5, 1, 2));
            return l;
        }

        //get the list
        public static List<Tuple<DateTime, int>> getListScoresWithDates(GolfCourse golfCourse)
        {
            List<Tuple<DateTime, int>>  l = new List<Tuple<DateTime, int>>();
            l.Add(new Tuple<DateTime, int>(new DateTime(), 4));
            l.Add(new Tuple<DateTime, int>(new DateTime(7), 8));
            return l;
        }

        public static void saveForStats(List<Shot> shots,Hole hole)
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Club>();
            connection.CreateTable<MyPosition>();
            connection.CreateTable<Shot>();

            SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection, shots, true);
            connection.CreateTable<ScoreHole>();
            List<ScoreHole> li = new List<ScoreHole>();
            ScoreHole h = new ScoreHole(hole, hole.Par-shots.Count+1);//plus 1 car le dernier coup n'est pas dans la liste
            SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, h, true);
        }

       
    }
}
