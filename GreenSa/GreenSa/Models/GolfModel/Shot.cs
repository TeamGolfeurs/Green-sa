﻿using GreenSa.Models.Tools;
using GreenSa.Models.Tools.GPS_Maps;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace GreenSa.Models.GolfModel
{
    public class Shot
    {

        public enum ShotCategory
        {
            PerfectShot,
            UnexpectedLongGoodShot,
            GoodShot,
            NotStraightShot,
            FailedShot,
            TolerableShot,
        }

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(foreignType: typeof(Club))]
        public String ClubId { get; set; }
        [OneToOne(CascadeOperations=CascadeOperation.CascadeRead)]
        public Club Club { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string InitPlaceId { get; set; }
        [OneToOne(foreignKey: "InitPlaceId", CascadeOperations = CascadeOperation.All)]
        public MyPosition InitPlace { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string TargetId { get; set; }
        [OneToOne(foreignKey: "TargetId", CascadeOperations = CascadeOperation.All)]
        public MyPosition Target { get; set; }

        [ForeignKey(typeof(MyPosition))]
        public string RealShotId { get; set; }
        [OneToOne(foreignKey:"RealShotId",CascadeOperations =CascadeOperation.All)]
        public MyPosition RealShot { get; set; }

        public DateTime Date { get; set; }

        public ShotCategory ShotType { get; set; }


        public double RealShotDist()
        {
            if (InitPlace == null) return 0;
            return CustomMap.DistanceTo(InitPlace.X,InitPlace.Y,RealShot.X,RealShot.Y,"M");
        }

        public double TargetDist()
        {
            if (InitPlace == null) return 0;
            return CustomMap.DistanceTo(InitPlace.X, InitPlace.Y, Target.X, Target.Y, "M");
        }

        public double RealShotTargetDist()
        {
            if (InitPlace == null) return 0;
            return CustomMap.DistanceTo(Target.X, Target.Y, RealShot.X, RealShot.Y, "M");
        }

        public Shot(Club currentClub, MyPosition initPlace,MyPosition target, MyPosition realShot, DateTime date )
        {
            this.Club = currentClub;
            this.InitPlace = initPlace;
            this.Target = target;
            this.RealShot = realShot;
            this.Date = date;
            this.ShotType = determineShotCategory();
        }
        
        public Shot()
        {

        }



        private ShotCategory determineShotCategory()
        {
            ShotCategory sc = ShotCategory.GoodShot;
            double index = StatistiquesGolf.getPlayerIndex();

            double psmd = this.getPerfectShotMaxDist(index);
            double gsmd = this.getGoodShotMaxDist(index);
            double ugsmdg = 10 + this.Club.DistanceMoyenne * 10 / 100;//unexpected good shot min dist gap
            double ssd = this.Club.DistanceMoyenne * 2 / 3;//short shot dist

            double rstd = this.RealShotTargetDist();
            double td = this.TargetDist();
            double rsd = this.RealShotDist();
            double shotAngle = this.GetShotAngle(rstd, td, rsd, true);


            if (rstd <= psmd)//if in the perfect shot circle
            {
                sc = ShotCategory.PerfectShot;
            } else if (rstd <= gsmd)//if in the good shot circle
            {
                sc = ShotCategory.GoodShot;
            } else if (rsd - td > ugsmdg && shotAngle < 7)//if player shot much further than he expected and with a small dispertion
            {
                sc = ShotCategory.UnexpectedLongGoodShot;
            } else//bad shot
            {
                if (rsd < ssd)//if player failed his shot and his ball traveled a short distance
                {
                    sc = ShotCategory.FailedShot;
                } else if (shotAngle > 10)//if player didn't shot straigth
                {
                    sc = ShotCategory.NotStraightShot;
                } else
                {
                    sc = ShotCategory.TolerableShot;
                }
            }
            System.Diagnostics.Debug.WriteLine("\n Category : " + sc + "\n Index : " + index + "\n ugsmdg : " + ugsmdg + "\n DistMoyClub : " + Club.DistanceMoyenne + "\n rstd : " + rstd + "\n td : " + td + "\n rsd : " + rsd + "\n shotAngle : " + shotAngle + "\n psmd : " + psmd + "\n gsmd : " + gsmd);
            return sc;
        }

        /** Gets the shot angle 
         * rstd : RealShotTargetDist
         * td : TargetDist
         * rsd : RealShotDist
         * degree : true to get the angle in degrees
         */
        public double GetShotAngle(double rstd, double td, double rsd, Boolean degree = false)
        {
            double cosAngle = (-rstd * rstd + td * td + rsd * rsd) / (2.0 * td * rsd);
            double angle = Math.Acos(cosAngle);
            if (degree)//degree
            {
                angle = angle * 180 / Math.PI;
            }
            return angle;
        }

        /** Computes the distance maximal to classify a shot as a good (tolerable) one
         */
        private double getGoodShotMaxDist(double index)
        {
            double psmd = 0.0;
            double ratio = 0.0;
            if (index < 10.0) {
                ratio = 15.0 / 100;
            } else if (index < 20.0) {
                ratio = 16.0 / 100;
            } else if (index < 30.0) {
                ratio = 17.0 / 100;
            } else if (index < 40.0) {
                ratio = 18.0 / 100;
            } else {
                ratio = 20.0 / 100;
            }
            System.Diagnostics.Debug.WriteLine("\nRatio : " + ratio);
            psmd = this.Club.DistanceMoyenne * ratio;
            return psmd;
        }

        /** Computes the distance maximal to classify a shot as a perfect one
         */
        private double getPerfectShotMaxDist(double index)
        {
            double psmd = 0.0;
            double ratio = 0.0;
            if (index < 10.0) {
                ratio = 4.0 / 100;
            } else if (index < 20.0) {
                ratio = 5.0 / 100;
            } else if (index < 30.0) {
                ratio = 6.0 / 100;
            } else if (index < 40.0) {
                ratio = 7.0 / 100;
            } else {
                ratio = 8.0 / 100;
            }
            System.Diagnostics.Debug.WriteLine("\nRatio : " + ratio);
            psmd = this.Club.DistanceMoyenne * ratio;
            return psmd;
        }

        public override string ToString()
        {
            return "From "+InitPlace+", try "+Target+" but "+RealShot +" with "+Club+" the "+Date.DayOfWeek+" "+Date.Day+" "+Date.Month;
        }
        
    }
}