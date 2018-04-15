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

        public Shot()
        {

        }

        public Shot(Club currentClub, MyPosition initPlace,MyPosition target, MyPosition realShot, DateTime date )
        {
            this.Club = currentClub;
            this.InitPlace = initPlace;
            this.Target = target;
            this.RealShot = realShot;
            this.Date = date;
        }

    }
}