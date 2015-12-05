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
    /// Interaction logic for RebaWindow.xaml
    /// </summary>
    public partial class RebaWindow : Window
    {
        public RebaWindow(string _tag)
        {

            InitializeComponent();
            switch (_tag)
            {
                case "neck_position":
                    Button btn = new Button();
                    btn.Width = 100;
                    btn.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(@"Images/RebaSelection/neck_position/1.png", UriKind.Relative)),
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    stack_panel.Children.Add(btn);
                break;
                case "trunk_position":
                
                break;
                case "legs_position":
                
                break;
                case "neck_trunk_legs_load":
                
                break;
                case "upper_arm":
                    break;
                case "lower_arm":
                    break;
                case "wrist_position":
                    break;
                case "coupling":
                    break;
                case "activity":
                    break;

            }
        }
    }
}
