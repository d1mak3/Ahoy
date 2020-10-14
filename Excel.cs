// Thx Ukushu for library source (Library has been taken from https://github.com/ukushu/DataExporter/blob/master/Excel.cs)

using System.Collections.Generic;
using ClosedXML.Excel;

namespace Test
{
  public class Excel
  {
    public List<List<string>> Rows = new List<List<string>>(); // Таблица из Excel в виде двумерного списка
    
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

    public void ClearFile() // Очищаем список
    {
      Rows.Clear();
    }
  }
}