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
    /// Interaction logic for RebaScoreView.xaml
    /// </summary>
    public partial class ScoreView : Window
    {
        public enum ScoreViewType
        {
            Reba,
            Rula
        }
        public ScoreView(Employee _employee , ScoreViewType _type)
        {
            InitializeComponent();
            List<IndexScore> l;
            this.DataContext = _employee;
            if(_type == ScoreViewType.Reba)
             l =new IndexScore().GetReba(_employee.Id , _employee.Reba_score.id);
            else l = new IndexScore().GetRula(_employee.Id, _employee.Rula_score.id);

            foreach (IndexScore _indexScore in l)
            {

                Label t = new Label();
                t.Content = _indexScore.DisplayName;
                t.FontSize = 16;
                _stackPane1.Children.Add(t);

                Label t2 = new Label();
                t2.Content = _indexScore.Score.ToString();
                t2.FontSize = 16;
                t2.BorderThickness = new Thickness(0, 0, 0, 1);
                t2.BorderBrush = (Brush)TryFindResource("KinectBluish");
                t2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                
                _stackPane2.Children.Add(t2);
            }
        }
    }
}
