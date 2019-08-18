using System.Net;
using System.Windows.Forms;

namespace TestTaskHHru
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * This program collects and sorts vacancy data from "hh.ru/catalog/Prodazhi":
             * 1. Parser collects strings with company name and salary;
             * 2. Program divides collected strings into components: 
             * name, title, description, min/max-salary (if exists) and publication date.
             * 3. Program calculates average salary from collected data;
             * 4. If vacancy's salary does not differ by 10% from the average then it will be transferred into database;
             * 5. Load collected data from database and show it to user.
             */
            WebClient wc = new WebClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
