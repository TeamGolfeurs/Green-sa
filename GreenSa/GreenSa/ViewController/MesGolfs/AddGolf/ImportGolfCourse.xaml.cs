using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using GreenSa.Models.Tools.GPS_Maps;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using GreenSa.Models.GolfModel;
using GreenSa.Models.Tools;
using GreenSa.Persistence;
using System.Reflection;
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

            this.validPar.BorderColor = Color.FromHex("0C5E11");
            this.validPar.BorderWidth = 2;
            this.deletePin.BorderColor = Color.FromHex("0C5E11");
            this.deletePin.BorderWidth = 2;
            this.deleteAllPins.BorderColor = Color.FromHex("0C5E11");
            this.deleteAllPins.BorderWidth = 2;
            this.localizeCity.BorderColor = Color.FromHex("0C5E11");
            this.localizeCity.BorderWidth = 2;
            this.SetParVisibility(false);
            this.SetCourseNameVisibility(false);
            map.update();

            //Suscribe to get a notification about the pin that was added on the map by the user
            MessagingCenter.Subscribe<Pin>(this, "getAddGolfMapPins", (pin) => {
                this.pins.Add(pin);
                this.SetParVisibility(true);
                this.SetCourseNameVisibility(false);
            });
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
        private async void OnCreateCourseClick(object sender, EventArgs e)
        {

            if (this.pins.Count == 9 || this.pins.Count == 18)
            {
                var confirmDelete = await this.DisplayAlert("Création du parcours", "Voulez vous créer ce parcours ?", "Oui", "Non");
                if (confirmDelete)
                {
                    String xmlGolfCourse = this.CreateXmlGolfCourse();
                    this.InsertGolfCourseBdd(xmlGolfCourse);
                }
            }
            else
            {
                await this.DisplayAlert("Erreur", "Un parcours ne peut être que de 9 ou 18 trous", "Ok");
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
                    NinePinsCourseNameManagement();
                    this.SetParVisibility(false);
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
                    this.ManageAllPinsDelete();
                }
            }
            else
            {
                await this.DisplayAlert("Erreur de suppression", "Aucun trou à supprimer", "Ok");
            }
        }

        /** This function is called when the 'Valider' button is pressed
        */
        private void OnValidParClick(object sender, EventArgs e)
        {
            int par = Convert.ToInt32(this.golfParEntry.Text);
            this.pins[this.pins.Count - 1].Id = par;//As the pin ID isn't used, let's use it to store the par
            try
            {
                MessagingCenter.Send<Object>(par, "validPar");
            } catch (TargetInvocationException exception)
            {
                this.DisplayAlert("OnValidParClick", exception.InnerException.StackTrace, "ok");
            }
            this.SetParVisibility(false);
            if (this.pins.Count == 18)//if 18 holes on the map
            {
                this.SetCourseNameVisibility(true);
            }
            else
            {
                NinePinsCourseNameManagement();//check if 9 holes on the map
            }
        }

        /** Creates an xml string describing the golf course whose data are specified in this pane by the user
         * return this xml string
         */
        private String CreateXmlGolfCourse()
        {
            StringBuilder xmlGolfCourse = new StringBuilder("<GolfCourse>");
            xmlGolfCourse.Append("<Name>" + this.golfNameEntry.Text + "</Name>");
            xmlGolfCourse.Append("<NbTrous>" + this.pins.Count + "</NbTrous>");
            xmlGolfCourse.Append("<NomGolf>" + this.cityEntry.Text + "</NomGolf>");
            xmlGolfCourse.Append("<Coordinates>");
            foreach (Pin hole in this.pins)
            {
                xmlGolfCourse.Append("<Trou>");
                xmlGolfCourse.Append("<par>" + hole.Id + "</par>");//hole.Id is used to store the hole's par
                xmlGolfCourse.Append("<lat>" + hole.Position.Latitude + "</lat>");
                xmlGolfCourse.Append("<lng>" + hole.Position.Longitude + "</lng>");
                xmlGolfCourse.Append("</Trou>");
            }
            xmlGolfCourse.Append("</Coordinates>");
            xmlGolfCourse.Append("</GolfCourse>");
            return xmlGolfCourse.ToString();
        }

        /** Inserts a golf course in the database from an xml string describing the golf course
         * xmlGolfCourse : the xml string describing teh golf course
         */
        private void InsertGolfCourseBdd(String xmlGolfCourse)
        {
            GolfCourse gc;
            try
            {
                gc = GolfXMLReader.getSingleGolfCourseFromText(xmlGolfCourse);
                SQLite.SQLiteConnection connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                try
                {
                    connection.CreateTable<Hole>();
                    connection.CreateTable<MyPosition>();
                    connection.CreateTable<GolfCourse>();
                    connection.BeginTransaction();
                    SQLiteNetExtensions.Extensions.WriteOperations.InsertWithChildren(connection, gc, true);
                    connection.Commit();
                    this.DisplayAlert("Succès", "Le " + this.pins.Count + " trous : " + golfNameEntry.Text + " a été créé avec succès", "Continuer");
                    this.ManageAllPinsDelete();
                }
                catch (Exception bddException)
                {
                    this.DisplayAlert("Erreur avec la base de donnée", bddException.StackTrace, "Ok");
                    connection.Rollback();
                }
            }
            catch (Exception xmlConversionException)
            {
                this.DisplayAlert("Erreur lors de la conversion XML -> GolfCourse", xmlConversionException.StackTrace, "Ok");
            }
        }

        /** Manages the delete of all the pins currently placed on the map
         */
        private void ManageAllPinsDelete()
        {
            this.pins.Clear();
            MessagingCenter.Send<Object>(this, "deleteAllPins");
            this.SetCourseNameVisibility(false);
            this.SetParVisibility(false);
        }


        /** Set the visibility to the golf course name input area
         * isVisible : true is it has to be visible, false otherwise
         */
        private void SetCourseNameVisibility(Boolean isVisible)
        {
            golfNameLabel.IsVisible = isVisible;
            golfNameEntry.IsVisible = isVisible;
        }


        /** Set the visibility to the par validation input area
         * isVisible : true is it has to be visible, false otherwise
         */
        private void SetParVisibility(Boolean isVisible)
        {
            golfParLabel.Text = "Par du trou n°" + this.pins.Count + " :";
            golfParEntry.IsVisible = isVisible;
            golfParLabel.IsVisible = isVisible;
            validPar.IsVisible = isVisible;
        }

        /** Set visible the golf course name input area if nine pins are placed on the map, not visible otherwise
         */
        private void NinePinsCourseNameManagement()
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