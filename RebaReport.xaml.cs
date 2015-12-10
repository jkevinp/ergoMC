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
    public partial class RebaReport : Window
    {
        Employee _employee = null;
        public RebaReport(RebaObject _rebaObject)
        {
            this._employee = new Employee();
            this._employee.RebaObject = _rebaObject;
            this._employee.Reba_score = new RebaScore();
            this._employee.Reba_score.calculateAll(_rebaObject);
            this.DataContext = _employee;
            InitializeComponent();
        }
        public RebaReport(RebaObject _rebaObject , Employee _emp)
        {
            this._employee = _emp;
            this._employee.RebaObject = _rebaObject;
            this._employee.Reba_score = new RebaScore();
            this._employee.Reba_score.calculateAll(_rebaObject);
            this.DataContext = _employee;
            InitializeComponent();
        }
        private void btn_evaluate_Click(object sender, RoutedEventArgs e)
        {
            if (_employee.Id == 0) _employee.Id = _employee.Save(true);
            if (_employee.Id == 0)
            {
                MessageBox.Show("Please Fill in all fields or Check if the employee record already exists.");
                return;
            }
            this._employee.Reba_score.employee_id = _employee.Id;
            var id = this._employee.Reba_score.Save(true);

            if (id == 0)
            {
                MessageBox.Show("Please Fill in all fields or Check if the employee record already exists.");
                return;
            }

            foreach (IndexScore indexScore in this._employee.RebaObject.getScoreList())
            {
                indexScore.ChangeTable("rebascore");
                indexScore.employee_id = _employee.Id;
                indexScore.id = id;
                indexScore.SaveReba();
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
