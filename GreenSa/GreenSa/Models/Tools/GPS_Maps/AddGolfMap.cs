using TK.CustomMap;
using Xamarin.Forms.Maps;
//using Xamarin.Forms.GoogleMaps;

namespace GreenSa.Models.Tools.GPS_Maps
{
    public class AddGolfMap : TKCustomMap
    {

        public AddGolfMap() {
            this.MapClicked += OnMapClicked;
        }

        public void addPin(Pin p)
        {
            this.Pins.Add(p);
        }


        public void update() {
            this.Pins.Clear();
            this.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.364765, -1.915990), Distance.FromMiles(0.12)));
        }

        public void OnMapClicked(object sender, TKGenericEventArgs<Position> e)
        {
            this.addPin(new Pin()
            {
                Label = "New Pin",
                Position = e.Value
            });
        }

    }
}
