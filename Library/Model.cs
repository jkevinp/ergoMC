using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SQLite;
using System.Reflection;
using ProjectK.ErgoMC.Assessment.classes;

namespace ProjectK.ErgoMC.Assessment.Library
{
    public  class Model
    {
        public Model()
        {
            this.connect(CONFIG.DB_NAME);
        }
       
        public string table = "";
        private Dictionary<string, Model> relations = new Dictionary<string, Model>();
        private SQLiteConnection _conn;
        private string path = string.Empty;
        private string extension = ".sqlite";
        const string create_table_001 = "create table IF NOT EXISTS employee(`id` INTEGER PRIMARY KEY, `firstname` varchar(30) , `lastname` varchar(30), `middlename` varchar(30) , `company` varchar(100), `job` varchar(50))";
        public Model(string _dbname)
        {
            path  = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  + "\\";
            this.connect(_dbname);
        }
        public void connect(string _dbname)
        {
            
            try {
                if (File.Exists(path + _dbname + extension))
                _conn = new SQLiteConnection("Data Source=" + path + _dbname + extension);
            else
            {
                this.CreateDatabase(_dbname);
                _conn = new SQLiteConnection("Data Source=" + path + _dbname +extension );
                
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public int CreateDatabase(string _dbname){
            if (File.Exists(path + _dbname + extension))
            {
                _conn = new SQLiteConnection("Data Source=" + path + _dbname + extension);
                _conn.Open();
                SQLiteCommand _cmd = new SQLiteCommand(create_table_001, _conn);
                
                int result = _cmd.ExecuteNonQuery();
                _conn.Close();
                return 0;
            }
                

            SQLiteConnection.CreateFile( path + _dbname +  extension);
          
            return 0;
        }
        public int insert(string _sql)
        {
            SQLiteCommand _cmd = new SQLiteCommand(_sql, _conn);
            _conn.Open();
            _cmd.ExecuteNonQuery();

            string sql = @"select last_insert_rowid()";
            _cmd.CommandText = sql;
            var result = Helpers.Convert(_cmd.ExecuteScalar().ToString());
            _conn.Close();
            return result;
        }
        public int update(string _sql)
        {
            SQLiteCommand _cmd = new SQLiteCommand(_sql, _conn);
            _conn.Open();
            var result = _cmd.ExecuteNonQuery();
            _conn.Close();
            return result;
        }
        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd = new SQLiteCommand(_conn);
                _conn.Open();  //Initiate connection to the db
                cmd = _conn.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                SQLiteDataReader r = cmd.ExecuteReader();
                //while (r.Read())
                //{
                //    Console.WriteLine(r);

                //}
                dt.Load(r);
                r.Close();
                _conn.Close();
                //ad.Fill(dt); //fill the datasource
                
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
            _conn.Close();
            return dt;
        }
    }
}
