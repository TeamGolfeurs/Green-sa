using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenSa.Models.ViewElements
{
    public class MyButton : Button
    {
        public MyButton() : base()
        {
            this.BackgroundColor = Color.FromHex("0A7210");
            this.BorderColor = Color.FromHex("0C5E11");
            this.TextColor = Color.White;
            this.CornerRadius = 50;
            this.BorderWidth = 2;
        }
        public MyButton(Color bc, Double opacity) : base()
        {
            this.BackgroundColor = bc;
            this.Opacity = opacity;
            this.BorderColor = bc;
            this.TextColor = Color.White;
            this.CornerRadius = 50;
            this.BorderWidth = 2;
        }
    }
}
