using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using ProjectK.ErgoMC.Assessment.classes;
using PdfSharp.Drawing.Layout;
namespace ProjectK.ErgoMC.Assessment.Library
{
    public static class PDFMaker
    {
        public static PdfDocument makePDF()
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
            string pdfFilename = "firstpage.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
            return pdf;
        }
        public static  int margin = 20;
        public static List<string> _textList = new List<string>();
        public static PdfDocument makePDF(string title, Employee employee)
        {
            _textList = new List<string>();



            _textList.Add("Name of Employee: " + employee.Name);
            _textList.Add("Job: " + employee.Job);
            _textList.Add("Company: " + employee.Company);
            _textList.Add("");
            _textList.Add("Evaluation result");
            _textList.Add("");
            if (employee.Rula_score != null)
            {

                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                           Rula(Right)");
                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                                                   Date Evaluated: " + employee.Rula_score.DateStringEvaluated);
                _textList.Add("                                                                                   Evaluated By : " + employee.Rula_score.evaluator_name);
                _textList.Add("Posture Score A: " + employee.Rula_score.posture_score_a);
                _textList.Add("Wrist & Arm Score: " + employee.Rula_score.final_wrist_arm_score);
                _textList.Add("Posture Score B: " + employee.Rula_score.posture_score_b);
                _textList.Add("Neck,Trunk & Legs Score: " + employee.Rula_score.final_neck_trunk_leg_score);
                _textList.Add("Final Rula Score: " + employee.Rula_score.final_score);
                _textList.Add("Level of MSD Risk: " + employee.Rula_score.description);
            }
            _textList.Add("");
            if (employee.LeftRula_score != null)
            {
                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                           Rula(Left)");
                _textList.Add("--------------------------------------------------------------------------------------------------");

                _textList.Add("                                                                                   Date Evaluated: " + employee.LeftRula_score.DateStringEvaluated);
                _textList.Add("                                                                                   Evaluated By : " + employee.LeftRula_score.evaluator_name);
                _textList.Add("Posture Score A: " + employee.LeftRula_score.posture_score_a);
                _textList.Add("Wrist & Arm Score: " + employee.LeftRula_score.final_wrist_arm_score);
                _textList.Add("Posture Score B: " + employee.LeftRula_score.posture_score_b);
                _textList.Add("Neck,Trunk & Legs Score: " + employee.LeftRula_score.final_neck_trunk_leg_score);
                _textList.Add("Final Rula Score: " + employee.LeftRula_score.final_score);
                _textList.Add("Level of MSD Risk: " + employee.LeftRula_score.description);
            }
            _textList.Add("_newpage");



            _textList.Add("Name of Employee: " + employee.Name);
            _textList.Add("Job: " + employee.Job);
            _textList.Add("Company: " + employee.Company);
            _textList.Add("");
            _textList.Add("Evaluation result");
            _textList.Add("");
            if (employee.Reba_score != null)
            {

                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                           Reba(Right)");
                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                                                   Date Evaluated: " + employee.Reba_score.DateStringEvaluated);
                _textList.Add("                                                                                   Evaluated By : " + employee.Reba_score.evaluator_name);
                _textList.Add("Posture Score A: " + employee.Reba_score.posture_score_a);
                _textList.Add("Neck,Trunk & Legs Score: " + employee.Reba_score.final_score_a);
                _textList.Add("Posture Score B: " + employee.Reba_score.posture_score_b);
                _textList.Add("Wrist & Arm Score: " + employee.Reba_score.final_score_b);
                _textList.Add("Table C Score: " + employee.Reba_score.table_score_c);
                _textList.Add("Final Reba Score: " + employee.Reba_score.final_score);
                _textList.Add("Level of MSD Risk: " + employee.Reba_score.description);
            }
            _textList.Add("");
            if (employee.LeftReba_score != null)
            {
                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                           Reba(Left)");
                _textList.Add("--------------------------------------------------------------------------------------------------");
                _textList.Add("                                                                                   Date Evaluated: " + employee.LeftReba_score.DateStringEvaluated);
                _textList.Add("                                                                                   Evaluated By : " + employee.LeftReba_score.evaluator_name);
                _textList.Add("Posture Score A: " + employee.LeftReba_score.posture_score_a);
                _textList.Add("Neck,Trunk & Legs Score: " + employee.LeftReba_score.final_score_a);
                _textList.Add("Posture Score B: " + employee.LeftReba_score.posture_score_b);
                _textList.Add("Wrist & Arm Score: " + employee.LeftReba_score.final_score_b);
                _textList.Add("Table C Score: " + employee.LeftReba_score.table_score_c);
                _textList.Add("Final Reba Score: " + employee.LeftReba_score.final_score);
                _textList.Add("Level of MSD Risk: " + employee.LeftReba_score.description);
            }





            PdfDocument pdf = new PdfDocument();
            pdf.PageLayout = PdfPageLayout.SinglePage;
            string report_name = "Report for " + employee.Name + new Random().Next(0001, 1000);
            pdf.Info.Title = "Report for " + employee.Name;

            PdfPage pdfPage = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 12, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(graph);


            int x = 1;
            foreach (string s in _textList)
            {
                if (s == "_newpage")
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);
                    font = new XFont("Verdana", 12, XFontStyle.Bold);
                    tf = new XTextFormatter(graph);
                    x = 1;
                }
                else AddLine(tf, s, pdfPage, x);
                x++;
            }

            string pdfFilename = report_name + ".pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
            return pdf;
        }

        public static PdfDocument makePDF(string title, List<Employee> lemployee)
        {
            _textList = new List<string>();



            foreach (Employee employee in lemployee)
            {
                _textList.Add("Name of Employee: " + employee.Name);
                _textList.Add("Job: " + employee.Job);
                _textList.Add("Company: " + employee.Company);
                _textList.Add("");
                _textList.Add("Evaluation result");
                _textList.Add("");
                if (employee.Rula_score != null)
                {

                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                           Rula(Right)");
                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                                                   Date Evaluated: " + employee.Rula_score.DateStringEvaluated);
                    _textList.Add("                                                                                   Evaluated By : " + employee.Rula_score.evaluator_name);
                    _textList.Add("Posture Score A: " + employee.Rula_score.posture_score_a);
                    _textList.Add("Wrist & Arm Score: " + employee.Rula_score.final_wrist_arm_score);
                    _textList.Add("Posture Score B: " + employee.Rula_score.posture_score_b);
                    _textList.Add("Neck,Trunk & Legs Score: " + employee.Rula_score.final_neck_trunk_leg_score);
                    _textList.Add("Final Rula Score: " + employee.Rula_score.final_score);
                    _textList.Add("Level of MSD Risk: " + employee.Rula_score.description);
                }
                _textList.Add("");
                if (employee.LeftRula_score != null)
                {
                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                           Rula(Left)");
                    _textList.Add("--------------------------------------------------------------------------------------------------");

                    _textList.Add("                                                                                   Date Evaluated: " + employee.LeftRula_score.DateStringEvaluated);
                    _textList.Add("                                                                                   Evaluated By : " + employee.LeftRula_score.evaluator_name);
                    _textList.Add("Posture Score A: " + employee.LeftRula_score.posture_score_a);
                    _textList.Add("Wrist & Arm Score: " + employee.LeftRula_score.final_wrist_arm_score);
                    _textList.Add("Posture Score B: " + employee.LeftRula_score.posture_score_b);
                    _textList.Add("Neck,Trunk & Legs Score: " + employee.LeftRula_score.final_neck_trunk_leg_score);
                    _textList.Add("Final Rula Score: " + employee.LeftRula_score.final_score);
                    _textList.Add("Level of MSD Risk: " + employee.LeftRula_score.description);
                }
                _textList.Add("_newpage");



                _textList.Add("Name of Employee: " + employee.Name);
                _textList.Add("Job: " + employee.Job);
                _textList.Add("Company: " + employee.Company);
                _textList.Add("");
                _textList.Add("Evaluation result");
                _textList.Add("");
                if (employee.Reba_score != null)
                {

                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                           Reba(Right)");
                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                                                   Date Evaluated: " + employee.Reba_score.DateStringEvaluated);
                    _textList.Add("                                                                                   Evaluated By : " + employee.Reba_score.evaluator_name);
                    _textList.Add("Posture Score A: " + employee.Reba_score.posture_score_a);
                    _textList.Add("Neck,Trunk & Legs Score: " + employee.Reba_score.final_score_a);
                    _textList.Add("Posture Score B: " + employee.Reba_score.posture_score_b);
                    _textList.Add("Wrist & Arm Score: " + employee.Reba_score.final_score_b);
                    _textList.Add("Table C Score: " + employee.Reba_score.table_score_c);
                    _textList.Add("Final Reba Score: " + employee.Reba_score.final_score);
                    _textList.Add("Level of MSD Risk: " + employee.Reba_score.description);
                }
                _textList.Add("");
                if (employee.LeftReba_score != null)
                {
                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                           Reba(Left)");
                    _textList.Add("--------------------------------------------------------------------------------------------------");
                    _textList.Add("                                                                                   Date Evaluated: " + employee.LeftReba_score.DateStringEvaluated);
                    _textList.Add("                                                                                   Evaluated By : " + employee.LeftReba_score.evaluator_name);
                    _textList.Add("Posture Score A: " + employee.LeftReba_score.posture_score_a);
                    _textList.Add("Neck,Trunk & Legs Score: " + employee.LeftReba_score.final_score_a);
                    _textList.Add("Posture Score B: " + employee.LeftReba_score.posture_score_b);
                    _textList.Add("Wrist & Arm Score: " + employee.LeftReba_score.final_score_b);
                    _textList.Add("Table C Score: " + employee.LeftReba_score.table_score_c);
                    _textList.Add("Final Reba Score: " + employee.LeftReba_score.final_score);
                    _textList.Add("Level of MSD Risk: " + employee.LeftReba_score.description);
                }
                _textList.Add("_newpage");

            }



            PdfDocument pdf = new PdfDocument();
            pdf.PageLayout = PdfPageLayout.SinglePage;
            string report_name = "Report" + new Random().Next(0001, 1000);
            pdf.Info.Title = "Report";

            PdfPage pdfPage = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 12, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(graph);


            int x = 1;
            foreach (string s in _textList)
            {
                if (s == "_newpage")
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);
                    font = new XFont("Verdana", 12, XFontStyle.Bold);
                    tf = new XTextFormatter(graph);
                    x = 1;
                }
                else AddLine(tf, s, pdfPage, x);
                x++;
            }

            string pdfFilename = report_name + ".pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
            return pdf;
        }

        public static void AddLine(XTextFormatter tf, string text, PdfPage pdfPage , int line)
        {
              XFont font = new XFont("Verdana", 12, XFontStyle.Bold);
              
              tf.DrawString(text, font, XBrushes.Black, createLine(pdfPage, line), XStringFormats.TopLeft);
        }

        public static XRect createLine(PdfPage _pdfPage , int currentLine)
        {
            int startHeight = (int)_pdfPage.Height / 16;
            int lineheight = 16;
            int linebreakheight = 12;
            XRect rect = new XRect(margin, startHeight + (lineheight * currentLine) + linebreakheight, _pdfPage.Width - margin, _pdfPage.Height / 8);
            return rect;
        }
    }
}
