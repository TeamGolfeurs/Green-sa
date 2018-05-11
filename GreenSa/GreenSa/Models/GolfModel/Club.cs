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

        public static Club PUTTER = new Club("Putter", 0);
        [Ignore]
        public bool selected { get; set; }//used for IHM

        [PrimaryKey]
        public string Name { get; set; }
        public int DistanceMoyenne { get; set; }
        [Ignore]
        public int DistanceMoyenneJoueur
        {
            get
            {
                IEnumerable<Tuple<Club, double>> listWith1item = StatistiquesGolf.getAverageDistanceForClubs(c => c.Equals(this));
                if (listWith1item.Count() == 0)
                    return DistanceMoyenne;
                return (int)listWith1item.First().Item2;
            }
        }

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
