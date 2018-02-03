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

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Greensa.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        public CustomMapRenderer() : base() { }

        public CustomMapRenderer(Context context) : base(context){}

        GoogleMap map;
        Polyline polyline;//the current polyline
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

           /* if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            {
                Console.WriteLine("DEBUG---------- - Changed ! ");
               // UpdatePolyLine();
            }*/
        }


        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            this.map = map;
            map.SetOnMarkerDragListener(new markerListenerDrag(this));
           
            UpdatePolyLinePos(true);
        }


        protected override MarkerOptions CreateMarker(Pin pin)
        {
            if(!( pin is CustomPinMovable ))
            {
                return base.CreateMarker(pin);
            }
            var marker = base.CreateMarker(pin);
            marker.Draggable(true);
            marker.SetRotation(30.5f);
            marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));

            return marker;
        }
        public void UpdatePolyLinePos(bool init,LatLng pos=null)
        {
            if (polyline != null)
            {
                polyline.Remove();
                polyline.Dispose();           
            }
            //Console.WriteLine("Mise à jour de la POLYLIGNE !!!! (gg)");
            var polylineOptions = new PolylineOptions();
            polylineOptions.Clickable(true);
            polylineOptions.InvokeJointType(JointType.Round);//don't see the difference
            polylineOptions.InvokeWidth(10f);
            polylineOptions.InvokeColor(0x664444FF);

            int i = 0;
            foreach (var position in ((CustomMap)this.Element).RouteCoordinates)
            {
                if (i == 1 && !init)
                    polylineOptions.Add(pos);
                else
                    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));

                i++;
            }
            polyline = map.AddPolyline(polylineOptions);
        }

    }
}
