using GreenSa.Models.Tools;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace GreenSa.Models.GolfModel
{
    public class Shot
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(foreignType: typeof(Club))]
        public String ClubId { get; set; }
        [OneToOne]
        public Club Club { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public int InitPlaceId { get; set; }
        [OneToOne(foreignKey: "InitPlaceId", CascadeOperations = CascadeOperation.All)]
        public MyPosition InitPlace { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public int TargetId { get; set; }
        [OneToOne(foreignKey: "TargetId", CascadeOperations = CascadeOperation.All)]
        public MyPosition Target { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public int RealShotId { get; set; }
        [OneToOne(foreignKey:"RealShotId",CascadeOperations =CascadeOperation.All)]
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

        public override string ToString()
        {
            return "From "+InitPlace+", try "+Target+" but "+RealShot +" with "+Club+" the "+Date.DayOfWeek+" "+Date.Day+" "+Date.Month;
        }

    }
}