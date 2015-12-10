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
using ProjectK.ErgoMC.Assessment.Library;
using System.ComponentModel;
namespace ProjectK.ErgoMC.Assessment
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Page
    {
        Employee emp = new Employee();
        private Employee selectedEmployeee = new Employee();
        public double HeaderWidth { get; set; }
        public Employee SelectedEmployee
        {
            get { return this.selectedEmployeee;  }
            set { this.selectedEmployeee = value; }
        }
        private ListView _listView;
     
        public List()
        {
            
            InitializeComponent();
            FillList();
            this.DataContext = this;
            _listView = lv_list;
        }
        public void FillList()
        {
            lv_list.ItemsSource = emp.All();
            List<Employee> empList = emp.All();
            if(empList.Count > 0)
            this.selectedEmployeee = empList.First();
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

        private void lv_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listview = sender as ListView;

            if (listview.SelectedItem == null) return;
           
            this.SelectedEmployee = (Employee)listview.SelectedItem;
            this.SelectedEmployee.Rula_score.Get(this.SelectedEmployee);
            this.SelectedEmployee.Reba_score.Get(this.SelectedEmployee);
            scrollView.IsEnabled = true; 
           
            //PDFMaker.makePDF("" , emp);

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HeaderWidth = this.ActualWidth /2 / ((GridView)lv_list.View).Columns.Count;
           
            foreach (GridViewColumn v in ((GridView)lv_list.View).Columns)
            {
                v.Width = HeaderWidth;
                
            }
            scrollView.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Helpers.ToastWarning(Window.GetWindow(this),
                "Confirm saving changes.",
                "Are you sure you want to save the changes made?",
                MessageBoxButton.YesNo,
                MessageBoxResult.No
                ))
            {
                if (this.SelectedEmployee.Edit() >= 1)
                {
                    Helpers.ToastSuccess
                        (Window.GetWindow(this), "Successfully saved changes.", "The changes has been applied.", MessageBoxButton.OK);
                }
                else
                {
                    Helpers.ToastError
                    (Window.GetWindow(this), "Failed to saved changes.", "The changes has not been saved due to an error.", MessageBoxButton.OK);
                }
            }
        }

        private void Button_TakeAnalysis_Rula(object sender, RoutedEventArgs e)
        {
            ErgoMcApp._this.AddFrame(new Rula(this.SelectedEmployee));
        }

        private void Button_TakeAnalysis_Reba(object sender, RoutedEventArgs e)
        {
            ErgoMcApp._this.AddFrame(new Reba(this.SelectedEmployee));

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ScoreView r = new ScoreView(this.SelectedEmployee , ScoreView.ScoreViewType.Reba);
            r.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ScoreView r = new ScoreView(this.SelectedEmployee, ScoreView.ScoreViewType.Rula);
            r.Show();
        }

        private void Page_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            if (ErgoMcApp.status == 1)
            {

                FillList();
                ErgoMcApp.status = 0;
            }
        }

    }
}
