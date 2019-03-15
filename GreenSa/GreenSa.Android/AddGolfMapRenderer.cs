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

        public AddGolfMapRenderer(Context context) : base(context)
        {
            this.pinsCount = 0;
        }

        GoogleMap map;
        int pinsCount;
        Marker selectedMarker;

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

        private class MyMarkerClickListener : Java.Lang.Object, IOnMarkerClickListener
        {
            AddGolfMapRenderer context;
            public MyMarkerClickListener(AddGolfMapRenderer context)
            {
                this.context = context;
            }

            public bool OnMarkerClick(Marker marker)
            {
                this.context.selectedMarker = marker;
                MessagingCenter.Send<Object>(marker, "pinClicked");
                return true;
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            this.map = map;
            MessagingCenter.Subscribe<Object>(this, "deleteSelectedPin", (obj) =>
            {
                this.selectedMarker.Remove();
            });
            /*map.MarkerClick += (sender, e) =>
            {
                this.selectedMarker = sender as Marker;
                MessagingCenter.Send<Object>(sender, "pinClicked");
            };*/
            map.SetOnMarkerClickListener(new MyMarkerClickListener(this));
            
            map.MapClick += (sender, e) =>
            {
                if (this.pinsCount < 18)
                {
                    this.pinsCount++;
                    var pin = new Pin()
                    {
                        Label = "Trou n°"+this.pinsCount,
                        Position = new Position(e.Point.Latitude, e.Point.Longitude)
                    };
                    MessagingCenter.Send<Pin>(pin, "getAddGolfMapPins");
                    var marker = base.CreateMarker(pin);
                    marker.Draggable(true);
                    BitmapDescriptor ic = BitmapDescriptorFactory.FromResource(Resource.Drawable.flag);
                    marker.SetIcon(ic);
                    var addedMarker = this.map.AddMarker(marker);
                }
            };
        }
    }
}
