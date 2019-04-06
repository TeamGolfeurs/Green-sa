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
        private static int createdScorePartieCount = 0;

        public static ScorePartie CreateScorePartie()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<GolfCourse>();
            connection.CreateTable<ScorePartie>();
            connection.CreateTable<ScoreHole>();
            List<GolfCourse> golfCourses = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection);
            Random r = new Random();
            var holes = golfCourses[r.Next()%(golfCourses.Count)].Holes;
            //var holes = golfCourses[0].Holes;
            DateTime date = new DateTime(2019, DateTime.Now.Month, (TestClassFactory.createdScorePartieCount % 28) + 1);
            ScorePartie sp = new ScorePartie(date);
            int i = 0;
            if (holes != null)
            {
                foreach (Hole hole in holes)
                {
                    int randPutt = r.Next() % 4;
                    int randScore = r.Next() % 4;
                    if (i == 7)
                    {
                        randScore = 4;
                    }
                    System.Diagnostics.Debug.WriteLine("randPutt = " + randPutt+ "randScore = "+ randScore+"\n");
                    ScoreHole sh = new ScoreHole(hole, randScore, randPutt == 2, randPutt, DateTime.Now);
                    sp.add(sh);
                    i++;
                }
            }
            System.Diagnostics.Debug.WriteLine("\n");
            SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection, sp.scoreHoles, true);
            SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, sp, false);
            TestClassFactory.createdScorePartieCount++;
            return sp;
        }

    }
}
