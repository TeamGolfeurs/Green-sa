using GreenSa.Models.Tools;
using GreenSa.Persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenSa.Models.GolfModel
{
    /**
     * Contient toutes les infos nécessaire à la partie
     * */
    public class Partie
    {

        private GolfCourse golfCourse;
        private Club currentClub;

        public Club CurrentClub { get
            {
                return currentClub;
            }
            set {
                currentClub = value;
            }
        }
        public List<Shot> Shots { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Hole> Holes;
        private List<Hole>.Enumerator itHole;
        public ScorePartie ScoreOfThisPartie { get; set; }
        public int holeFinishedCount;

        public GolfCourse GolfCourse {
            get
            {
                return golfCourse;
            }
            set {
                golfCourse = value;
                Holes = value.Holes;
                itHole = value.GetHoleEnumerator();
            }
        }

        public Partie()
        {
            Shots = new List<Shot>();
            CurrentClub = GolfXMLReader.getClubFromName("Fer3");
            ScoreOfThisPartie = new ScorePartie();
            this.holeFinishedCount = -1;
        }

        public void setCurrentClub(Club club)
        {
            CurrentClub = club;
        }

        public int getPenalityCount()
        {
            int penalities = 0;
            foreach (Shot shot in this.Shots)
            {
                penalities += shot.PenalityCount;
            }
            return penalities;
        }

        public int getCurrentScore()
        {
            int penalities = getPenalityCount();
            return penalities + this.Shots.Count - this.getNextHole().Par;
        }

        /// <summary>
        /// Retourne le (current) trou si il existe sinon retourne null.
        /// </summary>
        /// <returns>La position du trou.</returns>
        public Hole getNextHole()
        {
            return itHole.Current;
        }

        //index,nbTotal
        public Tuple<int, int> getIndexHole()
        {
            return new Tuple<int, int>(golfCourse.Holes.IndexOf(itHole.Current) + 1, golfCourse.Holes.Count);
        }

        public Shot addPositionForCurrentHole(MyPosition start, MyPosition oldTarget, MyPosition userPosition)
        {
            Shot s = new Shot(CurrentClub, start, oldTarget, userPosition, DateTime.Now);
            Shots.Add(s);
            return s;
        }

        public void holeFinished(bool saveForStatistics)
        {
            if (saveForStatistics)
            {
                foreach (Shot s in Shots)
                {
                    System.Diagnostics.Debug.WriteLine(s.ToString());
                }
                try
                {
                    ScoreHole sh = StatistiquesGolf.saveForStats(this, itHole.Current);
                    ScoreOfThisPartie.add(sh);
                    Shots.Clear();
                    System.Diagnostics.Debug.WriteLine("okok");
                } catch (SQLiteException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            } else
            {
                Shots.Clear();
            }
        }

        public async Task gameFinished(bool saveForStatistics)
        {
            if (saveForStatistics)
            {
                ScoreOfThisPartie.DateFin = DateTime.Now;
                await StatistiquesGolf.saveGameForStats(ScoreOfThisPartie);
            }
        }


    /// <summary>
    /// Vérifie l'existence d'un prochain trou
    /// </summary>
    /// <returns></returns>
    public bool hasNextHole()
        {
            return this.holeFinishedCount < this.Holes.Count-1;        
        }

        public bool nextHole()
        {
            this.holeFinishedCount++;
            return itHole.MoveNext();
        }


        internal void updateUICircle()
        {
            MessagingCenter.Send<Partie>(this, "updateTheCircle");
        }
    }


}
