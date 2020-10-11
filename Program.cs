using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Test
{
  class Program
  {
    static void Unevenweek(Excel _excel, int _indexGroup)
    {      
      string output = string.Empty;
      output = _excel.Rows[1][_indexGroup].Substring(0, _excel.Rows[1][_indexGroup].IndexOf(' '));
      Console.WriteLine(output);
      int counter = 3;
      for (int i = 3; i < 74; i += 2)
      {
        output = _excel.Rows[i][1] + ' ' + _excel.Rows[i][_indexGroup] + '\t' + _excel.Rows[i][_indexGroup + 1] + '\t' + _excel.Rows[i][_indexGroup + 2];
        Console.WriteLine(output);
        counter += 2;
        if (counter % 15 == 0)
        {
          Console.WriteLine("\n---------------------------------------------------\n");
          counter = 3;
        }
      }
    } // Нечёт

    static void Evenweek(Excel _excel, int _indexGroup)
    {
      string output = string.Empty;
      output = _excel.Rows[1][_indexGroup].Substring(0, _excel.Rows[1][_indexGroup].IndexOf(' '));
      Console.WriteLine(output);
      int counter = 4;
      for (int i = 4; i < 74; i += 2)
      {
        output = _excel.Rows[i - 1][1] + ' ' + _excel.Rows[i][_indexGroup] + '\t' + _excel.Rows[i][_indexGroup + 1] + '\t' + _excel.Rows[i][_indexGroup + 2];
        Console.WriteLine(output);
        counter += 2;
        if (counter % 16 == 0)
        {
          Console.WriteLine("\n---------------------------------------------------\n");
          counter = 4;
        }
      }
    } // Чёт
    static void Main()
    {
      WebClient Schedule = new WebClient();
      string url = "http://webservices.mirea.ru/upload/iblock/feb/%D0%9A%D0%91%D0%B8%D0%A1%D0%9F%201%20%D0%BA%D1%83%D1%80%D1%81%201%20%D1%81%D0%B5%D0%BC.xlsx";
      string path = @"C:\Program Files\Ahoy\";
      string title = "Schedule.xlsx";
      bool directoryExistChecker = Directory.Exists(path);
      if (directoryExistChecker == true)
      {
        Schedule.DownloadFile(url, path + title);
      }
      else
      {
        Directory.CreateDirectory(path);
        Schedule.DownloadFile(url, path + title);
      }
      Excel excel = new Excel();
      excel.FileOpen(path + title);      
      int indexGroup = 5;
      bool checkCondition = false;
      while (true)
      {
        ConsoleKeyInfo key = new ConsoleKeyInfo();
        key = Console.ReadKey();
        if (key.Key == ConsoleKey.LeftArrow && indexGroup > 5)
        {
          indexGroup -= 5;
          Console.Clear();
          checkCondition = false;
        }
        else if (key.Key == ConsoleKey.RightArrow && indexGroup < 355)
        {
          indexGroup += 5;
          Console.Clear();
          checkCondition = false;
        }

        if (key.Key == ConsoleKey.Escape)
          break;

        if (key.Key == ConsoleKey.Spacebar)
        {
          if (checkCondition == false)
          {
            if (indexGroup % 20 == 0 && indexGroup > 5)
            {
              indexGroup += 5;
              Console.Clear();
              Unevenweek(excel, indexGroup);
              checkCondition = true;
            }
            else
            {
              Console.Clear();
              Unevenweek(excel, indexGroup);
              checkCondition = true;
            }
          }
          else if (checkCondition == true)
          {
            if (indexGroup % 20 == 0 && indexGroup > 5)
            {
              indexGroup += 5;
              Console.Clear();
              Evenweek(excel, indexGroup);
              checkCondition = false;
            }
            else
            {
              Console.Clear();
              Evenweek(excel, indexGroup);
              checkCondition = false;
            }
          }
        }
      }
    }
  }
}
