using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    /**
     * Contient toutes les infos nécessaire à la partie
     * */
    public class Partie
    {
        public GolfCourse golfCourse { get; set; }
        public List<Club> listeClubs { get; set; }
        public Club currentClub { get; set; }
        

    }

    
}
