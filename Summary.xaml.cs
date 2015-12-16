using ProjectK.ErgoMC.Assessment.classes;
using ProjectK.ErgoMC.Assessment.Library;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {

        private float num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0;
        public float Number1
        {
            get { return this.num1; }
            set { this.num1 = value; }
        }
        public float Number2
        {
            get { return this.num2; }
            set { this.num2 = value; }
        }
        public float Number3
        {
            get { return this.num3; }
            set { this.num3 = value; }
        }
        public float Number4
        {
            get { return this.num4; }
            set { this.num4 = value; }
        }
        public float Number5
        {
            get { return this.num5; }
            set { this.num5 = value; }
        }



        private float pnum1 = 0, pnum2 = 0, pnum3 = 0, pnum4 = 0, pnum5 = 0;
        public float pNumber1
        {
            get { return this.pnum1; }
            set { this.pnum1 = value; }
        }
        public float pNumber2
        {
            get { return this.pnum2; }
            set { this.pnum2 = value; }
        }
        public float pNumber3
        {
            get { return this.pnum3; }
            set { this.pnum3 = value; }
        }
        public float pNumber4
        {
            get { return this.pnum4; }
            set { this.pnum4 = value; }
        }
        public float pNumber5
        {
            get { return this.pnum5; }
            set { this.pnum5 = value; }
        }

        private float nnum1, nnum2, nnum3, nnum4, nnum5;
        public float nnumber1
        {
            get { return this.nnum1; }
            set { this.nnum1 = value; }
        }
        public float nnumber2
        {
            get { return this.nnum2; }
            set { this.nnum2 = value; }
        }
        public float nnumber3
        {
            get { return this.nnum3; }
            set { this.nnum3 = value; }
        }
        public float nnumber4
        {
            get { return this.nnum4; }
            set { this.nnum4 = value; }
        }
        public float nnumber5
        {
            get { return this.nnum5; }
            set { this.nnum5 = value; }
        }



        private float npnum1 = 0, npnum2 = 0, npnum3 = 0, npnum4 = 0, npnum5 = 0;
        public float npnumber1
        {
            get { return this.npnum1; }
            set { this.npnum1 = value; }
        }
        public float npnumber2
        {
            get { return this.npnum2; }
            set { this.npnum2 = value; }
        }
        public float npnumber3
        {
            get { return this.npnum3; }
            set { this.npnum3 = value; }
        }
        public float npnumber4
        {
            get { return this.npnum4; }
            set { this.npnum4 = value; }
        }
        public float npnumber5
        {
            get { return this.npnum5; }
            set { this.npnum5 = value; }
        }

          //case 1:
          //         this.description = "Neglible risk, no action required.";
          //         break;
          //     case 2:
          //     case 3:
          //         this.description = "Low risk, change may be needed.";
          //         break;
          //     case 4:
          //     case 5:
          //     case 6:
          //     case 7:
          //         this.description = "Medium risk,further investigation and change soon.";
          //     break;
          //     case 8:
          //     case 9:
          //     case 10:
          //     this.description = "High risk,investigate and implement change.";
          //     break;
          //     default:
          //         this.description = "Very high risk, implement change.";
          //         break;

        public Summary()
        {
            Model m = new Model();
            m.table = "reba";
            float total = 0;
            System.Data.DataTable t = m.selectQuery("SELECT final_score ,count(1) as number from reba GROUP By final_score");
            foreach (DataRow row in t.Rows)
            {
                switch (Helpers.Convert(row["final_score"].ToString()))
                {
                    case 1:
                        Number1 += Helpers.Convert(row["number"].ToString());
                        break;
                    case 2:
                    case 3:
                        Number2 += Helpers.Convert(row["number"].ToString());
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        Number3 += Helpers.Convert(row["number"].ToString());
                        break;
                    case 8:
                    case 9:
                    case 10:
                        Number4 += Helpers.Convert(row["number"].ToString());
                        break;
                    default:
                        Number5 += Helpers.Convert(row["number"].ToString());
                        break;
                }

            }
            total = (float)(num1 + num2 + num3 + num4 + num5);
            pNumber1 = Number1 / total * 100;
            pNumber2 = Number2 / total * 100;
            pNumber3 = Number3 / total * 100;
            pNumber4 = Number4 / total * 100;
            pNumber5 = Number5 / total * 100;


            float rulatotal = 0;
            System.Data.DataTable rt = m.selectQuery("SELECT final_score ,count(1) as number from rula GROUP By final_score");
            foreach (DataRow row in rt.Rows)
            {
                switch (Helpers.Convert(row["final_score"].ToString()))
                {
                    case 1:
                    case 2:
                        nnumber1 += Helpers.Convert(row["number"].ToString());
                        break;
                   
                    case 3:
                    case 4:
                        nnumber2 += Helpers.Convert(row["number"].ToString());
                        break;
                 
                    case 5:
                    case 6:
                        nnumber3 += Helpers.Convert(row["number"].ToString());
                        break;

                    default:
                        nnumber4 += Helpers.Convert(row["number"].ToString());
                        break;
                }

            }
            rulatotal = (float)(nnum1 + nnum2 + nnum3 + nnum4);
            npnumber1 = nnumber1 / rulatotal * 100;
            npnumber2 = nnumber2 / rulatotal * 100;
            npnumber3 = nnumber3 / rulatotal * 100;
            npnumber4 = nnumber4 / rulatotal * 100;


            
            InitializeComponent();
            
            DataContext = this;
        }


            //case 1:
            //    case 2:
            //        this.description = "Acceptable";
            //        break;
            //    case 3:
            //    case 4:
            //        this.description = "Investigate further.";
            //        break;
            //    case 5:
            //    case 6:
            //        this.description = "Investigate further and change soon.";
            //        break;
            //    default:
            //        this.description = "Investigate and change immediately";
            //        break;

          //switch (final_score)
          // {
          //     case 1:
          //         this.description = "Neglible risk, no action required.";
          //         break;
          //     case 2:
          //     case 3:
          //         this.description = "Low risk, change may be needed.";
          //         break;
          //     case 4:
          //     case 5:
          //     case 6:
          //     case 7:
          //         this.description = "Medium risk,further investigation and change soon.";
          //     break;
          //     case 8:
          //     case 9:
          //     case 10:
          //     this.description = "High risk,investigate and implement change.";
          //     break;
          //     default:
          //         this.description = "Very high risk, implement change.";
          //         break;
          // }
    }
}
