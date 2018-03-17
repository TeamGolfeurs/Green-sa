using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static GreenSa.Models.Tools.GPS_Maps.CustomMap;

namespace GreenSa.Models.Tools.GPS_Maps
{
    public class CustomPin : Xamarin.Forms.Maps.Pin
    {
        public static string UPDATEDMESSAGE = "updatePosion";


        public static string LOCKED = "NO_MOVABLE";
        public static string MOVABLE = "MOVABLE";
        public static string HOLE = "HOLE";
        public static string USER = "USER";

        public string type { get; set; }

        private CustomMap map;

        //
        // Résumé :
        //     The latitude and longitude of the Xamarin.Forms.Maps.Pin.
        //
        // Notes :
        //     To be added.
        public new Xamarin.Forms.Maps.Position Position {
            get {return base.Position;}
            set {base.Position=value;
                Debug.WriteLine("UPDATE SET POSITION IN CUSTOM PIN");

            }
        }


        public CustomPin(string type) : base()
        {
            this.type = type;
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
