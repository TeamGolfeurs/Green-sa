using GreenSa.Models.GolfModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GreenSa.Models.Tools
{
    class GolfXMLReader
    {
        public static List<GolfCourse> getListGolfCourseFromXMLFiles()
        {
            List<GolfCourse> gfcs = new List<GolfCourse>();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("GreenSa.Ressources.GolfCourses.GolfCourses_Descriptor.xml");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))//lit fichier contenant la liste des golf
            {
                text = reader.ReadToEnd();
            }

            //XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load("GreenSa/Ressources/GolfCourses/GolfCourses_Descriptor.xml");
            XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load(GenerateStreamFromString(text));//parsing


            foreach (var node in xDocumentForListOfGolfCoursesFiles.Element("GolfCourses").Elements("GolfCourse"))//for each file golfCourse
            {
                stream = assembly.GetManifestResourceStream("GreenSa.Ressources.GolfCourses." + node.Value + ".xml");
                text = "";
                using (var reader = new System.IO.StreamReader(stream))//read the file
                {
                    text = reader.ReadToEnd();
                }

                XDocument golfC = XDocument.Load(GenerateStreamFromString(text));//xmlparser

                List<Hole> trous = new List<Hole>();
                var nodeGolfC = golfC.Element("GolfCourse");
                foreach (var trou in nodeGolfC.Element("Coordinates").Elements("Trou"))//get the list of all holes
                {
                    MyPosition pos = new MyPosition(Double.Parse(trou.Element("lat").Value, CultureInfo.InvariantCulture), Double.Parse(trou.Element("lng").Value, CultureInfo.InvariantCulture));
                    trous.Add(new Hole(pos,2));//TODO modif
                }
                GolfCourse gc = new GolfCourse(nodeGolfC.Element("Name").Value, nodeGolfC.Element("NomGolf").Value, trous);
                gfcs.Add(gc);
            }
            return gfcs;
        }

        public static List<Club> getListClubFromXMLFiles()
        {
            List<Club> clubs = new List<Club>();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("GreenSa.Ressources.Clubs.Clubs_Descriptor.xml");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))//lit fichier contenant la liste des golf
            {
                text = reader.ReadToEnd();
            }

            //XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load("GreenSa/Ressources/GolfCourses/GolfCourses_Descriptor.xml");
            XDocument xDocumentForListOfGolfCoursesFiles = XDocument.Load(GenerateStreamFromString(text));//parsing


            foreach (var node in xDocumentForListOfGolfCoursesFiles.Element("Clubs").Elements("Club"))//for each file golfCourse
            {
                stream = assembly.GetManifestResourceStream("GreenSa.Ressources.Clubs." + node.Value + ".xml");
                text = "";
                using (var reader = new System.IO.StreamReader(stream))//read the file
                {
                    text = reader.ReadToEnd();
                }

                XDocument golfC = XDocument.Load(GenerateStreamFromString(text));//xmlparser

                var nodeGolfC = golfC.Element("Club");
                Club gc = new Club(nodeGolfC.Element("Name").Value, int.Parse(nodeGolfC.Element("DistanceMoyenne").Value));
                clubs.Add(gc);
            }
            return clubs;
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
