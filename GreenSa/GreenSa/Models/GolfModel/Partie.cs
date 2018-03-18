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
        GolfCourse golfCourse;
        List<Club> listeClubs;

        public GolfCourse getGolfCourse(){
            return golfCourse;
        }
        public void setGolCourse(GolfCourse g){
            golfCourse = g;
        }
        public List<Club> getClubs()
        {
            return listeClubs;
        }
        public void setClubs(List<Club> c)
        {
            listeClubs = c;
        }

    }
}
