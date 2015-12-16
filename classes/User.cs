using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
using System.Data;
using System.Windows;
using System.ComponentModel;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class User : Model, INotifyPropertyChanged
    {


        public User()
        {
            this.table = "user";
            
        }
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string user_type = "admin";
        private string forgot_answer = string.Empty;
        private string forgot_question = string.Empty;
        private int id = 0;
        private string username = string.Empty;
        private string password = string.Empty;
        private string status = "active";

        public string FirstName
        {
            get { return this.firstname; }
            set { this.firstname = value; OnPropertyChanged("FirstName"); }
        }
        public string LastName
        {
            get { return this.lastname; }
            set { this.lastname = value; OnPropertyChanged("LastName"); }
        }
        public string MiddleName
        {
            get { return this.middlename; }
            set { this.middlename = value; OnPropertyChanged("Middlename"); }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged("Id"); }
        }
        public string User_type
        {
            get { return this.user_type; }
            set { this.user_type = value; OnPropertyChanged("User_type"); }
        }
        public string Forgot_answer
        {
            get { return this.forgot_answer; }
            set { this.forgot_answer = value; OnPropertyChanged("Forgot_answer"); }
        }
        public string Forgot_question
        {
            get { return this.forgot_question; }
            set { this.forgot_question = value; OnPropertyChanged("Forgot_question"); }
        }
        public string UserName
        {
            get { return this.username; }
            set { this.username = value; OnPropertyChanged("UserName"); }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; OnPropertyChanged("Password"); }
        }
        public string Status
        {
            get {return this.status; }
            set { this.status = value; OnPropertyChanged("Status"); }
        }
        public string Name
        {
            get {
                string _middlename = (this.middlename.TrimStart(' ').Length > 0) ? this.middlename.TrimStart(' ').Substring(0, 1) + "." : "";
                return this.firstname + " "  + _middlename  +" " + this.lastname; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void Reset()
        {
          this.FirstName = string.Empty;
          this.LastName = string.Empty;
          this.MiddleName = string.Empty;
          this.User_type = "admin";
          this.Forgot_answer = string.Empty;
          this.Forgot_question = string.Empty;
          this.Id = 0;
          this.UserName = string.Empty;
          this.Password = string.Empty;
          this.Status = "active";
        }

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public bool login(string _username, string _password)
        {
            DataTable result = this.selectQuery("SELECT * from '" + this.table + "' where username='" + _username + "' and password='" + _password + "' and status='active' LIMIT 1");
            if (result.Rows.Count > 0) return true;
            return false;
        }
        /// <summary>
        /// Search user by username and password
        /// </summary>
        /// <param name="_username">Username of the user</param>
        /// <param name="_password">Password of the user</param>
        /// <returns>Null if not found, user object when found.</returns>
        public User search(string _username, string _password)
        {
            User user = new User();
            DataTable result = this.selectQuery("SELECT * from '" + this.table + "' where username='" + _username + "' and password='" + _password + "' and status='active' LIMIT 1");
            if (result.Rows.Count > 0)
            {
                user.firstname = (result.Rows[0]["firstname"].ToString());
                user.middlename = (result.Rows[0]["middlename"].ToString());
                user.lastname = (result.Rows[0]["lastname"].ToString());
                user.username = (result.Rows[0]["username"].ToString());
                user.forgot_answer = (result.Rows[0]["forgot_answer"].ToString());
                user.forgot_question = (result.Rows[0]["forgot_question"].ToString());
                user.id = Helpers.Convert(result.Rows[0]["id"].ToString());

                return user;
             
            }
            return null;
        }

        /// <summary>
        /// Search user record
        /// </summary>
        /// <param name="_username">the username of the user</param>
        /// <returns>Null if not found, User object if found.</returns>
        public User search(string _username)
        {
            User user = new User();
            DataTable result = this.selectQuery("SELECT * from '" + this.table + "' where username='" + _username + "' and status='active' LIMIT 1");
            if (result.Rows.Count > 0)
            {
                user.firstname = (result.Rows[0]["firstname"].ToString());
                user.middlename = (result.Rows[0]["middlename"].ToString());
                user.lastname = (result.Rows[0]["lastname"].ToString());
                user.username = (result.Rows[0]["username"].ToString());
                user.password = (result.Rows[0]["password"].ToString());
                user.id = Helpers.Convert(result.Rows[0]["id"].ToString());

                user.forgot_answer = (result.Rows[0]["forgot_answer"].ToString());
                user.forgot_question = (result.Rows[0]["forgot_question"].ToString());

                return user;

            }
            return null;
        }

        public int Save()
        {
          
            foreach (System.Reflection.PropertyInfo pi in this.GetType().GetProperties())
            {
                 if (pi.CanRead)
                 {
                     string value = pi.GetValue(this).ToString();
                     if (value.Length == 0)
                     {
                         
                         return 0;
                     }
                 }
            }
            int count = Helpers.Convert(this.selectQuery("SELECT count(1) as count FROM " + this.table + " where username='" + this.username + "'").Rows[0]["count"].ToString());
            if (count > 0)
            {
                return 0;
            }

            var result = this.insert("INSERT INTO `" + this.table + "` (firstname,middlename,lastname, username, password, forgot_question , forgot_answer, user_type, status) values('" + this.FirstName + "' ,' " + this.MiddleName + "' ,'" + this.LastName + "','" + this.username + "','" + this.password + "','" + this.forgot_question + "','" + this.forgot_answer + "','" + this.user_type + "','" + this.status + "')");
            return result;
        } 

    }
}
