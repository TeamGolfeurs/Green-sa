using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using GreenSa.Models.Tools.GPS_Maps;
using Xamarin.Forms.Maps;
using TK.CustomMap;
using System.Collections.ObjectModel;
//using Xamarin.Forms.Maps;
//using Xamarin.Forms.GoogleMaps;

namespace GreenSa.ViewController.Option
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportGolfCourse : ContentPage
    {
       // private Xamarin.Forms.GoogleMaps.Map map;//

        public ImportGolfCourse()
        {
            InitializeComponent();
            golfNameLabel.IsVisible = true;
            golfNameEntry.IsVisible = false;

            map.update();

            /*TKCustomMapPin[] pins = new TKCustomMapPin[20];
            map.CustomPins = pins;

            TKCustomMapPin pin = new TKCustomMapPin();
            var position = new Position(47.364765, -1.915990);
            pin.Position = position;
            pin.Title = "Hole 1";
            pin.Subtitle = "b";
            pin.DefaultPinColor = Color.Blue;
            pin.ShowCallout = true;
            pin.IsDraggable = true;
            pins[0] = pin;*/

            Position clickPos = new Position(47.364765, 1.95990);
            var holePin = new Pin()
            {
                Type = PinType.Place,
                Position = clickPos,
                Label = "Trou"
            };
            map.addPin(holePin);

            /*map.MapClickedCommand = new Command<Position>(Map_ClickedCommand);

            
            map.PinSelected += (sender, e) => this.DisplayAlert("test", "testt", "tessts");*/
        }

        private async void OnLocalizeClick(object sender, EventArgs e)
        {
            var address = cityEntry.Text;
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            if (location != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(0.12)));
            }
        }
    }
}