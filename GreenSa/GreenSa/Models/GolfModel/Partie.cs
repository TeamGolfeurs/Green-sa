using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    /**
     * Contient toutes les infos nécessaire à la partie
     * */
    public class Partie
    {

        private GolfCourse golfCourse;
        private List<Club> clubs;
        public Club currentClub;

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
        public List<Club> Clubs { get => clubs; set => clubs = value; }
        private List<MyPosition>.Enumerator itHole;

        public Partie()
        {

        }
        /// <summary>
        /// Retourne le prochain trou si il existe sinon retourne null.
        /// </summary>
        /// <returns>La position du trou.</returns>
        public MyPosition getNextHole()
        {
            return itHole.Current;

        }

        public void addPositionForCurrentHole(MyPosition oldTarget, MyPosition userPosition)
        {
            return;
            throw new NotImplementedException();
        }

        public void holeFinished()
        {
            return;
            throw new NotImplementedException();

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
