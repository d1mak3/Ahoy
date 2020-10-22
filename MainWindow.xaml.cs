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
      
      While ()

      List<string> days = new List<string>();
      days = sheet.EvenWeek(defaultInstitute, "БИСО-02-20", indexGroup);
    }

    void NextClick(object sender, RoutedEventArgs e)
    {
      
    }
  }
}
