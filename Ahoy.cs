using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Schedule
{
  public partial class Ahoy : Form
  {
    public Ahoy()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // Всплывающее окно с вопросом о загрузке таблицы
      var result = MessageBox.Show("Синхронизировать расписание с сайтом?", "Загрузка расписания", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      Classes.AllLinks Links = new Classes.AllLinks();
      if (result == DialogResult.Yes) // Если нажали Да, то скачивается расписание
      {
        WebClient downloader = new WebClient();
        string path = @"C:\Program FIles\Ahoy\";

        // Проверяем, существует ли директория 
        if (Directory.Exists(path) == false) // Если нет, то создаём
        {
          Directory.CreateDirectory(path);
          downloader.DownloadFile(Links.GetLink("biso0219"), path + "Schedule.xlsx");
        }
        else // Иначе просто скачиваем
        {
          downloader.DownloadFile(Links.GetLink("biso0219"), path + "Schedule.xlsx");
        }


      } // Если нажали кнопку Да
      

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}
