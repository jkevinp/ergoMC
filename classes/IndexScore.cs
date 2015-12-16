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
using System.Data;
namespace ProjectK.ErgoMC.Assessment.classes
{

    public class IndexScore: Model, INotifyPropertyChanged
    {
        private string _table = "rulascore";

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
        public IndexScore(int _min, int _max, string _name, string _displayname)//, int _maxAscore
        {
            this.name = _name;
            this.min = _min;
            this.max = _max;
          //  this.MaxAScore = _maxAscore;
           // this.MaxScore = this.max - this.MaxAScore;
            this.DisplayName = _displayname;
            this.error_message = "Please select a number between " + this.min + " to " + this.max + " for "  + this.DisplayName;
            this.table = _table;
        }
        public List<IndexScoreChoice> currentAdditionalChoices = new List<IndexScoreChoice>();
        public event PropertyChangedEventHandler PropertyChanged; 
        public string error_message = "Please enter a valid value.";
        public int min = 0;
        public int max = 0;
        public int id = 0;
        public int MaxScore = 0;
        public int MaxAScore = 0;
        public string name = string.Empty;
        public int employee_id = 0;
        public int score = 0;
        public int additionalScore = 0;
        public int total = 0;
        private string displayName = string.Empty;
        private string score_description = string.Empty;
        private string additionalScore_description = string.Empty;

        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }
        public string ScoreDescription
        {
            get { return this.score_description; }
            set { this.score_description = value; }
        }
        public string AdditionalScoreDescription
        {
            get { return this.additionalScore_description; }
            set { this.additionalScore_description = value; }
        }
        public int Score
        {
            get{ return this.score; }
            set { 
                this.score = value; 
                OnPropertyChanged("Score");
                this.Total = this.score + this.additionalScore;
            } 
        }
        public int AdditionalScore
        {
            get { return this.additionalScore; }
            set { 
                this.additionalScore = value; 
                OnPropertyChanged("AdditionalScore");
                this.Total = this.score + this.additionalScore;
            }
        }
      

        public int Total
        {
            get { return this.total; }
            set
            {
                this.total = value;
                OnPropertyChanged("Total");
            }
        }

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public int SetAscore(int x) { this.additionalScore = x; return this.additionalScore; }
        public int GetAscore() { return this.additionalScore; }
        public int getTotal() { return (this.additionalScore + score); }
        public int getScore() { return this.score; }
        public int SetScore(int x) {score = x; return this.score;}
        
        public int SaveRula()
        {
            var result = this.insert("INSERT INTO `" + this.table + "` (employee_id,rula_id,score_name,score_value,score_additional,score_total,score_displayname,score_description,score_additional_description) values('" + this.employee_id + "' ,' " + this.id + "' ,'" + this.name + "','" + this.getScore() + "','" + this.GetAscore() + "','" + this.getTotal() + "','" + this.DisplayName + "','" + this.ScoreDescription + "','" + this.AdditionalScoreDescription + "')");
            return result;
        }
        public int SaveReba()
        {
            var result = this.insert("INSERT INTO `" + this.table + "` (employee_id,reba_id,score_name,score_value,score_additional,score_total,score_displayname,score_description,score_additional_description) values('" + this.employee_id + "' ,' " + this.id + "' ,'" + this.name + "','" + this.getScore() + "','" + this.GetAscore() + "','" + this.getTotal() + "','" + this.DisplayName + "','" + this.ScoreDescription + "','" + this.AdditionalScoreDescription + "')");
            return result;
        }
        public List<IndexScore> GetReba(int _empID, int _id)
        {
            List<IndexScore> _scores = new List<IndexScore>();
            this.ChangeTable("rebascore");
            DataTable t = this.selectQuery("SELECT * from " + this.table + " where reba_id='" + _id + "'");
            foreach (DataRow row in t.Rows)
            {
                IndexScore _score = new IndexScore();
                _score.name = row["score_name"].ToString();
                _score.Score = Helpers.Convert(row["score_value"].ToString());
                _score.AdditionalScore = Helpers.Convert(row["score_additional"].ToString());
                _score.DisplayName = row["score_displayname"].ToString();
                _score.ScoreDescription = row["score_description"].ToString();
                _score.AdditionalScoreDescription = row["score_additional_description"].ToString();
                _scores.Add(_score);
            }
            return _scores;
        }
        public List<IndexScore> GetRula(int _empID, int _id)
        {
            List<IndexScore> _scores = new List<IndexScore>();
            this.ChangeTable("rulascore");
            DataTable t = this.selectQuery("SELECT * from " + this.table + " where rula_id='" + _id + "'");
            foreach (DataRow row in t.Rows)
            {
                IndexScore _score = new IndexScore();
                _score.name = row["score_name"].ToString();
                _score.Score = Helpers.Convert(row["score_value"].ToString());
                _score.AdditionalScore = Helpers.Convert(row["score_additional"].ToString());
                _score.DisplayName = row["score_displayname"].ToString();
                _score.ScoreDescription = row["score_description"].ToString();
                _score.AdditionalScoreDescription = row["score_additional_description"].ToString();
                _scores.Add(_score);
            }
            return _scores;
        }
        public bool validate()
        {
            int _total = this.getTotal();
            if ((this.getTotal() >= this.min && this.getTotal() <= max)) // && (this.Score <= this.MaxScore) && this.AdditionalScore <= this.MaxAScore
            {
                return true;
            }
            return false;
        }

       

    }
}
