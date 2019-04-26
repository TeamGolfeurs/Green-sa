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
        public static async Task<List<GolfCourse>> getListGolfsAsync(Func<GolfCourse, bool> filtre)
        {
            if (filtre == null)
                filtre = x => true;
            SQLite.SQLiteAsyncConnection connection =  DependencyService.Get<ISQLiteDb>().GetConnectionAsync();          
            
            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert
             
            await connection.CreateTableAsync<Hole>();
            await connection.CreateTableAsync<MyPosition>();
            await connection.CreateTableAsync<GolfCourse>();

            List<GolfCourse> gfcs = (await SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<GolfCourse>(connection, recursive: true));
            if (gfcs.Count==0)/*!existe dans BD*/
            {
                gfcs = GolfXMLReader.getListGolfCourseFromXMLFiles();
               
                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                await SQLiteNetExtensionsAsync.Extensions.WriteOperations.InsertOrReplaceAllWithChildrenAsync(connection,gfcs,true);


            }
            return gfcs;
        }

        




        /**
         * Donne une liste de club en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les club seront récupérés.
         * */
        //NOT IMPLEMENTED YET
        public static async Task<List<Club>> getListClubsAsync(Func<Club, bool> filtre)
        {
            if (filtre == null)
                filtre = x => true;

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML et on insert


            SQLite.SQLiteAsyncConnection connection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert
            List<Club> clubs = new List<Club>();
            await connection.CreateTableAsync<Club>();

            clubs = (await SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<Club>(connection));
            if (clubs.Count == 0)/*!existe dans BD*/
            {
                clubs = GolfXMLReader.getListClubFromXMLFiles();

                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                //SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection, clubs, recursive: true);
                await connection.InsertAllAsync(clubs);

            }
            return clubs;
        }
        public async static void calculAverageAsync(List<Club> clubs)
        {
            listAverage = await StatistiquesGolf.getAverageDistanceForClubsAsync(c => clubs.Contains(c), clubs);
        }

        public static Club giveMeTheBestClubForThatDistance(List<Club> clubs, double dUserTarget)
        {
            /* 
             * VERSION PLUS FACILE MAIS TROP LENTE
            Club minDiffClub = clubs.First();
            double minDiff = Math.Abs(dUserTarget - listAverage.ToList().Find(c => c.Item1.Name == minDiffClub.Name).Item2);
            double dist = -1;
            foreach (Club c in clubs)
            {
                if (!c.Equals(Club.PUTTER))
                {
                    dist = Math.Abs(dUserTarget - listAverage.ToList().Find(c1 => c1.Item1.Name.Equals(c.Name)).Item2);
                    Debug.WriteLine(c.Name + " - dist : " + dist + " - minDiff : " + minDiff);
                    if (dist < minDiff)
                    {
                        minDiff = dist;
                        minDiffClub = c;
                    }
                }
            }
              return minDiffClub;
            */
           Club minDiffClub = null;
            double minDiff = -1;
            if (dUserTarget < 10)
            {
                minDiffClub = Club.PUTTER;
            } else
            {
                foreach (Tuple<Club, double> tuple in listAverage)
                {
                    Club c = tuple.Item1;
                    if (!c.Equals(Club.PUTTER))
                    {
                        if (minDiffClub == null)
                        {
                            minDiffClub = c;
                            minDiff = Math.Abs(dUserTarget - tuple.Item2);
                        }
                        else
                        {
                            double distanceC = tuple.Item2;
                            double distDiff = Math.Abs(dUserTarget - distanceC);
                            if (distDiff < minDiff)
                            {
                                minDiffClub = c;
                                minDiff = distDiff;
                            }
                        }
                    }
                }
            }
            
            return minDiffClub;

        }
    }


}
