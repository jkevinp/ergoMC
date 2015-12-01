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
    /// Interaction logic for datagrid.xaml
    /// </summary>
    public partial class datagrid : Window
    {
        public datagrid()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //private void ImportExcel(string filename)
        //{
        //    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        //    xla.Visible = true;

        //    // Load the workbook
        //    Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Open(filename);

        //    // Get the worksheet
        //    Microsoft.Office.Interop.Excel.Worksheet ws = wb.Worksheets[1];

        //    // Retrieve number of columns, number of rows
        //    int numberOfColumns = GetNumberOfColumns(ws);
        //    int numberOfRows = GetNumberOfRows(ws);

        //    for (int r = 1; r <= numberOfRows; r++)
        //    {
        //        // Put your own code here to add the listviewitem
               
        //        ListViewItem item = lv.Items.Add(ws.Cells[r, 1].Value);

        //        for (int c = 1; c <= numberOfColumns; c++)
        //        {
        //            // Put your own code here to add the subitems
                    
        //            lv.Items.Add(ws.Cells[r, c].Value.ToString());
        //        }
        //    }

        //    // Quit Excel
        //    xla.Quit();
        //    xla = null;
        //}

        ///// <summary>
        ///// Gets the number of rows by searching the first null row
        ///// </summary>
        ///// <param name="ws">worksheet to search</param>
        ///// <returns>number of rows</returns>
        ///// <remarks>there probably is a better way to do this</remarks>
        //private int GetNumberOfRows(Microsoft.Office.Interop.Excel.Worksheet ws)
        //{
        //    int numberOfRows = 1;
        //    while (ws.Cells[1, numberOfRows].Value != null)
        //    {
        //        numberOfRows += 1;
        //    }
        //    numberOfRows -= 1; // substract 1 to get the last filled column

        //    return numberOfRows;
        //}

        ///// <summary>
        ///// Gets the number of columns by searching the first null row
        ///// </summary>
        ///// <param name="ws">worksheet to search</param>
        ///// <returns>number of columns</returns>
        ///// <remarks>there probably is a better way to do this</remarks>
        //private int GetNumberOfColumns(Microsoft.Office.Interop.Excel.Worksheet ws)
        //{
        //    int numberOfColumns = 1;
        //    while (ws.Cells[1, numberOfColumns].Value != null)
        //    {
        //        numberOfColumns += 1;
        //    }
        //    numberOfColumns -= 1; // substract 1 to get the last filled column

        //    return numberOfColumns;
        //}
    }
}
