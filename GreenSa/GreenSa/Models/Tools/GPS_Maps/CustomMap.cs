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
    public class CustomMap : Map , INotifyPropertyChanged
    {
        private List<Position>   routeCoordinates = new List<Position>();


        public List<Position> RouteCoordinates
        {
            get {
                routeCoordinates = new List<Position>();
                routeCoordinates.Add(UserPin.Position);
                routeCoordinates.Add(TargetPin.Position);
                routeCoordinates.Add(HolePin.Position);
                return routeCoordinates; 
            }
            set
            {
                routeCoordinates = value;
                MessagingCenter.Send<CustomMap>(this, "updateTheMap");

            }
        }
        public List<CustomPin> CustomPins{
            get{
                List<CustomPin> l = new List<CustomPin>();
                l.Add(UserPin);
                l.Add(TargetPin);
                l.Add(HolePin);
                return l;
            }
            set{
                
            }
        }
        public CustomPin UserPin { get => userPin; set => userPin = value; }
        public CustomPin TargetPin { get => targetPin; set => targetPin = value; }
        public CustomPin HolePin { get => holePin; set => holePin = value; }

        private CustomPin userPin;
        private CustomPin targetPin;
        private CustomPin holePin;

        public CustomMap()
        {


            //message which come from the markerListenerDrag (android only, ios set direclty)
            //when the target pin is moved =>update the model position of the target
            MessagingCenter.Subscribe<CustomPin>(this, CustomPin.UPDATEDMESSAGE, (sender) => {
                TargetPin.Position = sender.Position;
            });

            RouteCoordinates = new List<Position>();
            Position pos = new Position(0, 0);
            UserPin = new CustomPin(CustomPin.USER)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Je suis là"
            };
            TargetPin = new CustomPin(CustomPin.MOVABLE)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Je suis là"
            };
            HolePin = new CustomPin(CustomPin.HOLE)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Je suis là"
            };
            
        }
        
        public CustomMap(MapSpan region) : base(region)
        {
            RouteCoordinates = new List<Position>();
            RouteCoordinates.Add(region.Center);
            RouteCoordinates.Add(region.Center);
            RouteCoordinates.Add(region.Center);

        }

        public void setUserPosition(MyPosition pos)
        {
            UserPin.Position = new Position(pos.X, pos.Y);

            TargetPin.Position = calculationNewInterTarget();
            update();

        }


        public void setHolePosition(MyPosition pos)
        {
            HolePin.Position = new Position(pos.X, pos.Y);

        }

        public Position calculationNewInterTarget()
        {
            return new Position(  (UserPin.Position.Latitude+ HolePin.Position.Latitude )/2, 
                                    (UserPin.Position.Longitude + HolePin.Position.Longitude)/2);
        }



        public void update()
        {
            Pins.Clear();
            this.Pins.Add(UserPin);
            this.Pins.Add(TargetPin);
            this.Pins.Add(HolePin);
            /*Device.BeginInvokeOnMainThread(() =>
            {*/
            var list = new List<Position>();
            list.Add(UserPin.Position);
            list.Add(TargetPin.Position);
            list.Add(HolePin.Position);
            this.RouteCoordinates = list;
            // });

            this.MoveToRegion(MapSpan.FromCenterAndRadius(TargetPin.Position, Distance.FromMiles(1)));
        }


        public double getDistanceUserHole()
        {
            return DistanceTo(UserPin.Position.Latitude, UserPin.Position.Longitude,
                                HolePin.Position.Latitude, HolePin.Position.Longitude,"M");
        }

        public double getDistanceTargetHole()
        {
            return DistanceTo(TargetPin.Position.Latitude, TargetPin.Position.Longitude,
                                HolePin.Position.Latitude, HolePin.Position.Longitude, "M");
        }

        public double getDistanceUserTarget()
        {
            return DistanceTo(UserPin.Position.Latitude, UserPin.Position.Longitude,
                                TargetPin.Position.Latitude, TargetPin.Position.Longitude, "M");
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, string unit)
        {
            var rlat1 = Math.PI * lat1 / 180;
            var rlat2 = Math.PI * lat2 / 180;
            var rlon1 = Math.PI * lon1 / 180;
            var rlon2 = Math.PI * lon2 / 180;

            var theta = lon1 - lon2;
            var rtheta = Math.PI * theta / 180;

            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            if (unit == "K") { dist = dist * 1.609344; }
            if (unit == "M") { dist = dist * 1.609344 * 1000; }
            if (unit == "N") { dist = dist * 0.8684; }
            return dist;
        }

        internal void setTargetMovable()
        {

            TargetPin.type = CustomPin.MOVABLE;
            update();
        }

        public void lockTarget()
        {
            TargetPin.type = CustomPin.LOCKED;
            update();
        }
        public new event PropertyChangedEventHandler PropertyChanged;

        internal MyPosition getUserPosition()
        {
            return new MyPosition(userPin.Position.Latitude, userPin.Position.Longitude);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
