using GreenSa.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class GestionGolfs
    {


        /*
         *
         
         * 
         * */
        /**
         * Donne une liste de golf en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les golfs seront récupérés.
         * */
        //NOT IMPLEMENTED YET
        public static List<GolfCourse> getListGolfs(Filter<GolfCourse>.Filtre filtre )
        {
            if (filtre == null)
                filtre = x => true;

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML (/Ressources) et on insert


            return new List<GolfCourse> { new GolfCourse("Fer1"), new GolfCourse("Driver") };
        }



        /**
         * Donne une liste de golf en fonction d'un filtre
         * le fitre peut être null, dans ce cas tous les golfs seront récupérés.
         * */
        //NOT IMPLEMENTED YET
        public static List<Club> getListClubs(Filter<GolfCourse>.Filtre filtre)
        {
            if (filtre == null)
                filtre = x => true;

            //récup avec filtre
            //utilise SQLite
            //si la table n'existe pas encore on parse les fichiers XML et on insert


            return new List<Club> { new Club("Fer1"), new Club("Fer2") };
        }
    }

    
}
