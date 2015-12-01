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

namespace ProjectK.ErgoMC.Assessment
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        Employee _employee = null;
        public EmployeeView(RulaObject _rulaObject)
        {
            this._employee = new Employee();
            this._employee.rulaScore = _rulaObject;
            _employee.Rula_score = new RulaScore();
            _employee.Rula_score.CreateFromRulaObject(_rulaObject);
           
            this.DataContext = _employee;
            InitializeComponent();
        }
        private void btn_evaluate_Click(object sender, RoutedEventArgs e)
        {
            _employee.Id = _employee.Save(true);
            if (_employee.Id == 0)
            {
                MessageBox.Show("Please Fill in all fields or Check if the employee record already exists.");
                return;
            }
           this._employee.Rula_score.employee_id = _employee.Id;
           var rula_id = this._employee.Rula_score.Save(true);

           if (rula_id == 0)
           {
               MessageBox.Show("Please Fill in all fields or Check if the employee record already exists.");
               return;
           }
           foreach(IndexScore indexScore in this._employee.rulaScore.getScoreList()){
                indexScore.employee_id = _employee.Id;
                indexScore.rula_id = rula_id;
                indexScore.Save();
            }
           MessageBox.Show(this, "The Record has been saved", "Save Complete", MessageBoxButton.OK, MessageBoxImage.Information);
           this.btn_evaluate.IsEnabled = false;
            Console.WriteLine(_employee.Id);
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            _employee.Reset();
        }

     

    }
}
