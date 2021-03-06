﻿using System;
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
        public RebaScore(string _type)
        {
            this.table = TABLE;
            this.Type = _type;
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
        public int user_id { get; set; }
        private string type = string.Empty;
        public string Type
        {
            get
            {
                return this.type;
            }
            set { this.type = value; }
        }
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
        public string evaluator_name { get; set; }
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


        
        public void Get(Employee emp , string _type)
        {
            DataTable t = this.selectQuery("SELECT * FROM `" + table + "` where `employee_id`='" + this.employee_id + "' AND `type`='" + _type + "'");
            if (t.Rows.Count > 0)
            {
                if (_type == "left")
                {
                    emp.LeftReba_score.posture_score_a = Helpers.Convert(t.Rows[0]["posture_score_a"].ToString());
                    emp.LeftReba_score.posture_score_b = Helpers.Convert(t.Rows[0]["posture_score_b"].ToString());
                    emp.LeftReba_score.id = Helpers.Convert(t.Rows[0]["id"].ToString());
                    emp.LeftReba_score.final_score_b = Helpers.Convert(t.Rows[0]["final_score_b"].ToString());
                    emp.LeftReba_score.final_score = Helpers.Convert(t.Rows[0]["final_score"].ToString());
                    emp.LeftReba_score.final_score_a = Helpers.Convert(t.Rows[0]["final_score_a"].ToString());
                    emp.LeftReba_score.employee_id = Helpers.Convert(t.Rows[0]["employee_id"].ToString());
                    emp.LeftReba_score.table_score_c = Helpers.Convert(t.Rows[0]["table_score_c"].ToString());
                    emp.LeftReba_score.description = t.Rows[0]["description"].ToString();
                    emp.LeftReba_score.isDone = true;
                    emp.LeftReba_score.Type = t.Rows[0]["type"].ToString();
                    emp.LeftReba_score.unixtime = (double)Helpers.Convert(t.Rows[0]["unix_time"].ToString());
                    emp.LeftReba_score.isDone = true;
                    emp.LeftReba_score.user_id = Helpers.Convert(t.Rows[0]["user_id"].ToString());
                    DataTable x = this.selectQuery("SELECT * FROM user where id=" + emp.LeftReba_score.user_id);
                    if (x.Rows.Count > 0) emp.LeftReba_score.evaluator_name = x.Rows[0]["firstname"].ToString() + " " + x.Rows[0]["lastname"].ToString();

                }
                else
                {
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
                    emp.Reba_score.Type = t.Rows[0]["type"].ToString();
                    emp.Reba_score.unixtime = (double)Helpers.Convert(t.Rows[0]["unix_time"].ToString());
                    emp.Reba_score.isDone = true;
                    emp.Reba_score.user_id = Helpers.Convert(t.Rows[0]["user_id"].ToString());
                    DataTable x = this.selectQuery("SELECT * FROM user where id=" + emp.Reba_score.user_id);
                    if (x.Rows.Count > 0) emp.Reba_score.evaluator_name = x.Rows[0]["firstname"].ToString() + " " + x.Rows[0]["lastname"].ToString();

                }
               

            }
        }


        public int Save(bool is_unique)
        {
            this.user_id = Session.user.Id;
            var result = this.insert("INSERT INTO `" + table + "` (employee_id,posture_score_a,final_score_a,posture_score_b,final_score_b,table_score_c,final_score,description,user_id,unix_time,type) values('" + this.employee_id + "' ,'" + posture_score_a + "' ,'" + final_score_a + "','" + posture_score_b + "','" + final_score_b + "','" + table_score_c + "','" + final_score + "','" + description + "'," + this.user_id + "," + this.unixtime + ",'" + this.Type + "')");
            return result;
        }
       
    }
}
