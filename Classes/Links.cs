using System;
using System.Diagnostics; // Для запуска exe файла, который качает html код
using System.IO;
using System.Collections.Generic;
using System.Text; // Для Encoding (чтобы русские буквы вписывались в файл нормально)

namespace Ahoy.Classes
{
  // Класс в котором будут оформлены: Скачивание кода html-страницы "mirea.ru/schedule", Вытаскивание ссылок из кода, Сортировка ссылок по группам
  class Links 
  {
    private bool isSuccessfullyDownloaded; // Переменная для проверки скачан ли код (true, если скачан)
    private Dictionary<string, List<string>> sortedLinks = new Dictionary<string, List<string>>(); // Словарь типа ["Группа"] = {"Ссылка", "Ссылка", ...}
    private List<string> unsortedLinks = new List<string>(); // Список со всеми ссылками

    public Links(bool _needToDownload) 
    {
      if (_needToDownload) // Если надо скачивать
      {
        isSuccessfullyDownloaded = true; // Допустим файл сработал успешно
        try
        {
          Process.Start(@Environment.CurrentDirectory + @"\downloader.exe");
        }
        catch (Exception e)
        {
          isSuccessfullyDownloaded = false; // Если возникла ошибка в работе файла
          Console.WriteLine($"Ошибка программы скачивания расписания. Сообщение компилятора: {e}");
        }
      }
    } 
    
        public List<string> UnsortedLinks
        {
            get { return unsortedLinks; }
        }

