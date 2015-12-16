using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjectK.ErgoMC.Assessment.classes
{
    public class RulaObject
    {
        public IndexScore score_upper_arm = new IndexScore(1, 6, "score_upper_arm", "Upper Arm Score");
        public IndexScore score_lower_arm = new IndexScore(1,3, "score_lower_arm" , "Lower Arm Score");
        public IndexScore score_wrist_position = new IndexScore(1,4, "score_wrist_position" , "Wrist Position Score");
        public IndexScore score_wrist_twist = new IndexScore(1,2, "score_wrist_twist" , "Wrist Twist Score");
        public IndexScore score_arm_wrist_muscle = new IndexScore(0,1,"score_arm_wrist_muscle" , "Arm and Wrist Muscle Use Score");
        public IndexScore score_arm_wrist_load = new IndexScore(0, 3, "score_arm_wrist_load" , "Arm and wrist Load Score");
        public IndexScore score_neck = new IndexScore(1, 6, "score_neck" , "Neck Score");
        public IndexScore score_trunk = new IndexScore(1,6,"score_trunk" , "Trunk Score");
        public IndexScore score_legs = new IndexScore(1,2, "score_legs" , "Legs Score");
        public IndexScore score_neck_trunk_legs_muscle = new IndexScore(0, 1, "score_neck_trunk_legs_muscle" , "Neck, Trunk and Legs Muscle Use Score");
        public IndexScore score_neck_trunk_legs_load = new IndexScore(0,3, "score_neck_trunk_legs_load", "Neck, Trunk and Leg Load Score");
       
       private List<IndexScore> _scores = new List<IndexScore>();

       public IndexScore Score_upper_arm
       {
           get
           {
               return this.score_upper_arm;
           }
           set { this.score_upper_arm = value; }
       }
       public IndexScore Score_lower_arm
       {
           get
           {
               return this.score_lower_arm;
           }
           set
           {
               this.score_lower_arm = value;
           }
       }
        public IndexScore Score_wrist_position {
            get { return this.score_wrist_position; }
            set { this.score_wrist_position = value; }

        }
        public IndexScore Score_wrist_twist {
            get {
            return this.score_wrist_twist;
        }
            set { this.score_wrist_twist = value; }
        }
        public IndexScore Score_arm_wrist_muscle { get { return this.score_arm_wrist_muscle; } set { this.score_arm_wrist_muscle = value; } }
        public IndexScore Score_arm_wrist_load { get { return this.score_arm_wrist_load; } set { this.score_arm_wrist_load = value; } }
        public IndexScore Score_neck
        {
            get
            {
                return this.score_neck;
            }
            set
            {
                this.score_neck = value;
            }
        }
        public IndexScore Score_trunk
        {
            get
            {
                return this.score_trunk;
            }
            set
            {
                this.score_trunk = value;
            }
        }
        public IndexScore Score_legs
        {
            get
            {
                return this.score_legs;
            }
            set
            {
                this.score_legs = value;
            }
        }
        public IndexScore Score_neck_trunk_legs_muscle
        {
            get
            {
                return this.score_neck_trunk_legs_muscle;
            }
            set
            {
                this.score_neck_trunk_legs_muscle = value;
            }
        }
        public IndexScore Score_neck_trunk_legs_load
        {
            get
            {
                return this.score_neck_trunk_legs_load;
            }
            set
            {
                this.score_neck_trunk_legs_load = value;
            }
        }
       

       




        public RulaObject()
        {
            init();
        }
        public List<IndexScore> getScoreList()
        {
            return this._scores;
        }
        public void init()
        {
            _scores.Add(score_upper_arm);
            _scores.Add(score_lower_arm);
            _scores.Add(score_wrist_position);
            _scores.Add(score_wrist_twist);
            _scores.Add(score_arm_wrist_muscle);
            _scores.Add(score_arm_wrist_load);
            _scores.Add(score_neck);
            _scores.Add(score_trunk);
            _scores.Add(score_legs);
            _scores.Add(score_neck_trunk_legs_muscle);
            _scores.Add(score_neck_trunk_legs_load);
        }
        public void Save()
        {

        }


    }
}
