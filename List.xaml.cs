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
    /// 

    public partial class List : Page
    {
        double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double screenhight = System.Windows.SystemParameters.PrimaryScreenHeight;
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
            EmployeeCount = empList.Count;
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
            this.SelectedEmployee.Rula_score.Get(this.SelectedEmployee, "right");
            this.SelectedEmployee.Reba_score.Get(this.SelectedEmployee, "right");

            this.SelectedEmployee.LeftRula_score.Get(this.SelectedEmployee, "left");
            this.SelectedEmployee.LeftReba_score.Get(this.SelectedEmployee, "left");
            
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
            FillList();
        }

        private void Button_TakeAnalysis_Rula(object sender, RoutedEventArgs e)
        {
            if(this.SelectedEmployee != null)
            ErgoMcApp._this.AddFrame(new Rula(this.SelectedEmployee, "right"));
        }
        private void Button_TakeAnalysis_Reba(object sender, RoutedEventArgs e)
        {
            if (this.SelectedEmployee != null) ErgoMcApp._this.AddFrame(new Reba(this.SelectedEmployee, "right"));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string Tag = "right";
            Button t = sender as Button;
            if (t.Tag != null)
            {
                Tag = "left";
            }
            ScoreView r = new ScoreView(this.SelectedEmployee , ScoreView.ScoreViewType.Reba, Tag);
            r.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string Tag = "right";
            Button t = sender as Button;
            if (t.Tag != null)
            {
                Tag = "left";
            }
            ScoreView r = new ScoreView(this.SelectedEmployee, ScoreView.ScoreViewType.Rula, Tag);
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
        private int employeeCount = 0;
        public int EmployeeCount
        {
            get
            {
                return this.employeeCount;
            }
            set
            {
                this.employeeCount = value;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.SelectedEmployee != null && lv_list.SelectedItems.Count == 1)
            {
                PDFMaker.makePDF("", this.SelectedEmployee);
            }
            else if (lv_list.SelectedItems.Count >= 2)
            {
                List<Employee> emplist = new List<Employee>();
                foreach (object i in lv_list.SelectedItems)
                {
                    emplist.Add((Employee)i);
                }

                PDFMaker.makePDF("", emplist);
            }
        }

        private void Button_TakeAnalysis_Reba_Left(object sender, RoutedEventArgs e)
        {

            if (this.SelectedEmployee != null) ErgoMcApp._this.AddFrame(new Reba(this.SelectedEmployee, "left"));
        }

        private void Button_TakeAnalysis_Rula_Left(object sender, RoutedEventArgs e)
        {
            if (this.SelectedEmployee != null) ErgoMcApp._this.AddFrame(new Rula(this.SelectedEmployee, "left"));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.ToastWarning(Window.GetWindow(this),
                "Confirm saving changes.",
                "Are you sure you want to save the changes made?",
                MessageBoxButton.YesNo,
                MessageBoxResult.No
                ))
            {
           
                if (this.SelectedEmployee.Delete() >= 1)
                {
                    Helpers.ToastSuccess (Window.GetWindow(this), "Successfully saved changes.", "The changes has been applied.", MessageBoxButton.OK);
                }
                else
                {
                    Helpers.ToastError(Window.GetWindow(this), "Failed to saved changes.", "The changes has not been saved due to an error.", MessageBoxButton.OK);
                 
                }
            }
            FillList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Summary m = new Summary();
            m.Show();
        }

    }
}