    // Заносим скачанные ссылки в словарь
    public void GetLinks()
    {
      if (isSuccessfullyDownloaded)
      {
        StreamReader htmlCode = new StreamReader(@Environment.CurrentDirectory + @"\downloadedlinks.txt", Encoding.UTF8); // Открываем файл, в котором должны быть ссылки, для чтения
        string linksSearcher = htmlCode.ReadToEnd(); // Считываем файл в строку (не StringBuilder, потому что там нет операции indexOf (и не только))
        string linkToAdd = String.Empty;
        unsortedLinks.Clear();

        while (linksSearcher.IndexOf(@"http://webservices.mirea.ru/") != -1) // Пока есть "http://webservices.mirea.ru/"
        {
          linksSearcher = linksSearcher.Substring(linksSearcher.IndexOf(@"http://webservices.mirea.ru/")); // Обрезаем всё, что было до ссылки
          linkToAdd = linksSearcher.Substring(0, linksSearcher.IndexOf('"')); // Записываем ссылку
          linksSearcher = linksSearcher.Substring(linksSearcher.IndexOf('"')); // Стираем ссылку
          if (linkToAdd.IndexOf("xlsx") != -1) // Если ссылка на файл .xlsx добавляем ссылку в список
          {
            unsortedLinks.Add(linkToAdd);
          }
        }
        htmlCode.Close();

        // Записываем данные из неотсортированного списка в словарь типа [Группа] = Ссылка     
        for (int i = 0; i < unsortedLinks.Count; ++i)
        {
          if (unsortedLinks[i].IndexOf("ФТИ") != -1) // Если в ссылке есть ФТИ, сохраняем ссылку в ФТИ
          {
            if (sortedLinks.ContainsKey("ФТИ") == false) // Если ФТИ ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ФТИ", temp);
            }
            else
              sortedLinks["ФТИ"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИИНТЕГУ") != -1) // Если в ссылке есть ИИНТЕГУ, сохраняем ссылку в ИИНТЕГУ
          {
            if (sortedLinks.ContainsKey("ИИНТЕГУ") == false) // Если ИИНТЕГУ ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИИНТЕГУ", temp);
            }
            else
              sortedLinks["ИИНТЕГУ"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИИТ") != -1) // Если в ссылке есть ИИТ, сохраняем ссылку в ИИТ
          {
            if (sortedLinks.ContainsKey("ИИТ") == false) // Если ИИТ ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИИТ", temp);
            }
            else
              sortedLinks["ИИТ"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИК") != -1) // Если в ссылке есть ИК, сохраняем ссылку в ИК
          {
            if (sortedLinks.ContainsKey("ИК") == false) // Если ИК ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИК", temp);
            }
            else
              sortedLinks["ИК"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("КБиСП") != -1) // Если в ссылке есть КБиСП, сохраняем ссылку в КБиСП
          {
            if (sortedLinks.ContainsKey("КБиСП") == false) // Если КБиСП ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("КБиСП", temp);
            }
            else
              sortedLinks["КБиСП"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИРТС") != -1) // Если в ссылке есть ИРТС, сохраняем ссылку в ИРТС
          {
            if (sortedLinks.ContainsKey("ИРТС") == false) // Если ИРТС ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИРТС", temp);
            }
            else
              sortedLinks["ИРТС"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИТХТ") != -1) // Если в ссылке есть ИТХТ, сохраняем ссылку в ИТХТ
          {
            if (sortedLinks.ContainsKey("ИТХТ") == false) // Если ИТХТ ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИТХТ", temp);
            }
            else
              sortedLinks["ИТХТ"].Add(unsortedLinks[i]);
          }
          if (unsortedLinks[i].IndexOf("ИЭП") != -1) // Если в ссылке есть ИЭП, сохраняем ссылку в ИЭП
          {
            if (sortedLinks.ContainsKey("ИЭП") == false) // Если ИЭП ещё не создан, то создаём
            {
              List<string> temp = new List<string>();
              temp.Add(unsortedLinks[i]);
              sortedLinks.Add("ИЭП", temp);
            }
            else
              sortedLinks["ИЭП"].Add(unsortedLinks[i]);
          }
        }
      }
      else // Если downloader не сработал, то вызываем исключение
        try
        {
          throw new Exception("Downloader didn't execute");
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
        }
    }
   
    // Заносим старые ссылки в словарь
    public void GetLinksOffline()
    {
      StreamReader linksWatcher = new StreamReader(@Environment.CurrentDirectory + @"\pulledlinks.txt", Encoding.UTF8); // Открываем файл с ссылками для чтения      
      unsortedLinks.Clear(); // Очищаем список (на всякий случай)
      // Переписываем ссылки из файла в список
      while (true)
      {
        string line = linksWatcher.ReadLine();
        if (line == null)
          break;
        unsortedLinks.Add(line);        
        line = String.Empty;
      }    

      for (int i = 0; i < unsortedLinks.Count; ++i)
      {
        if (unsortedLinks[i].IndexOf("ФТИ") != -1) // Если в ссылке есть ФТИ, сохраняем ссылку в ФТИ
        {
          if (sortedLinks.ContainsKey("ФТИ") == false) // Если ФТИ ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ФТИ", temp);            
          }
          else
            sortedLinks["ФТИ"].Add(unsortedLinks[i]);         
        }
        if (unsortedLinks[i].IndexOf("ИИНТЕГУ") != -1) // Если в ссылке есть ИИНТЕГУ, сохраняем ссылку в ИИНТЕГУ
        {
          if (sortedLinks.ContainsKey("ИИНТЕГУ") == false) // Если ИИНТЕГУ ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИИНТЕГУ", temp);            
          }
          else
            sortedLinks["ИИНТЕГУ"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("ИИТ") != -1) // Если в ссылке есть ИИТ, сохраняем ссылку в ИИТ
        {
          if (sortedLinks.ContainsKey("ИИТ") == false) // Если ИИТ ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИИТ", temp);            
          }
          else
            sortedLinks["ИИТ"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("ИК") != -1) // Если в ссылке есть ИК, сохраняем ссылку в ИК
        {
          if (sortedLinks.ContainsKey("ИК") == false) // Если ИК ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИК", temp);            
          }
          else
            sortedLinks["ИК"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("КБиСП") != -1) // Если в ссылке есть КБиСП, сохраняем ссылку в КБиСП
        {
          if (sortedLinks.ContainsKey("КБиСП") == false) // Если КБиСП ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("КБиСП", temp);            
          }
          else
            sortedLinks["КБиСП"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("ИРТС") != -1) // Если в ссылке есть ИРТС, сохраняем ссылку в ИРТС
        {
          if (sortedLinks.ContainsKey("ИРТС") == false) // Если ИРТС ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИРТС", temp);            
          }
          else
            sortedLinks["ИРТС"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("ИТХТ") != -1) // Если в ссылке есть ИТХТ, сохраняем ссылку в ИТХТ
        {
          if (sortedLinks.ContainsKey("ИТХТ") == false) // Если ИТХТ ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИТХТ", temp);            
          }
          else
            sortedLinks["ИТХТ"].Add(unsortedLinks[i]);
        }
        if (unsortedLinks[i].IndexOf("ИЭП") != -1) // Если в ссылке есть ИЭП, сохраняем ссылку в ИЭП
        {
          if (sortedLinks.ContainsKey("ИЭП") == false) // Если ИЭП ещё не создан, то создаём
          {
            List<string> temp = new List<string>();
            temp.Add(unsortedLinks[i]);
            sortedLinks.Add("ИЭП", temp);            
          }
          else
            sortedLinks["ИЭП"].Add(unsortedLinks[i]);
        }
      }

      
    }

    // Возвращаем список ссылок
    public List<string> UnsortedLinks 
    {
      get { return unsortedLinks; }
    }

    // Получаем ссылки из словаря
    public List<string> GetValues (string key)
    {      
      return sortedLinks[key];
    }    
  }
}
