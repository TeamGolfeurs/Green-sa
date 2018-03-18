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
        public  delegate void MyAction();


        public List<Position> RouteCoordinates
        {
            get { return routeCoordinates; }
            set
            {
                routeCoordinates = value;
                MessagingCenter.Send<CustomMap>(this, "updateThisPosition");

            }
        }

        private CustomPin userPin;
        private CustomPin targetPin;
        private CustomPin holePin;

        public CustomMap()
        {


            //message which come from the markerListenerDrag,
            //when the target pin is moved =>update the model position of the target
            MessagingCenter.Subscribe<CustomPin>(this, CustomPin.UPDATEDMESSAGE, (sender) => {
                targetPin.Position = sender.Position;
            });

            RouteCoordinates = new List<Position>();
            Position pos = new Position(0, 0);
            userPin = new CustomPin(CustomPin.USER)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Je suis là"
            };
            targetPin = new CustomPin(CustomPin.MOVABLE)
            {
                Type = PinType.Place,
                Position = pos,
                Label = "Je suis là"
            };
            holePin = new CustomPin(CustomPin.HOLE)
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
            userPin.Position = new Position(pos.X, pos.Y);

            targetPin.Position = calculationNewInterTarget();
            update();

        }


        public void setHolePosition(MyPosition pos)
        {
            holePin.Position = new Position(pos.X, pos.Y);

        }

        public Position calculationNewInterTarget()
        {
            return new Position(  (userPin.Position.Latitude+ holePin.Position.Latitude )/2, 
                                    (userPin.Position.Longitude + holePin.Position.Longitude)/2);
        }



        public void update()
        {
            Pins.Clear();
            this.Pins.Add(userPin);
            this.Pins.Add(targetPin);
            this.Pins.Add(holePin);
            /*Device.BeginInvokeOnMainThread(() =>
            {*/
            var list = new List<Position>();
            list.Add(userPin.Position);
            list.Add(targetPin.Position);
            list.Add(holePin.Position);
            this.RouteCoordinates = list;
            // });

            this.MoveToRegion(MapSpan.FromCenterAndRadius(targetPin.Position, Distance.FromMiles(1)));
        }


        public double getDistanceUserHole()
        {
            return DistanceTo(userPin.Position.Latitude, userPin.Position.Longitude,
                                holePin.Position.Latitude, holePin.Position.Longitude,"M");
        }

        public double getDistanceTargetHole()
        {
            return DistanceTo(targetPin.Position.Latitude, targetPin.Position.Longitude,
                                holePin.Position.Latitude, holePin.Position.Longitude, "M");
        }

        public double getDistanceUserTarget()
        {
            return DistanceTo(userPin.Position.Latitude, userPin.Position.Longitude,
                                targetPin.Position.Latitude, targetPin.Position.Longitude, "M");
        }

        static double DistanceTo(double lat1, double lon1, double lat2, double lon2, string unit)
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

            targetPin.type = CustomPin.MOVABLE;
            update();
        }

        public void lockTarget()
        {
            targetPin.type = CustomPin.LOCKED;
            update();
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
