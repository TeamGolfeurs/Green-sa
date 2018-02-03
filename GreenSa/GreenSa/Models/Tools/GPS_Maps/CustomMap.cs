using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GreenSa.Model.Tools.GPS_Maps
{
    public class CustomMap : Map , INotifyPropertyChanged
    {
        public static readonly BindableProperty RouteCoordinatesProperty =
             BindableProperty.Create<CustomMap, List<Position>>(p => p.RouteCoordinates, new List<Position>());

        public List<Position> RouteCoordinates
        {
            get
            {
                return (List<Position>)GetValue(RouteCoordinatesProperty);
            }

            set {
                SetValue(RouteCoordinatesProperty, value);
                OnPropertyChanged(CustomMap.RouteCoordinatesProperty.PropertyName);

            }
        }
        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }

        public CustomMap(MapSpan region) : base(region)
        {
            RouteCoordinates = new List<Position>();

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
