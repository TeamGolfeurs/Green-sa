using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using static Android.Gms.Maps.GoogleMap;
using Xamarin.Forms;
using GreenSa.Models.Tools.GPS_Maps;
using Greensa.Droid;
using GreenSa.Droid;
using System.Collections.ObjectModel;
using GreenSa.Models.GolfModel;
using GreenSa.ViewController.Play.Game;

[assembly: ExportRenderer(typeof(AddGolfMap), typeof(AddGolfMapRenderer))]
namespace Greensa.Droid
{
    public class AddGolfMapRenderer : MapRenderer
    {

        GoogleMap map;
        private List<Marker> Markers;

        public AddGolfMapRenderer(Context context) : base(context)
        {
            this.Markers = new List<Marker>();

            //Suscribe to get a notification to delete the last pin
            MessagingCenter.Subscribe<Object>(this, "deleteLastPin", (obj) =>
            {
                if (this.Markers.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    var markerToDelete = this.Markers[this.Markers.Count - 1];
                    this.Markers.RemoveAt(this.Markers.Count - 1);
                    markerToDelete.Remove();
                }
            });
            //Suscribe to get a notification to delete the last pin
            MessagingCenter.Subscribe<Object>(this, "deleteAllPins", (obj) =>
            {
                if (this.Markers.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    foreach (Marker markerToDelete in this.Markers)
                    {
                        markerToDelete.Remove();
                    }
                    this.Markers.Clear();
                }
            });
        }


        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) { }

            if (e.NewElement != null)
            {
                ((MapView)Control).GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
        }


        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            this.map = map;
            map.MapClick += (sender, e) =>
            {
                if (this.Markers.Count < 18)
                {
                    var pin = new Pin()
                    {
                        Label = "Trou n°"+ (this.Markers.Count+1),
                        Position = new Position(e.Point.Latitude, e.Point.Longitude)
                    };
                    MessagingCenter.Send<Pin>(pin, "getAddGolfMapPins");
                    var marker = base.CreateMarker(pin);
                    marker.Draggable(true);
                    BitmapDescriptor ic = BitmapDescriptorFactory.FromResource(Resource.Drawable.flag);
                    marker.SetIcon(ic);
                    var addedMarker = this.map.AddMarker(marker);
                    this.Markers.Add(addedMarker);
                }
            };
        }
    }
}
