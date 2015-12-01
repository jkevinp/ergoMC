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
    public partial class List : Page
    {
        Employee emp = new Employee();
        private ListView _listView;
        public List()
        {
            InitializeComponent();
            _listView = lv_list;
            FillList();
            this.DataContext = this;
        }
        public void FillList()
        {
            lv_list.ItemsSource = emp.All();
            List<Employee> empList = emp.All();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> _search = new Dictionary<string, string>();
            _search.Add("id", txt_search.Text);
            _search.Add("firstname", txt_search.Text);
            _search.Add("middlename", txt_search.Text);
            _search.Add("lastname", txt_search.Text);
            _search.Add("company", txt_search.Text);
            _search.Add("job", txt_search.Text);
            lv_list.ItemsSource = emp.All(_search, "OR");
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dictionary<string, string> _search = new Dictionary<string, string>();
            _search.Add("id", txt_search.Text);
            _search.Add("firstname", txt_search.Text);
            _search.Add("middlename", txt_search.Text);
            _search.Add("lastname", txt_search.Text);
            _search.Add("company", txt_search.Text);
            _search.Add("job", txt_search.Text);
            lv_list.ItemsSource = emp.All(_search, "OR");
        }

    }
}
