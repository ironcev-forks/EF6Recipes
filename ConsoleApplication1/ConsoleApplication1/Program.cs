using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Workbook book = new Workbook("test.xlsx");
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            sheet.Hyperlinks.Add("C4", 1, 1, "Sheet2!A43:C45");
            
            sheet.Hyperlinks[1].TextToDisplay = "工作经历";
            sheet.Hyperlinks[1].Address = "Sheet2!A43:C45";
            book.Save("test.xlsx");
        }
    }
}
