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

namespace ProjectK.ErgoMC.Assessment.UI
{
    /// <summary>
    /// Interaction logic for Modal.xaml
    /// </summary>
    public partial class Modal : Window
    {
        private string message, buttonOneText, buttonTwoText ,window_title;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
        public string ButtonOneText
        {
            get { return this.buttonOneText; }
            set { this.buttonOneText = value; }
        }
        public string ButtonTwoText
        {
            get { return this.buttonTwoText; }
            set { this.buttonTwoText = value; }
        }
        public string FormTitle
        {
            get { return this.window_title; }
            set { this.window_title = value; }
        }
        
        /// <summary>
        /// Create a new Modal
        /// </summary>
        /// <param name="_title">Title of the modal</param>
        /// <param name="_msg">Modal Message</param>
        /// <param name="_buttonOneText">Positive Answer Text</param>
        /// <param name="_buttonTwoText">Negative Answer text</param>
        public Modal(string _title,string _msg , string _buttonOneText , string _buttonTwoText)
        {
            this.Message = _msg;
            this.FormTitle = _title;
            this.ButtonOneText = _buttonOneText.ToUpper();
            this.ButtonTwoText = _buttonTwoText.ToUpper();
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
