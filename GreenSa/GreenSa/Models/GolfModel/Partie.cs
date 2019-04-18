using GreenSa.Models.Tools;
using GreenSa.Persistence;
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
        private List<Hole>.Enumerator itHole;
        public ScorePartie ScoreOfThisPartie { get; set; }

        public GolfCourse GolfCourse {
            get
            {
                return golfCourse;
            }
            set {
                golfCourse = value;
                itHole = value.GetHoleEnumerator();

            }
        }


        public void setCurrentClub(Club club)
        {
            CurrentClub = club;
        }


        public Partie()
        {
            Shots = new List<Shot>();
            CurrentClub = GolfXMLReader.getClubFromName("Fer3");
            System.Diagnostics.Debug.WriteLine(CurrentClub.ToString());
            ScoreOfThisPartie = new ScorePartie();
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
                ScoreHole sh = StatistiquesGolf.saveForStats(Shots, itHole.Current);
                ScoreOfThisPartie.add(sh);
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
    /// Vérifie l'existence d'un prochain trou et se decale 
    /// </summary>
    /// <returns></returns>
    public bool hasNextHole()
        {

            return itHole.MoveNext();        
        }

        internal void updateUICircle()
        {
            MessagingCenter.Send<Partie>(this, "updateTheCircle");
        }
    }


}
