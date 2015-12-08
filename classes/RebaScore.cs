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
        public int table_score_c { get; set; }
        public double unixtime { get; set; }
        public bool isDone
        {
            get;
            set;
        }
        public DateTime DateTimeEvaluated
        {
            get { return Helpers.ConvertFromUnixTimestamp(this.unixtime); }
            set { this.unixtime = Helpers.ConvertToUnixTimestamp(value); }
        }
        public bool canTakeTest
        {
            get { return !this.isDone; }
        }
        public RebaScore calculateAll(RebaObject _obj)
        {
            int[,] chart = Helpers.getRebaChart(Helpers.reba_chart_type.table_a);
            int neck = _obj.Score_neck.getTotal();
            int trunk = _obj.Score_trunk.getTotal();
            int leg = _obj.Score_legs.getTotal();

            int x = Helpers.getXTableA(trunk);
            int y = Helpers.getYTableA(neck, leg);
            if (Helpers.Check2DArray(x, y, chart))this.posture_score_a = chart[x, y];
            else return null;
            

           this.final_score_a = posture_score_a + _obj.Score_neck_trunk_legs_load.getTotal();

           chart = Helpers.getRebaChart(Helpers.reba_chart_type.table_b);

           x = Helpers.getXTableB(_obj.Score_upper_arm.getTotal());
           y = Helpers.getYTableB(_obj.Score_lower_arm.getTotal(), _obj.Score_wrist_position.getTotal());
           if (Helpers.Check2DArray(x, y, chart))this.posture_score_b = chart[x, y];
           else return null;

           //this.posture_score_b = Helpers.getRebaChart(Helpers.reba_chart_type.table_b)[x, y];
           this.final_score_b = this.posture_score_b + _obj.Score_coupling.getTotal();

            chart = Helpers.getRebaChart(Helpers.reba_chart_type.table_c);
           if (Helpers.Check2DArray(x, y, chart))this.table_score_c = chart[final_score_a - 1, final_score_b - 1];
           else return null;

           //this.table_score_c = Helpers.getRebaChart(Helpers.reba_chart_type.table_c)[final_score_a - 1, final_score_b - 1];

           this.final_score = table_score_c + _obj.Score_activity.getTotal() ;

           switch (final_score)
           {
               case 1:
                   this.description = "Neglible risk, no action required.";
                   break;
               case 2:
               case 3:
                   this.description = "Low risk, change may be needed.";
                   break;
               case 4:
               case 5:
               case 6:
               case 7:
                   this.description = "Medium risk,further investigation and change soon.";
               break;
               case 8:
               case 9:
               case 10:
               this.description = "High risk,investigate and implement change.";
               break;
               default:
                   this.description = "Very high risk, implement change.";
                   break;
           }
        

            return this.rebaScore;

        }

        public void Get(Employee emp)
        {
            DataTable t = this.selectQuery("SELECT * FROM `" + table + "` where `employee_id`='" + this.employee_id + "' LIMIT 1");
            if (t.Rows.Count > 0) { 
                emp.Reba_score.posture_score_a = Helpers.Convert(t.Rows[0]["posture_score_a"].ToString());
                emp.Reba_score.posture_score_b = Helpers.Convert(t.Rows[0]["posture_score_b"].ToString());
                emp.Reba_score.id = Helpers.Convert(t.Rows[0]["id"].ToString());
                emp.Reba_score.final_score_b = Helpers.Convert(t.Rows[0]["final_score_b"].ToString());
                emp.Reba_score.final_score = Helpers.Convert(t.Rows[0]["final_score"].ToString());
                emp.Reba_score.final_score_a = Helpers.Convert(t.Rows[0]["final_score_a"].ToString());
                emp.Reba_score.employee_id = Helpers.Convert(t.Rows[0]["employee_id"].ToString());
                emp.Reba_score.table_score_c = Helpers.Convert(t.Rows[0]["table_score_c"].ToString());
                emp.Reba_score.description = t.Rows[0]["description"].ToString();
                emp.Reba_score.isDone = true;
                emp.Reba_score.unixtime = (double)Helpers.Convert(t.Rows[0]["unix_time"].ToString());
            }
        }

        public int Save(bool is_unique)
        {
            // var result = this.insert("INSERT INTO `" + table + "` (employee_id,rula_id,score_name,score_value,score_additional,score_total) values('" + this.employee_id +"' ,'0' ,'" + score +"','" + +"','" + +"','" + +"')");

            var result = this.insert("INSERT INTO `" + table + "` (employee_id,posture_score_a,final_score_a,posture_score_b,final_score_b,table_score_c,final_score,description,user_id) values('" + this.employee_id + "' ,'" + posture_score_a + "' ,'" + final_score_a + "','" + posture_score_b + "','" + final_score_b + "','" + table_score_c +  "','" + final_score + "','" + description + "',"  + Session.user.Id  + ")");
            return result;
        }
       
    }
}
