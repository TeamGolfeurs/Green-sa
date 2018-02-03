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
            cmr.UpdatePolyLinePos(false,marker.Position);
        }

        public void OnMarkerDragEnd(Marker marker)
        {
            cmr.UpdatePolyLinePos(false,marker.Position);
            marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));

        }

        public void OnMarkerDragStart(Marker marker)
        {
            cmr.UpdatePolyLinePos(false, marker.Position);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.funny));
            
        }


    }
}