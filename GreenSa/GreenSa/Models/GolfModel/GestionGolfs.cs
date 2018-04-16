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
        public static  List<GolfCourse> getListGolfsAsync(Filter<GolfCourse>.Filtre filtre)
        {
            if (filtre == null)
                filtre = x => true;
            SQLite.SQLiteConnection connection =  DependencyService.Get<ISQLiteDb>().GetConnection();
            Debug.WriteLine("Got connection");
          
            
            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert
            List<GolfCourse> gfcs = new List<GolfCourse>();
            connection.CreateTable<MyPosition>();
            Debug.WriteLine("Vreated table pos");
            connection.CreateTable<GolfCourseMyPosition>();

            connection.CreateTable<GolfCourse>();
            Debug.WriteLine("Vreated table GolfCourse");

            gfcs = (SQLiteNetExtensions.Extensions.ReadOperations.GetAllWithChildren<GolfCourse>(connection));
            if (gfcs.Count==0)/*!existe dans BD*/
            {
                Debug.WriteLine("PARSING");
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("GreenSa.Ressources.GolfCourses.GolfCourses_Descriptor.xml");
                string text = "";
                using (var reader = new System.IO.StreamReader(stream))//lit fichier contenant la liste des golf
                {
                    text = reader.ReadToEnd();
                }
                                
                //XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load("GreenSa/Ressources/GolfCourses/GolfCourses_Descriptor.xml");
                XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load(GenerateStreamFromString(text));//parsing


                foreach ( var node in xDocumentForListOfGolfCoursesFiles.Element("GolfCourses").Elements("GolfCourse"))//for each file golfCourse
                {


                    stream = assembly.GetManifestResourceStream("GreenSa.Ressources.GolfCourses."+node.Value+".xml");
                    text = "";
                    using (var reader = new System.IO.StreamReader(stream))//read the file
                    {
                        text = reader.ReadToEnd();
                    }

                    XDocument golfC = XDocument.Load(GenerateStreamFromString(text));//xmlparser

                    List<MyPosition> trous = new List<MyPosition>();
                    var nodeGolfC = golfC.Element("GolfCourse");
                    foreach (var trou in nodeGolfC.Element("Coordinates").Elements("Trou"))//get the list of all holes
                    {
                        MyPosition pos = new MyPosition(Double.Parse(trou.Element("lat").Value, CultureInfo.InvariantCulture), Double.Parse(trou.Element("lng").Value, CultureInfo.InvariantCulture));
                        trous.Add(pos);
                    }
                    GolfCourse gc = new GolfCourse(nodeGolfC.Element("Name").Value, nodeGolfC.Element("NomGolf").Value, trous);
                    gfcs.Add(gc);
                }

                //add in the database
                //addGolfCoursesInDatabase(connection,gfcs);
                //connection.InsertAll(gfcs);
                SQLiteNetExtensions.Extensions.WriteOperations.InsertAllWithChildren(connection,gfcs,recursive:true);


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


            return new List<Club> { new Club("Fer1",TypeClub.FER), new Club("Fer2", TypeClub.FER) };
        }

        

        public static Stream GenerateStreamFromString(string s)
        {
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(s);
                    writer.Flush();
                    stream.Position = 0;
                    return stream;
        }
    }


}
