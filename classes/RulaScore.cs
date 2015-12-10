using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
using System.Data;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class RulaScore : Model
    {
        /// <summary>
        /// Rula Model
        /// </summary>
        public RulaScore()
        {
            this.table = TABLE;
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
            int tempy = final_neck_trunk_leg_score;
            int tempx = final_wrist_arm_score;
            chart = Helpers.getChart(Helpers.chart_type.rula_final);

            if ((final_wrist_arm_score - 1) >= chart.GetLength(0))
            {
                tempx = chart.GetLength(0) -1;

            }
            int xx = chart.GetLength(0);

            if ((final_neck_trunk_leg_score - 1) >= chart.GetLength(1))
            {
                tempy = chart.GetLength(1) -1;

            }
           
            this.final_score = chart[tempx, tempy];

            switch (final_score)
            {
                case 1:
                case 2:
                    this.description = "Acceptable";
                    break;
                case 3:
                case 4:
                    this.description = "Investigate further.";
                    break;
                case 5:
                case 6:
                    this.description = "Investigate further and change soon.";
                    break;
                default:
                    this.description = "Investigate and change immediately";
                    break;
            }


            return this.rulaScore;
        }
        private RulaScore rulaScore = null;
        public const string TABLE = "rula";
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
        public string description
        {
            get;
            set;
        }
        public int user_id { get; set; }
        public double unixtime
        {
            get;
            set;
        }
        public bool isDone
        {
            get;
            set;
        }
        public DateTime DateTimeEvaluated
        {
            get
            {
                if (this.unixtime == 0)
                {
                    this.unixtime = Helpers.ConvertToUnixTimestamp(DateTime.Now);
                    return DateTime.Now;
                }
                return Helpers.ConvertFromUnixTimestamp(this.unixtime);
            }
            set { this.unixtime = Helpers.ConvertToUnixTimestamp(value); }
        }
        public string DateStringEvaluated
        {
            get
            {
                if (this.unixtime == 0)
                {
                    this.unixtime = Helpers.ConvertToUnixTimestamp(DateTime.Now);
                    return DateTime.Now.ToShortDateString();
                }
                return Helpers.ConvertFromUnixTimestamp(this.unixtime).ToShortDateString();
            }
        }

        public bool canTakeTest
        {
            get { return !this.isDone; }
        }
        public string evaluator_name { get; set; }
        public void Get(Employee emp)
        {
            DataTable t = this.selectQuery("SELECT * FROM `" + table + "` where `employee_id`='" + this.employee_id + "' LIMIT 1");
            if (t.Rows.Count != 0)
            {
                emp.Rula_score.posture_score_a = Helpers.Convert(t.Rows[0]["posture_score_a"].ToString());
                emp.Rula_score.posture_score_b = Helpers.Convert(t.Rows[0]["posture_score_b"].ToString());
                emp.Rula_score.id = Helpers.Convert(t.Rows[0]["id"].ToString());
                emp.Rula_score.final_wrist_arm_score = Helpers.Convert(t.Rows[0]["final_wrist_arm_score"].ToString());
                emp.Rula_score.final_score = Helpers.Convert(t.Rows[0]["final_score"].ToString());
                emp.Rula_score.final_neck_trunk_leg_score = Helpers.Convert(t.Rows[0]["final_neck_trunk_leg_score"].ToString());
                emp.Rula_score.employee_id = Helpers.Convert(t.Rows[0]["employee_id"].ToString());
                emp.Rula_score.description = t.Rows[0]["description"].ToString();
                emp.Rula_score.unixtime = (double)Helpers.Convert(t.Rows[0]["unix_time"].ToString());
                emp.Rula_score.isDone = true;
                emp.Rula_score.user_id = Helpers.Convert(t.Rows[0]["user_id"].ToString());
                DataTable x = this.selectQuery("SELECT * FROM user where id=" + emp.Rula_score.user_id);
                if (x.Rows.Count > 0)emp.Rula_score.evaluator_name = x.Rows[0]["firstname"].ToString()  + " " + x.Rows[0]["lastname"].ToString();
                
            }
         
        }
        public int Save(bool is_unique)
        {
            this.user_id = Session.user.Id;
            var result = this.insert("INSERT INTO `" + table + "` (employee_id,posture_score_a,final_wrist_arm_score,posture_score_b,final_neck_trunk_leg_score,final_score,description,user_id,unix_time) values('" + this.employee_id + "' ,'" + posture_score_a + "' ,'" + final_wrist_arm_score + "','" + posture_score_b + "','" + final_neck_trunk_leg_score + "','" + final_score + "','" + description + "',"  + this.user_id   + "," + this.unixtime +")");
            return result;
        }
      
    }
}
