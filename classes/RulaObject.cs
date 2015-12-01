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
        public IndexScore score_upper_arm = new IndexScore(1,6, "score_upper_arm");
        public IndexScore score_lower_arm = new IndexScore(1,3, "score_lower_arm");
        public IndexScore score_wrist_position = new IndexScore(1,4, "score_wrist_position");
        public IndexScore score_wrist_twist = new IndexScore(1,2, "score_wrist_twist");
        public IndexScore score_arm_wrist_muscle = new IndexScore(0,1,"score_arm_wrist_muscle");
        public IndexScore score_arm_wrist_load = new IndexScore(0, 3, "score_arm_wrist_load");
        public IndexScore score_neck = new IndexScore(1, 6, "score_neck");
        public IndexScore score_trunk = new IndexScore(1,6,"score_trunk");
        public IndexScore score_legs = new IndexScore(1,2, "score_legs");
        public IndexScore score_neck_trunk_legs_muscle = new IndexScore(0, 1, "score_neck_trunk_legs_muscle");
        public IndexScore score_neck_trunk_legs_load = new IndexScore(0,3, "score_neck_trunk_legs_load");
        private List<IndexScore> _scores = new List<IndexScore>();

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
