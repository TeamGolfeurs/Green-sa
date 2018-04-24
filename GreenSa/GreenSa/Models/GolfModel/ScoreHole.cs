using GreenSa.Models.Tools;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    public class ScoreHole
    {
        [PrimaryKey]
        int Id { get; set; }
        [ForeignKey(typeof(MyPosition))]
        private String IdHole { get; set; }
        [OneToOne]
        private Hole Hole { get; set; }
        private int Score { get; set; }

        public ScoreHole()
        {

        }

        public ScoreHole(Hole hole, int score)
        {
            this.Hole = hole;
            this.Score = score;
        }
      

    }
}
