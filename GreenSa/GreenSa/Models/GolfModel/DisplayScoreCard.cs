using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.GolfModel
{
    class DisplayScoreCard
    {
        public string number { get; set; }
        public string par { get; set; }
        public string score { get; set; }

        public DisplayScoreCard(int i, ScoreHole sh)
        {
            number = i.ToString();
            par = sh.Hole.Par.ToString();
            score = (sh.Score+ sh.Hole.Par).ToString();
        }

        public DisplayScoreCard(int i, int p)
        {
            number = i.ToString();
            par = p.ToString();
            score = "-";
        }

        public override string ToString()
        {
            return number.ToString() + " : " + par.ToString() + " : " + score.ToString();
        }
    }
}
