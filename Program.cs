using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using TestTaskHHru.Parser;
using TestTaskHHru.Parser.HeadHunter;

/*
TODO:
1) переименовать Price на Salary в программе и в бд
2) убрать функции из БД и заменить на простые выражения SQL 
3) заменить простой insert на массовый
4) отметать вакансии без з/п на стадии парсинга
*/
namespace TestTaskHHru
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * This program collects and sorts vacancy data from "hh.ru/catalog/Prodazhi":
             * 1. Parser collects strings with company name and salary;
             * 2. Program divides collected strings into components: name, min-salary (if exists), max-salary. Strings without salary will be deleted;
             * 3. Program calculates average salary from collected data;
             * 4. If the salary does not differ by 10% from the average then it will be transfered into database;
             * 5. Open a window and show collected data from database for user.
             */
            WebClient wc = new WebClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
