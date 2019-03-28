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


        [PrimaryKey]
        public string Name { get; set; }

        public bool selected { get; set; }//used for IHM
        public int DistanceMoyenne { get; set; }
        [Ignore]
        public Tuple<int,int,int> DistanceMoyenneJoueur
        {//moy,min,max
             get
            {
                IEnumerable<Tuple<Club, double>> listWith1item = StatistiquesGolf.getAverageDistanceForClubsAsync(c => c.Equals(this));
                if (listWith1item.Count() == 0)
                    return new Tuple<int, int, int>( DistanceMoyenne, 0, 0);
               Tuple<double, double> minMax = StatistiquesGolf.getMinMaxDistanceForClubs(this);

                return new Tuple<int, int, int>((int)listWith1item.First().Item2, (int)minMax.Item1, (int)minMax.Item2);
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
