using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectK.ErgoMC.Assessment.classes;
namespace ProjectK.ErgoMC.Assessment.classes
{
    /// <summary>
    /// Interaction logic for splash.xaml
    /// </summary>
    public partial class splash : Window
    {
        public User _user { get; set; }
        public splash()
        {
            _user = new User();
            InitializeComponent();
            this.DataContext = _user;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           bool result = _user.login(txt_username.Text, txt_password.Password);
           if (result)
           {
               Session.user = new User();
               Session.user = Session.user.search(txt_username.Text, txt_password.Password);

               if (Session.user != null)
               {
                   ErgoMcApp app = new ErgoMcApp();
                   app.Show();
                   this.Close();
               }
               else
               {
                  
               }
           }
           else
           {
               MessageBox.Show("Invalid username or password.");
           } 
        }
    }
}
