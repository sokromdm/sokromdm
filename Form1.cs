using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestTaskHHru.Parser;
using TestTaskHHru.Parser.HeadHunter;

namespace TestTaskHHru
{
    public partial class Form1 : Form
    {

        ParserWorker<List<Vacancy>> parser;
        List<Vacancy> vacancies = new List<Vacancy>();
        DBWorker dataBase = new DBWorker();
        int averageSalary;

        public Form1()
        {
            InitializeComponent();

            parser = new ParserWorker<List<Vacancy>>(
                new HHParser()
                );

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, List<Vacancy> arg2)
        {
            foreach (var item in arg2)
            {
                vacancies.Add(item);
            }

        }

        private void Parser_OnCompleted(object obj)
        {
            ListTitels.Items.Add("Parsing complete!");
            averageSalary = CalculateAverageSalary(vacancies);
            ListTitels.Items.Add("Average salary from collected data: " + averageSalary + " (" + averageSalary*0.9 + " - " + averageSalary*1.1 + ")");
            ListTitels.Items.Add("Adding vacancies into the Database...");
            foreach (Vacancy v in vacancies)
            {
                if (!(v.VacansySalaryMin==0 && v.VacansySalaryMax==0))
                {
                    if (!(v.VacansySalaryMin == 0) && (v.VacansySalaryMax == 0))
                    {
                        if ((v.VacansySalaryMin < averageSalary * 1.1) && (v.VacansySalaryMin > averageSalary * 0.9))
                        {
                            dataBase.Insert(v);
                        }
                    }
                    else if ((v.VacansySalaryMin == 0) && !(v.VacansySalaryMax == 0))
                    {
                        if ((v.VacansySalaryMax < averageSalary * 1.1) && (v.VacansySalaryMax > averageSalary * 0.9))
                        {
                            dataBase.Insert(v);
                        }
                    }
                    else if (!(v.VacansySalaryMin == 0) && !(v.VacansySalaryMax == 0))
                    {
                        if (((v.VacansySalaryMax < averageSalary * 1.1) && (v.VacansySalaryMax > averageSalary * 0.9)) ||
                            ((v.VacansySalaryMin < averageSalary * 1.1) && (v.VacansySalaryMin > averageSalary * 0.9)) ||
                            ((v.VacansySalaryMin > averageSalary * 0.9) && (v.VacansySalaryMax < averageSalary * 1.1)) ||
                            ((v.VacansySalaryMin < averageSalary * 0.9) && (v.VacansySalaryMax > averageSalary * 1.1)))
                        {
                            dataBase.Insert(v);
                        }
                    }
                }
            }
            ListTitels.Items.Add("Vacancies was successfully transfered into the Database.");
            ListTitels.Items.Add("Loading table into the viever...");
            dgvData.DataSource = null;
            dgvData.DataSource = dataBase.Select();
            ListTitels.Items.Add("Done!");
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ListTitels.Items.Add("Attempt to parse from page " + NumberStart.Value + " to " + NumberEnd.Value + "...");
            parser.Settings = new HHSettings((int)NumberStart.Value, (int)NumberEnd.Value);
            parser.Start();
            dataBase.DeleteAll();
        }

        private void ButtonAbort_Click(object sender, EventArgs e)
        {
            parser.Abort();
            ListTitels.Items.Add("Parsing stopped.");
        }

        private int CalculateAverageSalary(List<Vacancy> list)
        {
            int average = 0;
            int count = 0;
            foreach (Vacancy v in list)
            {
                if (v.VacansySalaryMin != 0)
                {
                    average += v.VacansySalaryMin;
                    count++;
                }
                if (v.VacansySalaryMax != 0)
                {
                    average += v.VacansySalaryMax;
                    count++;
                }
            }
            if (count>0) average = average / count;
            else ListTitels.Items.Add("No vacancies found.");
            return average;
        }
    }
}
