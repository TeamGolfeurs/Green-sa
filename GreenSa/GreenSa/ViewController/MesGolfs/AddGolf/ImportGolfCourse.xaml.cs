using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using GreenSa.Models.Tools.GPS_Maps;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using System.Collections.Generic;
//using Xamarin.Forms.Maps;
//using Xamarin.Forms.GoogleMaps;

namespace GreenSa.ViewController.Option
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportGolfCourse : ContentPage
    {

        List<Pin> pins;//list of the current pins placed on the map

        public ImportGolfCourse()
        {
            InitializeComponent();
            this.pins = new List<Pin>();

            this.deletePin.BorderColor = Color.FromHex("0C5E11");
            this.deletePin.BorderWidth = 2;
            this.deleteAllPins.BorderColor = Color.FromHex("0C5E11");
            this.deleteAllPins.BorderWidth = 2;
            this.localizeCity.BorderColor = Color.FromHex("0C5E11");
            this.localizeCity.BorderWidth = 2;
            this.SetCourseNameVisibility(false);
            map.update();

            //Suscribe to get a notification about the pin that was added on the map by the user
            MessagingCenter.Subscribe<Pin>(this, "getAddGolfMapPins", (pin) => {
                this.pins.Add(pin);
                if (this.pins.Count == 18)
                {
                    this.SetCourseNameVisibility(true);
                } else
                {
                    ninePinsCourseNameManagement();
                }
                
            });
        }

        /** Set the visibility to the golf course name input area
         * isVisible : true is it has to be visible, false otherwise
         */
        private void SetCourseNameVisibility(Boolean isVisible)
        {
            golfNameLabel.IsVisible = isVisible;
            golfNameEntry.IsVisible = isVisible;
        }

        /** This function is called when the 'Localiser' button is pressed
         */
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

        /** This function is called when the 'Créer le parcours' button is pressed
         */
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

        /** This function is called when the 'Supprimer le dernier trou' button is pressed
         */
        private async void OnDeletePinClick(object sender, EventArgs e)
        {
            if (this.pins.Any())
            {
                var confirmDelete = await this.DisplayAlert("Suppression du dernier trou", "Voulez vous supprimer le trou n°" + this.pins.Count + " ?", "Oui", "Non");
                if (confirmDelete)
                {
                    this.pins.RemoveAt(this.pins.Count - 1);
                    MessagingCenter.Send<Object>(this, "deleteLastPin");
                    ninePinsCourseNameManagement();
                }
            } else
            {
                await this.DisplayAlert("Erreur de suppression", "Aucun trou à supprimer", "Ok");
            }
        }

        /** This function is called when the 'Supprimer tous les trous' button is pressed
         */
        private async void OnDeleteAllPinsClick(object sender, EventArgs e)
        {
            if (this.pins.Any())
            {
                var confirmDelete = await this.DisplayAlert("Suppression de tous les trous", "Voulez vous supprimer tous les trous ?", "Oui", "Non");
                if (confirmDelete)
                {
                    this.pins.Clear();
                    MessagingCenter.Send<Object>(this, "deleteAllPins");
                    this.SetCourseNameVisibility(false);
                }
            }
            else
            {
                await this.DisplayAlert("Erreur de suppression", "Aucun trou à supprimer", "Ok");
            }
        }

        /** Set visible the golf course name input area if nine pins are placed on the map, not visible otherwise
         */
        private void ninePinsCourseNameManagement()
        {
            if (this.pins.Count == 9)
            {
                this.SetCourseNameVisibility(true);
            }
            else
            {
                this.SetCourseNameVisibility(false);
            }
        }

    }
}