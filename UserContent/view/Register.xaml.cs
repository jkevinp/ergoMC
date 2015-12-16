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
namespace ProjectK.ErgoMC.Assessment.UserContent.view
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private User _user = new User();
        public User User
        {
            get { return this._user; }
            set { this._user = value; }
        }
        public Register()
        {

            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password = txt_password.Password;
            string password2 = txt_password1.Password;
            if (!password.Equals(password2))
            {
                Helpers.ToastRequired(this, "Registration Failed.", "Password does not match!", System.Windows.MessageBoxButton.OK);
                return;
            }
            _user.Password = password;
            int result = _user.Save();
          if (result == 0)
          {
              Helpers.ToastRequired(this, "Registration Failed.", "Please fill in all fields or check if the current user does not exists from the database.", System.Windows.MessageBoxButton.OK);
          }
          else
          {
              Helpers.ToastSuccess(this, "Registration Success!", "User has been saved.", MessageBoxButton.OK);
          }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txt_password.Password = string.Empty;
            txt_password1.Password = string.Empty;
            _user.Reset();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helpers.Animate(this, 0.3f, (float)this.Height, 0, Window.HeightProperty);
            Helpers.Animate(this, 0.5f, 1f, 0, Window.OpacityProperty, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Helpers.Animate(this, 0.3f, 0, (float)this.Height, Window.HeightProperty);
            Helpers.Animate(this, 0.5f, 0, 1f, Window.OpacityProperty);
        }
    }
}
