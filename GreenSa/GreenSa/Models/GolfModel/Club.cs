using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Club
    {
        [Ignore]
        public bool selected { get; set; }//used for IHM

        [PrimaryKey]
        public string Name { get; set; }
        public int DistanceMoyenne { get; set; }
        public Club()
        {
            selected = true;
        }

        public Club(string name,int distMoy)
        {
            Name = name;
            selected = true;

            DistanceMoyenne = distMoy;
        }

        /*
         * Va chercher dans la base de donnée la valeur moyenne
         **/
        public int getDistanceMoyenne()
        {
           
            return DistanceMoyenne;
        }

        public override string ToString()
        {
            return Name+"  dMoy = "+DistanceMoyenne;
        }

        public override bool Equals(object obj)
        {
            return obj is Club && ((Club)obj).Name==Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    
}
