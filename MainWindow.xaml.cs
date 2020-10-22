using System;
using System.Collections.Generic;
using System.Drawing;
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
    static bool AskToDownload()
    {
            Classes.Links downloader = new Classes.Links();
            List<string> comporator = downloader.UnsortedLinks;
            
      MessageBoxResult result = MessageBox.Show("Wanna Download?", "Downloader", MessageBoxButton.YesNo, MessageBoxImage.Question);
      bool checker = false;
      if (result == MessageBoxResult.Yes)
      {
        checker = true;
      }
      return checker;
    }

    public MainWindow()
    {
      InitializeComponent();    
      
    }

    void NextClick(object sender, RoutedEventArgs e)
    {
    }
  }
}
