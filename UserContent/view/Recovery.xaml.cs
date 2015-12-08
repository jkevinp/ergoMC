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
using System.Windows.Media.Animation;
namespace ProjectK.ErgoMC.Assessment.UserContent.view
{
    /// <summary>
    /// Interaction logic for Recovery.xaml
    /// </summary>
    public partial class Recovery : Window
    {
        bool isHidden = true;
        private User _user = new User();
        public User User
        {
            get { return this._user; }
            set { this._user = value; }
        }
        public Recovery()
        {
            this.DataContext = this;
           
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isHidden)
            {
                var user = _user.search(txt_username.Text);
                if (user == null)
                {

                    Helpers.ToastError(this, "User not found", "Check your username and try again.", MessageBoxButton.OK);
                }
                else
                {
                    isHidden = false;
                    User = user;
                    txt_forgot_question.Text = User.Forgot_question;
                    txt_username.IsReadOnly = true;
                    Helpers.Animate(this, 0.3f, (float)this.Height, (float)this.MaxHeight, Window.HeightProperty);
                    Hidden.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                if (txt_forgot_answer.Text == User.Forgot_answer)
                {
                    Helpers.ToastSuccess(this, "User found!", "Your password is " + User.Password, MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    Helpers.ToastError(this, "Incorrect Answer", "Check your answer and try again.", MessageBoxButton.OK);
                }
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Helpers.Animate(this, 0.3f, 0, (float)this.Height, Window.HeightProperty);
            Helpers.Animate(this, 0.5f, 0, 1f, Window.OpacityProperty);
        }
    }
}
