using GreenSa.Models.Tools;
using GreenSa.Persistence;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace GreenSa.Models.GolfModel
{
    public class GestionGolfs
    {
        private static IEnumerable<Tuple<Club, double>> listAverage;

        /**
* Donne une liste de golf en fonction d'un filtre
* le fitre peut être null, dans ce cas tous les golfs seront récupérés.
* */
        //NOT IMPLEMENTED YE
        public static  List<GolfCourse> getListGolfs(Func<GolfCourse, bool> filtre)
        {
            if (filtre == null)
                filtre = x => true;
            SQLite.SQLiteConnection connection =  DependencyService.Get<ISQLiteDb>().GetConnection();          
            
            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert
            List<GolfCourse> gfcs = new List<GolfCourse>();
            connection.CreateTable<Hole>();

            connection.CreateTable<MyPosition>();
            connection.CreateTable<GolfCourse>();

            gfcs = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection, recursive: true));
            if (gfcs.Count==0)/*!existe dans BD*/
            {
                gfcs = GolfXMLReader.getListGolfCourseFromXMLFiles();
               
                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                SQLiteNetExtensions.Extensions.WriteOperations.InsertOrReplaceAllWithChildren(connection,gfcs,true);


            }
            return gfcs;
        }

        




        /**
         * Donne une liste de golf en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les golfs seront récupérés.
         * */
        //NOT IMPLEMENTED YET
        public static List<Club> getListClubs(Func<Club, bool> filtre)
        {
            if (filtre == null)
                filtre = x => true;

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML et on insert


            SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert
            List<Club> clubs = new List<Club>();
            connection.CreateTable<Club>();

            clubs = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<Club>(connection));
            if (clubs.Count == 0)/*!existe dans BD*/
            {
                clubs = GolfXMLReader.getListClubFromXMLFiles();

                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                //SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection, clubs, recursive: true);
                connection.InsertAll(clubs);

            }
            return clubs;
        }
        public static void calculAverage(IEnumerable<Club> clubs)
        {
             listAverage = StatistiquesGolf.getAverageDistanceForClubs(c => clubs.Contains(c));
        }

        public static Club giveMeTheBestClubForThatDistance(List<Club> clubs, double dUserTarget)
        {
            /* 
             * VERSION PLUS FACILE MAIS TROP LENTE
             * Club minDiffClub = clubs.First();
              int minDiff = (int) Math.Abs(dUserTarget- minDiffClub.DistanceMoyenneJoueur.Item1);
              foreach (Club c in clubs)
              {
                  double dist = Math.Abs(dUserTarget - minDiffClub.DistanceMoyenneJoueur.Item1); 
                  if (dist <minDiff)
                  {
                      minDiff = (int)dist;
                      minDiffClub = c;
                  }
              }
              return minDiffClub;*/
            Club minDiffClub = null;
            double minDiff = -1;
            foreach(Tuple<Club,double> tuple in listAverage)
            {
                if(minDiffClub==null)
                {
                    minDiffClub = tuple.Item1;
                    minDiff = Math.Abs(dUserTarget - tuple.Item2);
                }
                else
                {
                    Club c = tuple.Item1;
                    double distanceC = tuple.Item2;
                    double distDiff = Math.Abs(dUserTarget - distanceC);
                    if (distDiff<minDiff)
                    {
                        minDiffClub = c;
                        minDiff = distDiff;
                    }
                }
            }

            IEnumerable<Club> listeClubFait = listAverage.Select<Tuple<Club, double>, Club>((tuple) => tuple.Item1);
            IEnumerable<Club> clubsNotFait = clubs.Where((club) => !listeClubFait.Contains(club));

            foreach(Club clubNotFait in clubsNotFait)
            {
                if (minDiffClub == null)
                {
                    minDiffClub = clubNotFait;
                    double distDiff = Math.Abs(dUserTarget - clubNotFait.DistanceMoyenne);
                }
                else
                {
                    double distDiff = Math.Abs(dUserTarget - clubNotFait.DistanceMoyenne);
                    if (distDiff < minDiff)
                    {
                        minDiffClub = clubNotFait;
                        minDiff = distDiff;
                    }
                }
            }

            return minDiffClub;

        }
    }


}
