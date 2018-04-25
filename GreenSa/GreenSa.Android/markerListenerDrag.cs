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
using static Android.Views.View;
using static Android.Gms.Maps.GoogleMap;
using Android.Gms.Maps.Model;
using Java.Lang;
using GreenSa.Droid;
using Xamarin.Forms;
using GreenSa.Models.Tools.GPS_Maps;

namespace Greensa.Droid
{
    public class markerListenerDrag : Java.Lang.Object, IOnMarkerDragListener
    {
        CustomMapRenderer cmr;
        public markerListenerDrag(CustomMapRenderer cmr)
        {
            this.cmr = cmr;
        }
        

        public void OnMarkerDrag(Marker marker)
        {
            CustomPin pin = new CustomPin(CustomPin.HOLE);
            pin.Position = new Xamarin.Forms.Maps.Position(marker.Position.Latitude, marker.Position.Longitude);

            //send message to CustomMap
            MessagingCenter.Send<CustomPin>(pin,CustomPin.UPDATEDMESSAGE);


            cmr.UpdatePolyLinePos(false,marker.Position);
        }

        public void OnMarkerDragEnd(Marker marker)
        {
            cmr.UpdatePolyLinePos(false,marker.Position);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.shape_circle));

        }

        public void OnMarkerDragStart(Marker marker)
        {
            cmr.UpdatePolyLinePos(false, marker.Position);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.funny));
            
        }


    }
}