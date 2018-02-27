using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class Club
    {
        /**
         * id et name ?
         * */
        public string name {get;set;}

       public Club(string name)
        {
            this.name = name;
        }

        /*
         * Va chercher dans la base de donnée la valeur moyenne
         * 
         * */
        public int getDistanceMoyenne()
        {
            return 0;
        }
    }
}
