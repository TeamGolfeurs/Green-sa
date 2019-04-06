using GreenSa.Models.Profiles;
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
        public static  IEnumerable<Tuple<Club, double>> getAverageDistanceForClubsAsync(Func<Club,bool> filtre)
        {
            IEnumerable<Tuple<Club, double>> res=new List<Tuple<Club, double>>();
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Dictionary<Club, double> sommesEachClubs = new Dictionary<Club, double>();
            Dictionary<Club, int> nombreDeShotParClub = new Dictionary<Club, int>();

            try
            {
                

                List<Club> clubs = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Club>(connection);
                System.Diagnostics.Debug.WriteLine("clubs.Count : " + clubs.Count);
                foreach (Club c in clubs)
                {
                    System.Diagnostics.Debug.WriteLine("clubs : " + clubs.ToString());
                    if (!c.Equals(Club.PUTTER))
                    {
                        if (!sommesEachClubs.ContainsKey(c))
                        {
                            sommesEachClubs.Add(c, 0);
                            nombreDeShotParClub.Add(c, 0);
                        }
                        sommesEachClubs[c] += (double)c.DistanceMoyenne;
                        nombreDeShotParClub[c] += 1;
                    }
                }

                IEnumerable<Shot> shots = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection);
                shots = shots.Where(s => filtre(s.Club) && !s.Club.Equals(Club.PUTTER));
                foreach (Shot s in shots)
                {
                    if (!sommesEachClubs.ContainsKey(s.Club))
                    {
                        sommesEachClubs.Add(s.Club, 0);
                        nombreDeShotParClub.Add(s.Club, 0);
                    }
                    sommesEachClubs[s.Club] += (CustomMap.DistanceTo(s.InitPlace.X, s.InitPlace.Y, s.RealShot.X, s.RealShot.Y, "M"));
                    nombreDeShotParClub[s.Club] += 1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error : " + ex.StackTrace);

            }
            res = sommesEachClubs.Select(k => new Tuple<Club, double>(k.Key, k.Value / nombreDeShotParClub[k.Key]));//petit map pour calculer la moyenne
            return res;
        }

        public static double getAveragePars()
        {
            double avPars = -1;
            double sum = 0;
            double nineHolesCount = 0;
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScorePartie>();
            connection.CreateTable<ScoreHole>();
            List<ScorePartie> scoresPartie = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScorePartie>(connection);
            foreach (ScorePartie sp in scoresPartie)
            {
                foreach (ScoreHole sh in sp.scoreHoles)
                {
                    sum += (sh.Score == 0) ? 1 : 0;
                }
                nineHolesCount += (sp.scoreHoles.Count == 9) ? 1 : 2;
            }
            if (scoresPartie.Count > 0)
            {
                avPars = sum / nineHolesCount;
            }
            return avPars;
        }

        public static double getAveragePutts(List<ScoreHole> scoresHoles)
        {
            double avPutts = -1.0;
            int sum = 0;
            foreach (ScoreHole sh in scoresHoles)
            {
                sum += sh.NombrePutt;
            }
            if (scoresHoles.Count > 0)
            {
                avPutts = (double)((double)sum / (double)scoresHoles.Count);
            }
            return avPutts;
        }

        public static Tuple<double, double> getMinMaxDistanceForClubs(Club club)
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            IEnumerable<Shot> shots = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection));
            shots = shots.Where(s => s.Club.Equals(club));
            double min = 99999;
            double max = 0;
            foreach(Shot s in shots)
            {
                if (s.Distance < min)
                    min = s.Distance;
                if (s.Distance >max)
                    max = s.Distance;
            }
            return new Tuple<double, double>(min, max);
        }

        /** Gets a tuple containing the name of the club with which the player did the higher distance and this distance
         */
        public static Tuple<string, int> getMaxDistClub()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Shot>();
            List<Shot> shots = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Shot>(connection);
            double maxDist = 0.0;
            string clubName = "";
            foreach (Shot shot in shots)
            {
                double dist = shot.Distance;
                if (dist > maxDist)
                {
                    maxDist = dist;
                    clubName = shot.Club.Name;
                }
            }
            return new Tuple<string, int>(clubName, (int)maxDist);
        }

        /** Gets all the ScorePartie from the database
         */
        public static List<ScorePartie> getScoreParties()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScorePartie>();
            List<ScorePartie> scores = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScorePartie>(connection, recursive: true);
            return scores;
        }

        /** Gets the all the ScoreHole from the database
         */
        public static List<ScoreHole> getScoreHoles()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<ScoreHole>();
            connection.CreateTable<Hole>();
            List<ScoreHole> scoresHoles = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection, recursive: true);
            return scoresHoles;
        }

        /** Gets the all the golf courses from the database
         */
        public static List<GolfCourse> getGolfCourses()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<GolfCourse>();
            List<GolfCourse> golfCourses = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection);
            return golfCourses;
        }


        public static int getWorstHole(GolfCourse golfCourse)
        {
            int holeNumber = 0;
            List<ScoreHole> scoreHoles = getScoreHoles(golfCourse);
            if (scoreHoles.Count > 0)
            {
                //Dictionnary<Hole, Tuple<sum, count>> to compute mean
                Dictionary<Hole, double> sumScorePerHole = new Dictionary<Hole, double>();
                Dictionary<Hole, double> countScorePerHole = new Dictionary<Hole, double>();
                foreach (ScoreHole sh in scoreHoles)
                {
                    if (!sumScorePerHole.ContainsKey(sh.Hole))
                    {
                        sumScorePerHole.Add(sh.Hole, 0.0);
                        countScorePerHole.Add(sh.Hole, 0.0);
                    }
                    sumScorePerHole[sh.Hole] += sh.Score;
                    countScorePerHole[sh.Hole] += 1.0;
                }
                double maxScore = 0.0;
                foreach (Hole hole in sumScorePerHole.Keys)
                {
                    double currentMean = sumScorePerHole[hole] / countScorePerHole[hole];
                    if (currentMean > maxScore)
                    {
                        maxScore = currentMean;
                        holeNumber = golfCourse.Holes.IndexOf(hole) + 1;
                    }
                }
            }
            return holeNumber;
        }


        // Terrain, List Tuple<Hole,AverageScore,BestScore,WorstScore>, nbFoisJouée 
        //ordered by nbFoisJouée
        public static Dictionary<GolfCourse, List<Tuple<Hole, float, int, int,float,int>>> getScoreForGolfCourses(Func<GolfCourse, bool> filtre)
        {
            Dictionary<GolfCourse, List<Tuple<Hole, float, int, int,float,int>>> l = new Dictionary<GolfCourse, List<Tuple<Hole, float, int, int,float,int>>>();
            // l.Add(new Tuple<GolfCourse, int, int, int, int> (new GolfCourse("StJacques9trousEN DUR","StJac",new List<MyPosition>()), 4,2,1,2));
            //l.Add(new Tuple<GolfCourse, int, int, int, int>(new GolfCourse("StJacques9trous EN DUR", "StJac", new List<MyPosition>()), 8, 5, 1, 2));

            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<GolfCourse>();
            connection.CreateTable<ScoreHole>();
            IEnumerable<GolfCourse> gfcs = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection, recursive: true);
            gfcs=gfcs.Where((gf) => filtre(gf));
            foreach (GolfCourse gc in gfcs)
            {
                List<Tuple<Hole, float, int, int,float,int>> lsHole = new List<Tuple<Hole, float, int, int,float,int>>();

                int nbFoisJoue = 0;
                IEnumerable<ScoreHole> scores = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<ScoreHole>(connection, recursive: true);

                foreach (Hole h in gc.Holes)
                {
                    float moy = 0;
                    float moyPutt = 0;
                    int min = 99;
                    int max = -99;
                    IEnumerable<ScoreHole> scoresTmp = scores.Where((ScoreHole gf) => gf.Hole.Equals(h));
                    nbFoisJoue = scoresTmp.Count();
                    foreach (ScoreHole sh in scoresTmp)
                    {
                        moy += sh.Score;
                        moyPutt += sh.NombrePutt;
                        min = sh.Score < min ? sh.Score : min;
                        max = sh.Score > max ? sh.Score : max;
                    }
                    moy = moy / nbFoisJoue;
                    moyPutt = moyPutt / nbFoisJoue;
                    lsHole.Add(new Tuple<Hole, float, int, int,float,int>(h, moy, min, max,moyPutt,nbFoisJoue));
                }

                l.Add(gc, lsHole);
            }
            return l;
        }

        public static List<ScorePartie> getScoreParties(GolfCourse golfCourse)
        {
            List<ScorePartie> allScoreParties = getScoreParties();
            List<ScorePartie> allNeededScoreParties = allScoreParties.Where(sh => sh.scoreHoles[0].Hole.IdGolfC.Equals(golfCourse.Name)).ToList();
            return allNeededScoreParties;
        }

        public static List<ScoreHole> getScoreHoles(GolfCourse golfCourse)
        {
            List<ScoreHole> allScoreHoles = getScoreHoles();
            List<ScoreHole> allNeededScoreHoles = allScoreHoles.Where(sh => sh.Hole.IdGolfC.Equals(golfCourse.Name)).ToList();
            return allNeededScoreHoles;
        }

        //get the list
        public static Dictionary<ScorePossible,float> getProportionScore(GolfCourse golfCourse)
        {
            Dictionary<ScorePossible, float> res = new Dictionary<ScorePossible, float>();
            List<ScoreHole> allNeededScoreHoles = getScoreHoles(golfCourse);
            int nbTotal = allNeededScoreHoles.Count;

            res.Add(ScorePossible.ALBATROS, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score <= (int)ScorePossible.ALBATROS)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.EAGLE, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score==(int)ScorePossible.EAGLE)).Count()/ (float)nbTotal *100 );
            res.Add(ScorePossible.BIRDIE, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.BIRDIE)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.PAR, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.PAR)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.BOGEY, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.BOGEY)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.DOUBLE_BOUGEY, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score == (int)ScorePossible.DOUBLE_BOUGEY)).Count() / (float)nbTotal * 100);
            res.Add(ScorePossible.MORE, allNeededScoreHoles.Where<ScoreHole>(sh => (sh.Score >= (int)ScorePossible.MORE)).Count() / (float)nbTotal * 100);

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

            foreach (KeyValuePair<int, float> entry in resTmp)
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

        public static ScoreHole saveForStats(List<Shot> shots,Hole hole)
        {
            if (shots.Count == 0)
                throw new Exception("0 shots dans la liste des shots.");
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Club>();
            connection.CreateTable<MyPosition>();
            connection.CreateTable<Shot>();

            SQLiteNetExtensions.Extensions.WriteOperations.InsertOrReplaceAllWithChildren(connection, shots, true);
            //connection.InsertAll(shots);
            connection.CreateTable<ScoreHole>();

            ScoreHole h = new ScoreHole(hole, shots.Count-hole.Par, isHit(shots, hole.Par), nbCoupPutt(shots),DateTime.Now);
            SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, h, false);
            string sql = @"select last_insert_rowid()";
            h.Id = connection.ExecuteScalar<int>(sql);
            //connection.Insert(h);
            return h;
        }

        public async static Task saveGameForStats(ScorePartie scoreOfThisPartie)
        {
            SQLite.SQLiteAsyncConnection connection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();
            await connection.CreateTableAsync<ScoreHole>();
            await connection.CreateTableAsync<ScorePartie>();

            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.InsertWithChildrenAsync(connection, scoreOfThisPartie,false);
        }


        //SCorePartie
        //
        public async static Task<IEnumerable<ScorePartie>> getListOfScorePartie()
        {
            SQLite.SQLiteAsyncConnection connection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();
            await connection.CreateTableAsync<ScoreHole>();
            await connection.CreateTableAsync<ScorePartie>();

            List<ScorePartie> all = (await SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<ScorePartie>(connection, recursive: true));

            return all.OrderByDescending(sp =>sp.DateDebut);
        }

        public static double getPlayerIndex()
        {
            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Profil>();
            List<Profil> profil = SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Profil>(connection);
            double index = 53.5;
            if (profil.Count > 0)
            {
                index = profil[0].Index;
            }
            return index;
        }


        /** Computes the number of shots before the ball gets onto the green
         * shots : the list of the shots of a single hole
         * return : the number of shots before the ball gets onto the green
         */
        private static int numberShotsBeforeGreen(List<Shot> shots)
        {
            int shotsBeforeGreen = 0;
            Club used = shots[0].Club;
            while ((!used.Equals(Club.PUTTER)) && shotsBeforeGreen < shots.Count - 1)//2 <4-1, 3 =4-1
            {
                shotsBeforeGreen++;
                used = shots[shotsBeforeGreen].Club;
            }
            return shotsBeforeGreen;
        }

        /** Computes the number of putts
         * shots : the list of the shots of a single hole
         * return : the number of putts
         */
        private static int nbCoupPutt(List<Shot> shots)
        {
            int shotsBeforeGreen = numberShotsBeforeGreen(shots);
            return shots.Count - shotsBeforeGreen;
        }

        /** Checks if the green was reached in regulation
         * shots : the list of the shots of a single hole
         * return : true if the number of shots before the ball gets onto the green is lower or equals to 2 under the par, false otherewise
         */
        private static bool isHit(List<Shot> shots, int par)
        {
            int shotsBeforeGreen = numberShotsBeforeGreen(shots);
            //int nbCoutNecessairePourHit = (int) (3.0 / 5 * par);
            int nbCoutNecessairePourHit = par - 2;

            return shotsBeforeGreen <= nbCoutNecessairePourHit;
        }


    }
}
