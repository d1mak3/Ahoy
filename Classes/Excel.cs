// Thx Ukushu for library source (original library has been taken from https://github.com/ukushu/DataExporter/blob/master/Excel.cs)
using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;
using System.Net;

namespace Ahoy.Classes
{
  public class Excel
  {
    public readonly List<List<string>> Rows = new List<List<string>>(); // Таблица из Excel в виде двумерного списка
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

    public void DownloadExcels() // Скачиваем таблицы
    {      
      // Достаём ссылки
      Links linksDownloader = new Links(false);
      linksDownloader.GetLinksOffline();
      Dictionary<string, List<string>> sortedLinks = linksDownloader.SortedLinks;

      // Скачиваем по этим ссылкам таблицы
      Console.WriteLine("Downloading excels");
      if (!Directory.Exists(Environment.CurrentDirectory + @"\Excels\")) // Создаём основную директорию, если её нет
      {
        Directory.CreateDirectory(Environment.CurrentDirectory + @"\Excels\");        
      }
      DownloadFromLinks("ФТИ");
      DownloadFromLinks("ИИНТЕГУ");
      DownloadFromLinks("ИИТ");
      DownloadFromLinks("ИК");
      DownloadFromLinks("КБиСП");
      DownloadFromLinks("ИРТС");
      DownloadFromLinks("ИТХТ");
      DownloadFromLinks("ИЭП");
    }

    private static void DownloadFromLinks(string _keyForLinks)
    {
      // Подготовка к скачиванию табличек
      Links getLinks = new Links(false);
      if (!Directory.Exists(Environment.CurrentDirectory + @"\Excels\" + _keyForLinks)) // Создаём директорию для табличек
        Directory.CreateDirectory(Environment.CurrentDirectory + @"\Excels\" + _keyForLinks);
      getLinks.GetLinksOffline();
      List<string> links = getLinks.UnsortedLinks; // Вытаскиваем ссылки из класса в локальный список

      //Скачивание табличек
      int counter = 0;
      WebClient excelDownloader = new WebClient();
      foreach (string s in links)
      {
        excelDownloader.DownloadFile(s, @Environment.CurrentDirectory + @"\Excels\" + _keyForLinks + @"\" + _keyForLinks + $"_{counter}" + ".xlsx"); // Сохраняем файлы в формате типа: (пример) ФТИ_1.xlsx
      }

    }

    public List<string> EvenWeek(string _institute, int _indexGroup) // Список данных для чётной недели
    {
      List<string> output = new List<string>();
      output.Add(Rows[1][_indexGroup].Substring(0, Rows[1][_indexGroup].IndexOf(' '))); // Название группы
      for (int i = 4; i < 74; i += 2) // Пары
      {
        output.Add($"{Rows[i - 1][1]},{Rows[i][_indexGroup]},{Rows[i][_indexGroup + 1]},{Rows[i][_indexGroup + 2]}"); // Записываем: Номер пары,Название пары,Тип пары,Препод           
      }

      return output;
    }

    public List<string> UnEvenWeek(string _institute, string _groupTag, int _indexGroup) // Список данных для нечётной недели
    {
      List<string> output = new List<string>();
      output.Add(Rows[1][_indexGroup].Substring(0, Rows[1][_indexGroup].IndexOf(' '))); // Название группы
      for (int i = 3; i < 74; i += 2) // Пары
      {
        output.Add($"{Rows[i - 1][1]},{Rows[i][_indexGroup]},{Rows[i][_indexGroup + 1]},{Rows[i][_indexGroup + 2]}"); // Записываем: Номер пары,Название пары,Тип пары,Препод           
      }

      return output;
    }

    public void ClearFile() // Очищаем список
    {
      Rows.Clear();
    }
  }
}