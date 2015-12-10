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
using ProjectK.ErgoMC.Assessment.UserContent.view;
using ProjectK.ErgoMC.Assessment.Library;
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

            Model m = new Model(CONFIG.DB_NAME);
            m.CreateDatabase(CONFIG.DB_NAME);
            txt_username.Focus();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Recovery r = new Recovery();
            r.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Helpers.Animate(this, 0.3f, 0, (float)this.Height, Window.HeightProperty);
            Helpers.Animate(this, 0.5f, 0, 1f, Window.OpacityProperty);
        }

        private void txt_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, new RoutedEventArgs());
            }
        }
    }
}
