using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class RulaScore : Model
    {
        /// <summary>
        /// Rula Model
        /// </summary>
        public RulaScore()
        {
            this.table = _table;
            this.rulaScore = this;
        }
        public RulaScore CreateFromRulaObject(RulaObject rula)
        {

            int[,] chart = Helpers.getChart(Helpers.chart_type.arm_wrist);
            int y = Helpers.getY(rula.score_upper_arm.getTotal(), rula.score_lower_arm.getTotal());
            int x = Helpers.getX(rula.score_wrist_position.getTotal(), rula.score_wrist_twist.getTotal());



            if (Helpers.Check2DArray(y, x, chart))
            {
                this.posture_score_a = chart[y, x];
            }
            else return null;

            this.final_wrist_arm_score = this.posture_score_a + rula.score_arm_wrist_muscle.getTotal() + rula.score_arm_wrist_load.getTotal();
           

            chart = Helpers.getChart(Helpers.chart_type.trunk);
            y = Helpers.getYTrunk(rula.score_neck.getTotal());
            x = Helpers.getXTrunk(rula.score_trunk.getTotal(), rula.score_legs.getTotal());
            if (Helpers.Check2DArray(y, x, chart))
            {
                this.posture_score_b = chart[y, x];
            }
            else return null;
            this.final_neck_trunk_leg_score = posture_score_b + rula.score_neck_trunk_legs_muscle.getTotal() + rula.score_neck_trunk_legs_load.getTotal();
            this.final_score = Helpers.getChart(Helpers.chart_type.rula_final)[final_wrist_arm_score - 1, final_neck_trunk_leg_score - 1];
         
            return this.rulaScore;
        }
        private RulaScore rulaScore = null;
        public const string _table = "rula";
        public int id
        {
            get;
            set;
        }
        public int employee_id
        {
            get;
            set;
        }
        public int posture_score_a
        {
            get;
            set;
        }
        public int final_wrist_arm_score
        {
            get;
            set;
        }
        public int posture_score_b
        {
            get;
            set;
        }
        public int final_neck_trunk_leg_score
        {
            get;
            set;
        }
        public int final_score
        {
            get;
            set;
        }
    }
}
