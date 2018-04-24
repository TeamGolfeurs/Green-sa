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
        /**
         * Donne une liste de golf en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les golfs seront récupérés.
         * */
        //NOT IMPLEMENTED YE
        public static  List<GolfCourse> getListGolfs(Filter<GolfCourse>.Filtre filtre)
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

            gfcs = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection,(g)=>true,true));
            if (gfcs.Count==0)/*!existe dans BD*/
            {
                gfcs = GolfXMLReader.getListGolfCourseFromXMLFiles();
               
                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection,gfcs,true);


            }
            return gfcs;
        }

        




        /**
         * Donne une liste de golf en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les golfs seront récupérés.
         * */
        //NOT IMPLEMENTED YET
        public static List<Club> getListClubs(Filter<Club>.Filtre filtre)
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

      

       
    }


}
