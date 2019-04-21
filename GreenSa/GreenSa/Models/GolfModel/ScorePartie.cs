﻿using GreenSa.Models.Tools;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class ScorePartie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<ScoreHole> scoreHoles { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        [Ignore]
        public String DateString
        {
            get
            {
                return  ((DateDebut.Day < 10) ? "0" : "") + DateDebut.Day + "/" + ((DateDebut.Month < 10) ? "0" : "") + DateDebut.Month + "/" + (DateDebut.Year-2000);
            }
        }

        [Ignore]
        public string GolfName { get; set; }

        public Tuple<int, int> GetScore()
        {
            int score = 0;
            foreach (ScoreHole sh in this.scoreHoles)
            {
                score += sh.Score;
            }
            return new Tuple<int, int>(score, this.scoreHoles.Count);
        }

        public ScorePartie()
        {
            scoreHoles = new List<ScoreHole>();
            DateDebut = DateTime.Now;
        }

        public ScorePartie(DateTime date)
        {
            scoreHoles = new List<ScoreHole>();
            DateDebut = date;
        }

        public void add(ScoreHole sh)
        {
            scoreHoles.Add(sh);
        }

        private string getMonthStr(int month)
        {
            String mont = "";
            switch (month)
            {
                case 1:
                    mont = "janvier";
                    break;
                case 2:
                    mont = "février";
                    break;
                case 3:
                    mont = "mars";
                    break;
                case 4:
                    mont = "avril";
                    break;
                case 5:
                    mont = "mai";
                    break;
                case 6:
                    mont = "juin";
                    break;
                case 7:
                    mont = "juillet";
                    break;
                case 8:
                    mont = "août";
                    break;
                case 9:
                    mont = "septembre";
                    break;
                case 10:
                    mont = "octobre";
                    break;
                case 11:
                    mont = "novembre";
                    break;
                case 12:
                    mont = "décembre";
                    break;
            }
            return mont;
        }
    }
}
