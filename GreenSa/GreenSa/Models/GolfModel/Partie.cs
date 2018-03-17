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

        public GolfCourse GolfCourse { get => golfCourse; set => golfCourse = value; }
        public List<Club> Clubs { get => clubs; set => clubs = value; }

        /// <summary>
        /// Retourne le prochain trou si il existe sinon retourne null.
        /// </summary>
        /// <returns>La position du trou.</returns>
        public MyPosition getNextHole()
        {
            if (hasNextHole())
            {
                return golfCourse.Holes.GetEnumerator().Current;
            }
            return null;

        }

        public void addPositionForCurrentHole(MyPosition oldTarget, MyPosition userPosition)
        {
            throw new NotImplementedException();
        }

        public void holeFinished()
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Vérifie l'existence d'un prochain trou.
        /// </summary>
        /// <returns></returns>
        public bool hasNextHole()
        {
            return true;
            return golfCourse.Holes.GetEnumerator().MoveNext();
        }

    }


}
