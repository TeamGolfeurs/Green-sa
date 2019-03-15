using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using GreenSa.Models.Tools.GPS_Maps;
using Xamarin.Forms.Maps;
using TK.CustomMap;
using System.Collections.ObjectModel;
using System.Collections.Generic;
//using Xamarin.Forms.Maps;
//using Xamarin.Forms.GoogleMaps;

namespace GreenSa.ViewController.Option
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportGolfCourse : ContentPage
    {
        // private Xamarin.Forms.GoogleMaps.Map map;//
        List<Pin> pins;

        public ImportGolfCourse()
        {
            InitializeComponent();
            this.SetGolfNameVisibility(false);
            this.deletePin.IsVisible = false;

            this.pins = new List<Pin>();

            MessagingCenter.Subscribe<Pin>(this, "pinClicked", (pin) =>
            {
                this.deletePin.IsVisible = true;
            });
            MessagingCenter.Subscribe<Pin>(this, "getAddGolfMapPins", (pin) => {
                this.pins.Add(pin);
                if (this.pins.Count == 18)
                {
                    this.SetGolfNameVisibility(true);
                } else
                {
                    if (this.pins.Count == 9)
                    {
                        this.SetGolfNameVisibility(true);
                    }
                    else
                    {
                        this.SetGolfNameVisibility(false);
                    }
                }
                
            });
            map.update();
        }

        private void SetGolfNameVisibility(Boolean isVisible)
        {
            golfNameLabel.IsVisible = isVisible;
            golfNameEntry.IsVisible = isVisible;
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

        private void OnValidateClick(object sender, EventArgs e)
        {
            if (this.pins.Count == 9)
            {
                this.DisplayAlert("Succès", "Le 9 trous : " + golfNameEntry.Text + " a été créé avec succès", "Continuer");
            }
            else if (this.pins.Count == 18)
            {
                this.DisplayAlert("Succès", "Le 18 trous : " + golfNameEntry.Text + " a été créé avec succès", "Continuer");
            }
            else
            {
                this.DisplayAlert("Erreur", "Un parcours ne peut être que de 9 ou 18 trous", "Ok");
            }
        }

        private async void OnDeletePinClick(object sender, EventArgs e)
        {
            var confirmDelete = await this.DisplayAlert("Suppression d'un trou", "Voulez vous supprimer ce trou ?", "Oui", "Non");
            if (confirmDelete)
            {
                MessagingCenter.Send<Object>(sender, "deleteSelectedPin");
            }
            this.deletePin.IsVisible = false;
        }
    }
}