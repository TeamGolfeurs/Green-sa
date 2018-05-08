using GreenSa.Models.Tools;
using GreenSa.Models.Tools.GPS_Maps;
using GreenSa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static GreenSa.Models.GolfModel.Hole;

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
        public static IEnumerable<Tuple<Club, double>> getAverageDistanceForClubs(Func<Club,bool> filtre)
        {
            IEnumerable<Tuple<Club, double>> res=new List<Tuple<Club, double>>();
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Dictionary<Club, double> sommesEachClubs = new Dictionary<Club, double>();
            Dictionary<Club, int> nombreDeShotParClub = new Dictionary<Club, int>();

            try
            {
                IEnumerable<Shot> shots = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection));
                shots = shots.Where(s => filtre(s.Club) && !s.Club.Equals(Club.PUTTER)); 
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
        // Terrain, List Tuple<Hole,AverageScore,BestScore,WorstScore>, nbFoisJouée 
        //ordered by nbFoisJouée
        public static Dictionary<GolfCourse, Tuple<List<Tuple<Hole, float, int, int>>, int>> getScoreForGolfCourses(Func<GolfCourse, bool> filtre)
        {
            Dictionary<GolfCourse, Tuple<List<Tuple<Hole, float, int, int>>, int>> l = new Dictionary<GolfCourse, Tuple<List<Tuple<Hole, float, int, int>>, int>>();
            // l.Add(new Tuple<GolfCourse, int, int, int, int> (new GolfCourse("StJacques9trousEN DUR","StJac",new List<MyPosition>()), 4,2,1,2));
            //l.Add(new Tuple<GolfCourse, int, int, int, int>(new GolfCourse("StJacques9trous EN DUR", "StJac", new List<MyPosition>()), 8, 5, 1, 2));

            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<GolfCourse>();
            connection.CreateTable<ScoreHole>();
            IEnumerable<GolfCourse> gfcs = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection, recursive: true);
            gfcs=gfcs.Where((gf) => filtre(gf));
            foreach (GolfCourse gc in gfcs)
            {
                List<Tuple<Hole, float, int, int>> lsHole = new List<Tuple<Hole, float, int, int>>();

                int nbFoisJoue = 0;
                foreach (Hole h in gc.Holes)
                {
                    float moy = 0;
                    int min = 99;
                    int max = -99;
                    IEnumerable<ScoreHole> scores = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection ,recursive: true);
                    scores = scores.Where((ScoreHole gf) => gf.Hole.Equals(h));
                    nbFoisJoue = scores.Count();
                    foreach (ScoreHole sh in scores)
                    {
                        moy += sh.Score;
                        min = sh.Score < min ? sh.Score : min;
                        max = sh.Score > max ? sh.Score : max;
                    }
                    moy = moy / nbFoisJoue;
                    lsHole.Add(new Tuple<Hole, float, int, int>(h, moy, min, max));
                }
                l.Add(gc, new Tuple<List<Tuple<Hole, float, int, int>>, int>( lsHole,nbFoisJoue));
            }
            return l;
        }

        //get the list
        public static Dictionary<ScorePossible,float> getProportionScore()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScoreHole>();
            connection.CreateTable<Hole>();

            Dictionary<ScorePossible, float> res = new Dictionary<ScorePossible, float>();
            List<ScoreHole> all = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection,recursive:true);
            int nbTotal = all.Count;
            res.Add(ScorePossible.ALBATROS, all.Where<ScoreHole>(sh => (sh.Score <= (int)ScorePossible.ALBATROS)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.EAGLE, all.Where<ScoreHole>(sh => (sh.Score==(int)ScorePossible.EAGLE)).Count()/ (float)nbTotal *100 );
            res.Add(ScorePossible.BIRDIE, all.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.BIRDIE)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.PAR, all.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.PAR)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.BOGEY, all.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.BOGEY)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.DOUBLE_BOUGEY, all.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.DOUBLE_BOUGEY)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.MORE, all.Where<ScoreHole>(sh => (sh.Score >= (int)ScorePossible.MORE)).Count() / (float)nbTotal * 100);

            return res;
        }
        //Dictionary<Par, Moyenne> 
        public static Dictionary<int,float> getScoreForPar()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScoreHole>();
            connection.CreateTable<Hole>();

            Dictionary<int, float> resTmp = new Dictionary<int, float>();
            Dictionary<int, int> counter = new Dictionary<int, int>();

            List<ScoreHole> all = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection,recursive:true);
            int nbTotal = all.Count;
            foreach( ScoreHole h in all)
            {
                int par = h.Hole.Par;
                if (!resTmp.ContainsKey(par))
                {
                    resTmp.Add(par, 0);
                    counter.Add(par, 0);
                }
                resTmp[par]+= h.Score;
                counter[par] +=1;
            }
            Dictionary<int, float> res = new Dictionary<int, float>();

            foreach (KeyValuePair<int, float> entry in res)
            {
                res.Add(entry.Key, entry.Value / counter[entry.Key]);
            }

            return res;
        }
        //Hit
        public static float getProportionHit()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScoreHole>();
            connection.CreateTable<Hole>();

            List<ScoreHole> all = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection, recursive: true);
            int nbTotal = all.Count;
          
            return all.Where(sh=> sh.Hit).Count()/(float)nbTotal*100 ;
        }

        public static void saveForStats(List<Shot> shots,Hole hole)
        {
            if (shots.Count == 0)
                throw new Exception("0 shots dans la liste des shots.");
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Club>();
            connection.CreateTable<MyPosition>();
            connection.CreateTable<Shot>();

            SQLiteNetExtensions.Extensions.WriteOperations.InsertOrReplaceAllWithChildren(connection, shots, true);
            connection.CreateTable<ScoreHole>();
            List<ScoreHole> li = new List<ScoreHole>();
            
            
            ScoreHole h = new ScoreHole(hole, shots.Count-hole.Par, isHit(shots, hole.Par),DateTime.Now);
            SQLiteNetExtensions.Extensions.WriteOperations.InsertOrReplaceWithChildren(connection, h, false);
        }

        private static bool isHit(List<Shot> shots, int par)
        {
            Club used = shots[0].Club;
            int nbCoupAvantPasserAuGreen = 0;
            while ((!used.Equals(Club.PUTTER)) && nbCoupAvantPasserAuGreen < shots.Count-1)//2 <4-1, 3 =4-1
            {
                nbCoupAvantPasserAuGreen++;
                used = shots[nbCoupAvantPasserAuGreen].Club;
            }

            int nbCoutNecessairePourHit = (int)3.0 / 5 * par;

            return nbCoupAvantPasserAuGreen <= nbCoutNecessairePourHit;

        }


    }
}
