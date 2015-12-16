using ProjectK.ErgoMC.Assessment.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectK.ErgoMC.Assessment.classes
{
    public class IndexScoreChoice: Model
    {
        public const string TYPE_REBA = "reba";
        public const string TYPE_RULA = "rula";
        public const string _table = "choices";

        private int ref_id = 0;
        public int Ref_id
        {
            get
            {
               return this.ref_id;
            }
            set
            {
                this.ref_id = value;
            }
        }
        public const string RULA_PATH = "";
        public const string REBA_PATH = "";
        public IndexScoreChoice()
        {
            this.table = _table;
        }
        private string id = string.Empty;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public  string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public IndexScoreChoice(int _sv, string _c)
        {
            this.Content = _c;
            this.ScoreValue = _sv;
            this.table = _table;
        }
       
        public IndexScoreChoice(int _sv, string _c, string _type)
        {
            this.Content = _c;
            this.ScoreValue = _sv;
            this.Type = _type;
            this.table = _table;
        }
        
        private string imagePath = string.Empty;
        private int scoreValue = 0;
        private string content = string.Empty;

        private string type = "";
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public int ScoreValue
        {
            get { return this.scoreValue; }
            set { this.scoreValue = value; }
        }


        public List<IndexScoreChoice> GetScores(string _type , string _id)
        {
            List<IndexScoreChoice> s = new List<IndexScoreChoice>();

            DataTable t = this.selectQuery("SELECT * from `choices` where `ref_id`='" +_id +  "' and `ref_type`='" + _type +"'");
            foreach (DataRow row in t.Rows)
            {
                IndexScoreChoice isc = new IndexScoreChoice();
                isc.Content = row["choice_content"].ToString();
                isc.id = row["id"].ToString();
                isc.Type = row["choice_type"].ToString();
                isc.ScoreValue = Helpers.Convert(row["choice_score_value"].ToString());
                s.Add(isc);

            }
            return s;
        }

        /// <summary>
        /// Save The choice
        /// </summary>
        /// <param name="_ref_id">ID OF RULA/REBA</param>
        /// <param name="_choiceType">Type of choice, score or additional</param>
        /// <param name="_ref_table">TYPE OF table, if reba or rula</param>
        /// <returns></returns>
        public int Save(int _ref_id, string _choiceType, string _ref_table)
        {
            this.Id = this.RandomString(12);
            if (this.Type.Length == 0) this.Type = _choiceType;
            var result = this.insert("INSERT INTO `" + this.table + "` (id,choice_type,choice_score_value,choice_content,choice_image_path,ref_id,ref_type) values('" + this.Id + "','" + this.Type + "','" + this.ScoreValue + "','" + this.Content + "','" + this.ImagePath + "','" + _ref_id + "','" + _ref_table + "')");
            return result;
        }


       
    }
}
