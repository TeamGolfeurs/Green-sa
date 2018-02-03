using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace GreenSa.Model.Tools.GPS_Maps
{
    public class CustomPinMovable : Xamarin.Forms.Maps.Pin
    {
        //
        // Résumé :
        //     The latitude and longitude of the Xamarin.Forms.Maps.Pin.
        //
        // Notes :
        //     To be added.
        public new Position Position { get
            {
                return base.Position;
            }
             set
            {
                base.Position = value;
                
            }
        }


        public CustomPinMovable() : base()
        {

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        /*public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
