using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using ProjectK.ErgoMC.Assessment.Library;
namespace ProjectK.ErgoMC.Assessment.classes
{

    public class IndexScore: Model, INotifyPropertyChanged
    //: INotifyPropertyChanged
    {
        /// <summary>
        /// Initialize a New Index Score
        /// </summary>
        public IndexScore()
        {
            this.table = _table;
        }
        /// <summary>
        /// Initialize a New Index Score
        /// </summary>
        /// <param name="_min">Minimum Value</param>
        /// <param name="_max">Maximum Value</param>
        public IndexScore(int _min, int _max)
        {
            this.min = _min;
            this.max = _max;
            this.error_message = "Please select a number between " + this.min + " to " + this.max;
            this.table = _table;
        }

        private string _table = "rulascore";

        public void ChangeTable(string _table)
        {
            this.table = _table;
        }

        /// <summary>
        /// Initialize a New Index Score
        /// </summary>
        /// <param name="_min">Minimum Value</param>
        /// <param name="_max">Maximum Value</param>
        /// <param name="_name">Name to be inserted at the database</param>
        public IndexScore(int _min, int _max , string _name)
        {
            this.name = _name;
            this.min = _min;
            this.max = _max;
            this.error_message = "Please select a number between " + this.min + " to " + this.max + " for "  + name.Replace('_' , ' ');
            this.table = _table;
        }
        public string error_message = "Please enter a valid value.";
        public int min = 0;
        public int max = 0;
        public int id = 0;
        public string name = string.Empty;
        public int employee_id = 0;

        public int score = 0;
        public int additionalScore = 0;

        public int Score
        {
            get{ return this.score; }
            set{ this.score= value; } 
        }
        public int AdditionalScore
        {
            get { return this.additionalScore; }
            set { this.additionalScore = value; }
        }

        public int SetAscore(int x) { this.additionalScore = x; return this.additionalScore; }
        public int GetAscore() { return this.additionalScore; }
        public int getTotal() { return (this.additionalScore + score); }
        public int getScore() { return this.score; }
        public int SetScore(int x) {score = x; return this.score;}
        public event PropertyChangedEventHandler PropertyChanged;

        public int SaveRula()
        {
            var result = this.insert("INSERT INTO `" + this.table + "` (employee_id,rula_id,score_name,score_value,score_additional,score_total) values('" + this.employee_id + "' ,' " + this.id + "' ,'" + this.name + "','" + this.getScore() + "','" + this.GetAscore() + "','" + this.getTotal() + "')");
            return result;
        }
        public int SaveReba()
        {
            var result = this.insert("INSERT INTO `" + this.table + "` (employee_id,reba_id,score_name,score_value,score_additional,score_total) values('" + this.employee_id + "' ,' " + this.id + "' ,'" + this.name + "','" + this.getScore() + "','" + this.GetAscore() + "','" + this.getTotal() + "')");
            return result;
        }
      

        public bool validate()
        {
            int _total = this.getTotal();
            if (this.getTotal() >= this.min && this.getTotal()<= max)
            {
                return true;
            }
            return false;
        }

    }
}
