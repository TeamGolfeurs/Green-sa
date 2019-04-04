using GreenSa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenSa.Models.GolfModel
{
    class TestClassFactory
    {

        public static ScorePartie CreateScorePartie()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<GolfCourse>();
            connection.CreateTable<ScorePartie>();
            connection.CreateTable<ScoreHole>();
            List<GolfCourse> golfCourses = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection);
            Random r = new Random();
            var holes = golfCourses[r.Next()%(golfCourses.Count)].Holes;
            ScorePartie sp = new ScorePartie();
            if (holes != null)
            {
                foreach (Hole hole in holes)
                {
                    int rand = (r.Next() % 4);
                    ScoreHole sh = new ScoreHole(hole, r.Next()%6, rand == 2, rand, DateTime.Now);
                    sp.add(sh);
                }
            }
            SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection, sp.scoreHoles, true);
            SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, sp, false);
            return sp;
        }

    }
}
