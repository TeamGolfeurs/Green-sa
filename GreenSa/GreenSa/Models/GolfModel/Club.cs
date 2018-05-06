using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Club
    {
        public bool selected { get; set; }//used for IHM
        
        //[PrimaryKey,AutoIncrement]
        public int Id { get; set; }
       // [MaxLength(255)]
        public string Name { get; set; }
        public int MinDistance;
        public int MaxDistance;
        public TypeClub TypeClub;

        public Club(string name, TypeClub typeClub)
        {
            Name = name;
            TypeClub = typeClub;
            selected = true;
        }

        public string getName ()
        {
            return Name;
        }

        
    }

    public enum TypeClub
    {
        BOIS, FER
    }
}
