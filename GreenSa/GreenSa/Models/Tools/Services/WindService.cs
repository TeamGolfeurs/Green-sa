using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenSa.Models.Tools.Services
{
    public class WindService : Service
    {
        /**
         * Methode allant chercher des infos sur une API web pour retrouver des infos concernant le vent.
         * */
        /*async*/ public static WindInfo getCurrentWindInfo()
        {
            if (!isAvaible()) throw new NotAvaibleException();

            ImageSource img = ImageSource.FromResource("GreenSa.Ressources.Images.left-arrow.png");
            return new WindInfo(12,img);
        }



        public static bool isAvaible()
        {
            return true;
            throw new NotImplementedException();
        }

     
    }
}
