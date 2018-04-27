using System;
using CoreLocation;
using Foundation;
using MapKit;
using Xamarin.Forms.Maps;

namespace GreenSa.iOS
{
    public class CustomMKAnnotation : MKPointAnnotation
    {

        public Pin Pin{get;set;}
        CustomMapRenderer CustomMapRenderer { get; set; }
        //[Export("setCoordinate:")]
        public override void SetCoordinate(CLLocationCoordinate2D value)
        {
            base.SetCoordinate(value);
            Pin.Position = new Position(value.Latitude, value.Longitude);
            CustomMapRenderer.UpdatePolyLinePos();
        }
      


	}
}
