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
            set{
                currentClub = value;
                MessagingCenter.Send<Partie>(this, "updateTheCircle");
            }
        }
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
            CurrentClub = club;
        }

      
        public Partie()
        {
            Shots = new List<Shot>();
           CurrentClub = new Club("Fer3",170);
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
        public Tuple<int,int> getIndexHole()
        {
            return new Tuple<int,int>(golfCourse.Holes.IndexOf( itHole.Current)+1,golfCourse.Holes.Count);
        }

        public void addPositionForCurrentHole(MyPosition start,MyPosition oldTarget, MyPosition userPosition)
        {
            Shots.Add(new Shot(CurrentClub,start, oldTarget, userPosition,DateTime.Now));

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
