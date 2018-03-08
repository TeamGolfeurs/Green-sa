using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools.Services
{
    class GpsService : Service
    {
        public GpsService()
        {
        }

        public static Position getCurrentPosition()  
        {
            if (!isAvaible()) throw new NotAvaibleException();

            return new Position(48.109450, -1.639983);
        }


        public static bool isAvaible()
        {
            throw new NotImplementedException();
        }


    }
}
