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
        public int Id { get; set; }
        [ForeignKey(typeof(MyPosition))]
        public String IdHole { get; set; }
        [OneToOne]
        public Hole Hole { get; set; }
        public int Score { get; set; }

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
