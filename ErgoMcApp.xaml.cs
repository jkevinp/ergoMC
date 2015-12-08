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
    /// Interaction logic for ErgoMcApp.xaml
    /// </summary>
    public partial class ErgoMcApp : Window
    {
        public static ErgoMcApp _this = null;
        private classes.User _user = Session.user;
        public classes.User user
        {
            get { return this._user; }
            set { this._user = value; }
        }

        public ErgoMcApp()
        {
            InitializeComponent();
            AddFrame(new List());
            this.DataContext = this;

            _this = this;
        }
        public void AddFrame(Page _window)
        {
            TabItem tabitem = new TabItem();
            tabitem.Header = "Tab: " + _window.GetType().Name;
            Frame tabFrame = new Frame();
            _window.GetType();
            tabFrame.Content = _window;
            tabitem.Content = tabFrame;
            TabControl.Items.Add(tabitem);
            tabitem.Focus();
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.Items.Count > 0) { 
            if (!Helpers.ToastWarning(this, "Are you sure you want to close this tab?", "Press Yes to close, No to cancel.", MessageBoxButton.YesNo, MessageBoxResult.No)) return;
                TabItem _tabitem = TabControl.SelectedItem as TabItem;
                if (_tabitem != null) TabControl.Items.Remove(_tabitem);
            }
            else
            {
                Helpers.ToastRequired(this, "No more tabs to close.", "Cannot close an non-existing tab.", MessageBoxButton.OK);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var type = Type.GetType("ProjectK.ErgoMC.Assessment." + btn.Tag.ToString());
            Page p = (Page)Activator.CreateInstance(type);
            AddFrame(p);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            TabItem _tabitem = TabControl.SelectedItem as TabItem;
            int t = TabControl.SelectedIndex;
            if (t >= 1)
            {
                TabControl.SelectedIndex = t - 1;
            }
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            TabItem _tabitem = TabControl.SelectedItem as TabItem;
            int t = TabControl.SelectedIndex;
            if (TabControl.SelectedIndex < TabControl.Items.Count)
            {
                TabControl.SelectedIndex = t +1;
            }
        }

    }
}
