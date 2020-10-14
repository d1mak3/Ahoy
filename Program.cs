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
            string output = _excel.Rows[1][_indexGroup].Substring(0, _excel.Rows[1][_indexGroup].IndexOf(' ')); // Выводим название группы
            Console.WriteLine(output);
            int counter = 3;
            for (int i = 3; i < 74; i += 2)
            {
                output = _excel.Rows[i][1] + ' ' + _excel.Rows[i][_indexGroup] + '\t' + _excel.Rows[i][_indexGroup + 1] + '\t' + _excel.Rows[i][_indexGroup + 2] + '\t' + _excel.Rows[i][_indexGroup + 3]; // Выводим строку: Номер пары ' ' Название пары '\t' Тип пары '\t' Препод
                Console.WriteLine(output);
                counter += 2;
                if (counter % 15 == 0) // Разделяем дни пунктиром
                {
                    Console.WriteLine("\n---------------------------------------------------\n");
                    counter = 3;
                }
            }
        } // Нечёт

        static void Evenweek(Excel _excel, int _indexGroup)
        {
            string output = _excel.Rows[1][_indexGroup].Substring(0, _excel.Rows[1][_indexGroup].IndexOf(' ')); // Выводим название группы
      Console.WriteLine(output);
            int counter = 4;
            for (int i = 4; i < 74; i += 2)
            {
                output = _excel.Rows[i - 1][1] + ' ' + _excel.Rows[i][_indexGroup] + '\t' + _excel.Rows[i][_indexGroup + 1] + '\t' + _excel.Rows[i][_indexGroup + 2]; // Выводим строку: Номер пары ' ' Название пары '\t' Тип пары '\t' Препод
        Console.WriteLine(output);
                counter += 2;
                if (counter % 16 == 0) // Разделяем дни пунктиром
        {
                    Console.WriteLine("\n---------------------------------------------------\n");
                    counter = 4;
                }
            }
        } // Чёт
        static void Main()
        {
            WebClient Schedule = new WebClient();
            string url = "http://webservices.mirea.ru/upload/iblock/9ae/%D0%9A%D0%91%D0%B8%D0%A1%D0%9F%202%20%D0%BA%D1%83%D1%80%D1%81%201%20%D1%81%D0%B5%D0%BC.xlsx"; // Ссылка на расписание 2 курса ИКБСП
            string path = @"C:\Program Files\Ahoy\"; // Путь к директории, в которой будет храниться файл
            string title = "Schedule.xlsx"; // Название файла
            bool directoryExistChecker = Directory.Exists(path); // Проверяем, есть ли директория
            if (directoryExistChecker == true) // Если есть, то просто качаем файл
            {
                Schedule.DownloadFile(url, path + title);
            }
            else // Если нет, то создаём папку и качаем файл
            {
                Directory.CreateDirectory(path);
                Schedule.DownloadFile(url, path + title);
            }
            Excel excel = new Excel(); // Объект excel, в котором будет храниться таблица
            excel.FileOpen(path + title); // Открываем таблицу
            int indexGroup = 5; // Расстояние между названиями групп
            bool checkCondition = false; // Переменная для проверки на какой неделе мы сейчас находимся
            while (true)
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo(); // Объект класса ConsoleKeyInfo, чтобы понять какую именно кнопку нажали
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow && indexGroup > 5) // Если нажали влево и при этом мы не на самой первой группе, то переходим на группу назад (на 5 ячеек назад)
                {
                    indexGroup -= 5;
                    Console.Clear();
                    checkCondition = false;
                }
                else if (key.Key == ConsoleKey.RightArrow && indexGroup < 355) // Если нажали вправо и при этом мы не на самой последней группе, то переходим на группу вперёд (на 5 ячеек вперёд)
                {
                    indexGroup += 5;
                    Console.Clear();
                    checkCondition = false;
                }

                if (key.Key == ConsoleKey.Escape) // Если нажали Escape, то программа выключается
                    break;

                if (key.Key == ConsoleKey.Spacebar) // Если нажали на пробел, то меняется неделя
                {
                    if (checkCondition == false) // Если сейчас чётная, то выводим нечетную
                    {
                        if (indexGroup % 20 == 0 && indexGroup > 5) // Т.к. раз в 4 группы расписание съезжает немного вправо, надо раз в 4 группы переносить индекс вперёд
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
                    else if (checkCondition == true) // Если сейчас нечетная, то выводим четную
                    {
                        if (indexGroup % 20 == 0 && indexGroup > 5) // Т.к. раз в 4 группы расписание съезжает немного вправо, надо раз в 4 группы переносить индекс вперёд
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
