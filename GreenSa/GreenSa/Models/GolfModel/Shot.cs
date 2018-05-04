using GreenSa.Models.Tools;
using SQLiteNetExtensions.Attributes;
using System;

namespace GreenSa.Models.GolfModel
{
    public class Shot
    {
        [ForeignKey(typeof(Club))]
        public Club Club { get;  set; }
        [ForeignKey(typeof(MyPosition))]
        public MyPosition InitPlace { get; set; }
        [ForeignKey(typeof(MyPosition))]
        public MyPosition Target { get; set; }
        [ForeignKey(typeof(MyPosition))]
        public MyPosition RealShot { get; set; }
        public DateTime Date { get; set; }


        public Club getClubDuShot()
        {
            return Club;
        }

        public double getDistance()
        {
            var X = RealShot.X - InitPlace.X;
            var Y = RealShot.Y - InitPlace.Y;
            return (Math.Sqrt((X * X + Y * Y)));
        }

        public Shot(Club currentClub, MyPosition initPlace,MyPosition target, MyPosition realShot, DateTime date )
        {
            this.Club = currentClub;
            this.InitPlace = initPlace;
            this.Target = target;
            this.RealShot = realShot;
            this.Date = date;
        }

        public Shot()
        {

        }
    }
}