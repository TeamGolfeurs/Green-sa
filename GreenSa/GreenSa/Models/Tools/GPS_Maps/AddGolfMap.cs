using GreenSa.Models.GolfModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GreenSa.Models.Tools.GPS_Maps
{
    public class AddGolfMap : Map , INotifyPropertyChanged
    {
        private List<Position>   routeCoordinates = new List<Position>();


        public List<Position> RouteCoordinates
        {
            get {
                routeCoordinates = new List<Position>();
                routeCoordinates.Add(HolePin.Position);
                return routeCoordinates; 
            }
            set
            {
                routeCoordinates = value;
                MessagingCenter.Send<AddGolfMap>(this, "updateTheMap");

            }
        }
        public List<CustomPin> CustomPins{
            get{
                List<CustomPin> l = new List<CustomPin>();
                l.Add(HolePin);
                return l;
            }
            set{
                
            }
        }

        public CustomPin HolePin { get => holePin; set => holePin = value; }

        private CustomPin holePin;

        public AddGolfMap()
        {

            RouteCoordinates = new List<Position>();
            Position pos = new Position(47.364765, -1.915990);
            HolePin = new CustomPin(CustomPin.MOVABLE)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Trou"
            };
            
        }
        
        public AddGolfMap(MapSpan region) : base(region)
        {
            RouteCoordinates = new List<Position>();
            RouteCoordinates.Add(region.Center);
            RouteCoordinates.Add(region.Center);
            RouteCoordinates.Add(region.Center);

        }


        public void update() {
            /*Device.BeginInvokeOnMainThread(() =>
            {*/
            this.Pins.Clear();
            this.Pins.Add(HolePin);
            var list = new List<Position>();
            list.Add(HolePin.Position);
            this.RouteCoordinates = list;
            // });
            this.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.364765, -1.915990), Distance.FromMiles(0.12)));

        }


    }
}
