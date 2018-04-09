using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Club
    {
        public bool selected { get; set; }

        // Id
        private int id;
        public int Id { get => id; set => id = value; }

        public string Name { get; set; }
        public int MinDistance;
        public int MaxDistance;
        public TypeClub TypeClub;

        public Club(string name, TypeClub typeClub)
        {
            Name = name;
            TypeClub = typeClub;
        }

        /*
         * Va chercher dans la base de donnée la valeur moyenne
         **/
        public int getDistanceMoyenne()
        {
            throw new NotImplementedException();
            return 0;
        }
    }

    public enum TypeClub
    {
        BOIS, FER
    }
}
