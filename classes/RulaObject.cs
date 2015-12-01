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
        public IndexScore score_upper_arm = new IndexScore();
        public IndexScore score_lower_arm = new IndexScore();
        public IndexScore score_wrist_position = new IndexScore();
        public IndexScore score_wrist_twist = new IndexScore();
        public IndexScore score_arm_wrist_muscle = new IndexScore();
        public IndexScore score_arm_wrist_load = new IndexScore();
        public IndexScore score_neck = new IndexScore();
        public IndexScore score_trunk = new IndexScore();
        public IndexScore score_legs = new IndexScore();
        public IndexScore score_neck_trunk_legs_muscle = new IndexScore();
        public IndexScore score_neck_trunk_legs_load = new IndexScore();
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

    }
}
