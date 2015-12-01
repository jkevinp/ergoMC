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
            _employee.Id = _employee.Save();
            Console.WriteLine(_employee.Id);
        }

    }
}
