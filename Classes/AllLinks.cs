using System.Collections.Generic;

namespace Schedule.Classes
{
  public class AllLinks // Класс, в котором хранятся ссылки на файлы расписания
  {
    private Dictionary<string, string> dataLinks; // Словарь, в котором Ключ - название группы, а Значение - ссылка на файл расписания
    public AllLinks()
    {
      dataLinks = new Dictionary<string, string>();
      dataLinks.Add("biso0219", @"http://webservices.mirea.ru/upload/iblock/454/%D0%9A%D0%91%D0%B8%D0%A1%D0%9F%202%20%D0%BA%D1%83%D1%80%D1%81%201%20%D1%81%D0%B5%D0%BC.xlsx"); 
    }

    public string GetLink(string key) // Функция, возвращающая значение ключа
    {
      return dataLinks[key];
    }
  }
}
