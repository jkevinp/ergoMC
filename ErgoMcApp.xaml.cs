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

namespace ProjectK.ErgoMC.Assessment
{
    /// <summary>
    /// Interaction logic for ErgoMcApp.xaml
    /// </summary>
    public partial class ErgoMcApp : Window
    {
        public ErgoMcApp()
        {
            InitializeComponent();
            AddFrame(new MainWindow());
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
            TabItem _tabitem = TabControl.SelectedItem as TabItem;
            if(_tabitem !=null)
            TabControl.Items.Remove(_tabitem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var type = Type.GetType("ProjectK.ErgoMC.Assessment." + btn.Tag.ToString());
            Page p = (Page)Activator.CreateInstance(type);
            AddFrame(p);
        }

    }
}
