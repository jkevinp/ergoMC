using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
using System.Data;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class RebaScore : Model
    {
        public RebaScore()
        {
            this.table = TABLE;
            this.rebaScore = this;
        }
        private RebaScore rebaScore = null;
        public const string TABLE = "reba";
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
        public string description
        {
            get;
            set;
        }
        public int posture_score_a { get; set; }
        public int final_score_a { get; set; }
        public int final_score_b { get; set; }

        public int final_score { get; set; }
        public int posture_score_b { get; set; }
        public int table_score_c { get; set; } //

        public void calculateAll(RebaObject _obj)
        {
            int[,] chart = Helpers.getRebaChart(Helpers.reba_chart_type.table_a);
            int neck = _obj.Score_neck.getTotal();
            int trunk = _obj.Score_trunk.getTotal();
            int leg = _obj.Score_legs.getTotal();

            int x = Helpers.getXTableA(trunk);
            int y = Helpers.getYTableA(neck, leg);
           this.posture_score_a = chart[x, y];

           this.final_score_a = posture_score_a + _obj.Score_neck_trunk_legs_load.getTotal();

           x = Helpers.getXTableB(_obj.Score_upper_arm.getTotal());
           y = Helpers.getYTableB(_obj.Score_lower_arm.getTotal(), _obj.Score_wrist_position.getTotal());

           this.posture_score_b = Helpers.getRebaChart(Helpers.reba_chart_type.table_b)[x, y];
           this.final_score_b = this.posture_score_b + _obj.Score_coupling.getTotal();

           this.table_score_c = Helpers.getRebaChart(Helpers.reba_chart_type.table_c)[final_score_a - 1, final_score_b - 1];

           this.final_score = table_score_c + _obj.Score_activity.getTotal() ;


        }


       
    }
}
