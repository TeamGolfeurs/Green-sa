using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenSa.Models.Tools.Services
{
    public class WindInfo 
    {

        //force du vent
        public int strength;

        //le type sera peut-être à changer
        //il s'agit de l'image associé au sens du vent
        public ImageSource icon;

        public WindInfo(int strength, ImageSource icon)
        {
            this.strength = strength;
            this.icon = icon;
        }

     
    }
}
