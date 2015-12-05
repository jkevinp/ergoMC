using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectK.ErgoMC.Assessment.Library;
using System.Data;
namespace ProjectK.ErgoMC.Assessment.classes
{
    public class User :Model
    {


        public User()
        {
            this.table = "user";
            
        }
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string user_type = string.Empty;
        private string forgot_answer = string.Empty;
        private string forgot_question = string.Empty;
        private int id = 0;
        private string username = string.Empty;
        private string password = string.Empty;

        public string FirstName
        {
            get { return this.firstname; }
            set { this.firstname = value; }
        }
        public string LastName
        {
            get { return this.lastname; }
            set { this.lastname = value; }
        }
        public string MiddleName
        {
            get { return this.middlename; }
            set { this.middlename = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get {
                string _middlename = (this.middlename.Length > 0) ? this.middlename.Substring(0, 1) : " ";
                return this.firstname + " "  + _middlename  +" " + this.lastname; }
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
                user.id = Helpers.Convert(result.Rows[0]["id"].ToString());

                return user;
             
            }
            return null;
        }


    }
}
