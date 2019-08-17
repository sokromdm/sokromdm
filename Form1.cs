using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestTaskHHru.Parser;
using TestTaskHHru.Parser.HeadHunter;

namespace TestTaskHHru
{
    public partial class Form1 : Form
    {

        ParserWorker<Vacancy[]> parser;
        List<Vacancy> Vacancies = new List<Vacancy>();
        DBWorker dataBase = new DBWorker();

        public Form1()
        {
            InitializeComponent();

            parser = new ParserWorker<Vacancy[]>(
                new HHParser()
                );

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, Vacancy[] arg2)
        {
            
            foreach (var item in arg2)
            {
                dataBase.Insert(item);
                //Console.WriteLine(item.getVacansyPriceMax() + " " + item.getVacansyPriceMin());
                ListTitels.Items.Add(item.getVacancyTitle());
            }
        }

        private void Parser_OnCompleted(object obj)
        {
            //MessageBox.Show("All works done!");
            dgvData.DataSource = null;
            
            dgvData.DataSource = dataBase.Select();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            parser.Settings = new HHSettings((int)NumberStart.Value, (int)NumberEnd.Value);
            parser.Start();

            dataBase.DeleteAll();
        }

        private void ButtonAbort_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }
    }
}
