using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjectK.ErgoMC.Assessment.classes
{
    public class RebaObject
    {
        private List<IndexScore> _scores = new List<IndexScore>();

        private IndexScore score_neck = new IndexScore(1, 3, "score_neck" , "Neck Score");
        private IndexScore score_trunk = new IndexScore(1, 5, "score_trunk" , "Trunk Score");
        private IndexScore score_legs = new IndexScore(1, 4, "score_legs" , "Legs Score");
        private IndexScore score_neck_trunk_legs_load = new IndexScore(0, 3, "score_neck_trunk_legs_load" , "Neck, Trunk and Legs Load Score");
        private IndexScore score_upper_arm = new IndexScore(1, 6, "score_upper_arm" ,  "Upper Arm Score");
        private IndexScore score_wrist_position = new IndexScore(1, 3, "score_wrist_position" , "Wrist Position Score");
        private IndexScore score_lower_arm = new IndexScore(1, 2, "score_lower_arm" , "Lower Arm Score");
        private IndexScore score_coupling = new IndexScore(0, 3, "score_coupling" , "Coupling Score");
        private IndexScore score_activity= new IndexScore(0,1,"score_activitiy"  , "Activity Score");

        public IndexScore Score_neck {
            get { return this.score_neck; }
            set { this.score_neck = value;}
        }
        public IndexScore Score_trunk {
            get { return this.score_trunk; }
            set { this.score_trunk = value; }
        }
        public IndexScore Score_legs
        {
            get { return this.score_legs; }
            set { this.score_legs = value; }
        }
        public IndexScore Score_neck_trunk_legs_load {
            get { return this.score_neck_trunk_legs_load; }
            set { this.score_neck_trunk_legs_load = value; }
        }
        public IndexScore Score_upper_arm {
            get { return this.score_upper_arm; }
            set { this.score_upper_arm = value; }
        }
        public IndexScore Score_wrist_position{
            get { return this.score_wrist_position; }
            set { this.score_wrist_position = value; }
        }
        public IndexScore Score_lower_arm  {
            get { return this.score_lower_arm; }
            set { this.score_lower_arm = value; } 
        }
        public IndexScore Score_coupling  {
            get { return this.score_coupling; }
            set { this.score_coupling = value; }
        }
        public IndexScore Score_activity {
            get { return this.score_activity; }
            set { this.score_activity = value; }
        }
      

        public RebaObject()
        {
            init();
        }
        public List<IndexScore> getScoreList()
        {
            return this._scores;
        }
        public void init()
        {
            _scores.Add(score_neck);
            _scores.Add(score_trunk);
            _scores.Add(score_legs);
            _scores.Add(score_neck_trunk_legs_load);
            _scores.Add(score_upper_arm);
            _scores.Add(score_lower_arm);
            _scores.Add(score_wrist_position);
           
            _scores.Add(score_coupling);
            _scores.Add(score_activity);
        }
        public void Save()
        {

        }

    }
}
