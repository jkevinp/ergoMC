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
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Window
    {
        Employee emp = new Employee();
        private ListView _listView;
        public List()
        {
            InitializeComponent();
            _listView = lv_list;
            FillList();
        }
        public void FillList()
        {
            lv_list.ItemsSource = emp.All();
            List<Employee> empList = emp.All();

        }

    }
}
