using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
using System.Data;
using System.ComponentModel;
using ProjectK.ErgoMC.Assessment.Library;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class Employee :Model , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
        public Employee()
        {
            _employee = this;
            rulaScore = new RulaObject();
            this.table = _table;
        }
        public const string _table = "employee";
        #region private properties
        
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string company = string.Empty;
        private string job = string.Empty;
        private int id = 0;
        private RulaScore rula_score = null;
        #endregion
        #region public properties
        /// <summary>
        /// Rula Score Object
        /// </summary>
        public RulaObject rulaScore = null;
       
        /// <summary>
        /// Rula Model
        /// </summary>
        public RulaScore Rula_score
        {
            get { return this.rula_score; }
            set
            {
                rula_score = value;
            }
        }

        public string Firstname
        {
            get { return this.firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string Lastname
        {
            get { return this.lastname; }
            set { lastname = value; OnPropertyChanged("Lastname"); }
        }
        public string Middlename
        {
            get { return this.middlename; }
            set
            {
                middlename = value;
                OnPropertyChanged("Middlename");
            }
        }
        public string Company
        {
            get { return this.company; }
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }
        public string Job
        {
            get { return this.job; }
            set
            {
                job = value;
                OnPropertyChanged("Job");
            }
        }
        public int Id
        {
            get { return this.id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        #endregion
        
        private Employee _employee = null;
        public void Reset()
        {
        
            _employee.Firstname = "";
            _employee.Middlename = "";
            _employee.Lastname = "";
            _employee.Job = "";
            _employee.Company = "";
            _employee.id = 0;
        }

        /// <summary>
        /// Find an Employee
        /// </summary>
        /// <param name="_id">Employee id to be used.</param>
        /// <returns></returns>
        public Employee Find(int _id)
        {
            DataTable result = this.selectQuery("SELECT * FROM " +table  + " where `id`='" + _id + "' LIMIT 1");
            if (result.Rows.Count == 0) return null;
            _employee.Id = Helpers.Convert(result.Rows[0]["id"].ToString());
            _employee.Firstname = result.Rows[0]["firstname"].ToString();
            _employee.Middlename = result.Rows[0]["middlename"].ToString();
            _employee.Lastname = result.Rows[0]["lastname"].ToString();
            _employee.Job = result.Rows[0]["job"].ToString();
            _employee.company = result.Rows[0]["company"].ToString();
            _employee.id = Helpers.Convert( result.Rows[0]["id"].ToString());

            DataTable _rulascore = this.selectQuery("SELECT * FROM `rulascore` where `employee_id`= " + _id);
            foreach (DataRow dr in _rulascore.Rows)
            {
                int _value = Helpers.Convert(dr["score_value"].ToString());
                int _additional = Helpers.Convert(dr["score_additional"].ToString());
                switch (dr["score_name"].ToString())
                {
                    case "score_arm_wrist_load":
                        _employee.rulaScore.score_arm_wrist_load.score = _value;
                        _employee.rulaScore.score_arm_wrist_load.additionalScore = _additional;

                    break;
                    case "score_arm_wrist_muscle":
                    _employee.rulaScore.score_arm_wrist_muscle.score = _value;
                    _employee.rulaScore.score_arm_wrist_muscle.additionalScore = _additional;

                    break;
                    case "score_legs":
                    _employee.rulaScore.score_legs.score = _value;
                    _employee.rulaScore.score_legs.additionalScore = _additional;
                    break;
                }
            }
            return _employee;
        }
        /// <summary>
        /// Find All employee
        /// </summary>
        /// <returns>List of employees</returns>
        public List<Employee> All()
        {
            List<Employee> _result = new List<Employee>();
            DataTable result = this.selectQuery("SELECT * FROM `" + table +  "`");
            foreach(DataRow row in result.Rows){
                Employee employee = new Employee();
                employee.Id = Helpers.Convert(result.Rows[0]["id"].ToString());
                employee.Firstname = row["firstname"].ToString();
                employee.Middlename = row["middlename"].ToString();
                employee.Lastname = row["lastname"].ToString();
                employee.Job =  row["job"].ToString();
                employee.company = row["company"].ToString();
                employee.Rula_score = new RulaScore();
                employee.Rula_score.employee_id = employee.Id;
                employee.Rula_score.Get(employee);
                _result.Add(employee);
            }
            return _result;

        }
        /// <summary>
        /// Save current Employee
        /// </summary>
        /// <returns>1 if success, 0 if failed</returns>
        public int Save()
        {
            Employee _data = this._employee;
            var result = this.insert("INSERT INTO `" + table + "`(`firstname`,`middlename`,`lastname`,`job`,`company`) values('" + _data.Firstname + "' , '" + _data.Middlename + "', '" + _data.Lastname + "' , '" + _data.Job + "' , '" + _data.Company + "')");
            return result;
        }
        /// <summary>
        /// Check all fields if has match
        /// </summary>
        /// <param name="is_unique">true if use match search</param>
        /// <returns></returns>
        public int Save(bool is_unique)
        {
            Employee _data = this._employee;
            if (is_unique)
            {

                var check = this.selectQuery("SELECT count(1) as count FROM `" + table + "` where firstname= '" + _data.Firstname + "' AND lastname='" + _data.Lastname + "' AND middlename='" + _data.Middlename + "' AND company='" + _data.Company + "' AND job='" + _data.Job + "'");
                if (Helpers.Convert(check.Rows[0]["count"].ToString()) > 0)
                    return 0;
            }
            var result = this.insert("INSERT INTO `" + table + "`(`firstname`,`middlename`,`lastname`,`job`,`company`) values('" + _data.Firstname + "' , '" + _data.Middlename + "', '" + _data.Lastname + "' , '" + _data.Job + "' , '" + _data.Company + "')");
            return result;
        }
        /// <summary>
        /// Edit and save current Employee
        /// </summary>
        /// <returns>1 if success, 0 if failed</returns>
        public int Edit()
        {
            Employee _data = this._employee;
            var result = this.insert("INSERT INTO `" + table + "` SET firstname= '" + _data.Firstname + "', lastname='" + _data.Lastname + "' , middlename='" + _data.Middlename + "' ,company='" + _data.Company + "' ,job='" + _data.Job + "'");
            return result;
        }
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}