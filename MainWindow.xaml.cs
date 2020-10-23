using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ahoy
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    static int indexGroup = 5;
    static string defaultInstitute = "КБиСП";
    static int day = 0;
    static List<string> days = new List<string>();

    static bool AskToDownload()
    {
      Classes.Links downloader = new Classes.Links(true);

      List<string> comporator = downloader.UnsortedLinks;

      string path = @"Environment.CurrentDirectory" + @"\links.txt";

      string[] readText = File.ReadAllLines(path);

      bool checker = false;

      for (int i = 0; i < readText.Length; i++)
      {
        if (comporator[i] != readText[i])
        {
          MessageBoxResult result = MessageBox.Show("Wanna Download?", "Downloader", MessageBoxButton.YesNo, MessageBoxImage.Question);

          if (result == MessageBoxResult.Yes)
          {
            checker = true;
          }
        }
      }
      return checker;
    }

    public MainWindow()
    {
      InitializeComponent();
      Classes.Excel sheet = new Classes.Excel(); // Расим, вот эту штуку надо будет перенести на самый верх этого конструктора, а потом написать тот обработчик, о котором я тебе в вк написал.

      sheet.FileOpen(@Environment.CurrentDirectory + @"\" + $"{defaultInstitute}" + @"\" + $"{defaultInstitute}" + "_1.xlsx");
      
      // Ищем индекс группы в таблице
      while (sheet.Rows[1][indexGroup].IndexOf("20") == -1)
      {
        if (sheet.Rows[1][indexGroup].IndexOf("БИСО-02-20") != -1)
          break;
        indexGroup += 5;
      }  

      // Записываем расписание на чётную неделю для группы      
      days = sheet.EvenWeek(defaultInstitute, indexGroup);

      this.PanelsCreation(days);
    }

    // С помощью этого метода мы будем выводить расписание
    private void PanelsCreation(List<string> _days)
    {
      if (Lessons.Children.Count == 0)
      {
        Button newCell = new Button();
        newCell.Content = _days[0];
        Lessons.Children.Add(newCell);
      }
      if (day > 0 && day < _days.Count)
      {        
        for (int i = day; i < day + 6; ++i)
        {
          Button newCell = new Button();
          newCell.Content = _days[i].Substring(1,); // Дописать
          Lessons.Children.Add(newCell);
        }
      }      
    }

    private void NextClick(object sender, RoutedEventArgs e)
    {   
      if (day < 6)
         day += 6; // Переходим на день вперёд      
      Lessons.Children.Clear();      
      this.PanelsCreation(days);
    }

    private void Previous_Click(object sender, RoutedEventArgs e)
    {
      if (day > 1)
        day -= 6; // Переходим на день назад
      Lessons.Children.Clear();
      this.PanelsCreation(days);
    }
  }
}
