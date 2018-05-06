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
        public Club currentClub { get; set; }
        public List<Shot> Shots { get; set; }
        public List<Club> Clubs { get ; set; }
        private List<Hole>.Enumerator itHole;

        public GolfCourse GolfCourse {
            get
            {
               return  golfCourse;
            }
            set {
                golfCourse = value;
                itHole = value.GetHoleEnumerator();

            }
        }

        public void setCurrentClub(Club club)
        {
            currentClub = club;
        }

      
        public Partie()
        {
            Shots = new List<Shot>();
           currentClub = new Club("Fer3",170);
        }
        /// <summary>
        /// Retourne le prochain trou si il existe sinon retourne null.
        /// </summary>
        /// <returns>La position du trou.</returns>
        public Hole getNextHole()
        {
            return itHole.Current;
        }

        public void addPositionForCurrentHole(MyPosition start,MyPosition oldTarget, MyPosition userPosition)
        {
            Shots.Add(new Shot(currentClub,start, oldTarget, userPosition,DateTime.Now));
        }

        public void holeFinished(bool saveForStatistics)
        {
            if (saveForStatistics)
            {
                StatistiquesGolf.saveForStats(Shots,itHole.Current);
                Shots.Clear();
                
            }

        }

        /// <summary>
        /// Vérifie l'existence d'un prochain trou.
        /// </summary>
        /// <returns></returns>
        public bool hasNextHole()
        {

            return itHole.MoveNext();        
        }

    }


}
