// Thx Ukushu for library source (Library has been taken from https://github.com/ukushu/DataExporter/blob/master/Excel.cs)
using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Web;
using System.Text;
using System.IO;


namespace Ahoy.Classes
{
  public class Excel
  {
    private List<List<string>> Rows = new List<List<string>>(); // Таблица из Excel в виде двумерного списка
    private Dictionary<string, List<string>> institutes = new Dictionary<string, List<string>>();
    public void FileOpen(string path) // Открываем файл -> заполняем список
    {

      var workbook = new XLWorkbook(path);
      var ws1 = workbook.Worksheet(1);

      foreach (var xlRow in ws1.RangeUsed().Rows())
      {
        Rows.Add(new List<string>());
        foreach (var xlCell in xlRow.Cells())
        {
          var formula = xlCell.FormulaA1;
          var value = xlCell.Value.ToString();

          string targetCellValue = (formula.Length == 0) ? value : "=" + formula;

          Rows[Rows.Count - 1].Add(targetCellValue);
        }
      }      
    }

    public void DownloadExcel()
    {
      Links linksDownloader = new Links();
      Console.WriteLine("Downloading excels");
      if (!Directory.Exists(Environment.CurrentDirectory + @"\Excels\"))
      {
        Directory.CreateDirectory(Environment.CurrentDirectory + @"\Excels\");

      }
    }

    public List<string> EvenWeek(string _institute, string _groupTag)
    {
      
      return
    }

    public void ClearFile() // Очищаем список
    {
      Rows.Clear();
    }
  }
}