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
        private List<MyPosition>.Enumerator itHole;

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

        public List<Club> getListClub()
        {
            return Clubs;
        }

        public List<Shot> getListShot()
        {
            return Shots;
        }
        public Partie()
        {
            Shots = new List<Shot>();

        }
        /// <summary>
        /// Retourne le prochain trou si il existe sinon retourne null.
        /// </summary>
        /// <returns>La position du trou.</returns>
        public MyPosition getNextHole()
        {
            return itHole.Current;
        }

        public void addPositionForCurrentHole(MyPosition start,MyPosition oldTarget, MyPosition userPosition)
        {
            Shots.Add(new Shot(currentClub,start, oldTarget, userPosition,DateTime.Now));
        }

        public async void holeFinished(bool saveForStatistics)
        {
            if (saveForStatistics)
            { 
                SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                connection.CreateTable<Shot>();
                connection.InsertAll(Shots);
                
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
