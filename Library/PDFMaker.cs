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

        public static PdfDocument makePDF(string title, Employee employee)
        {

            PdfDocument pdf = new PdfDocument();
            pdf.PageLayout = PdfPageLayout.SinglePage;
            string report_name = "Report for " + employee.Name + new Random().Next(0001, 1000);
            pdf.Info.Title = "Report for " + employee.Name;

            PdfPage pdfPage = pdf.AddPage();
           
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 12, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(graph);
            
           
            tf.DrawString("Name of Employee: " + employee.Name, font, XBrushes.Black, createLine(pdfPage,1), XStringFormats.TopLeft);
            tf.DrawString("Job: " + employee.Job, font, XBrushes.Black, createLine(pdfPage, 2), XStringFormats.TopLeft);
            tf.DrawString("Company: " + employee.Company, font, XBrushes.Black, createLine(pdfPage, 3), XStringFormats.TopLeft);
           
            
            string pdfFilename = report_name + ".pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
            return pdf;
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
