using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTaskHHru
{
    class DBWorker
    {
        private string connstring = String.Format("Server={0};Port={1};" + 
            "User Id={2};Password={3};Database={4};",
            "localhost", 5432, "postgres",
            "admin", "TestTaskHHru");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        public DBWorker()
        {
            conn = new NpgsqlConnection(connstring);
        }

        public DataTable Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from vc_select();";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select error: " + ex.Message + " " + ex.Data);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert(Vacancy vacancy)
        {
            int result = 0;
            try
            {
                conn.Open();
                sql = @"select * from vc_insert(:Title, :Name, :Description, :Date, :Price_min, :Price_max)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("Title", vacancy.VacancyTitle);
                cmd.Parameters.AddWithValue("Name", vacancy.VacancyName);
                cmd.Parameters.AddWithValue("Description", vacancy.VacancyDescription);
                cmd.Parameters.AddWithValue("Date", vacancy.VacancyPublicationDate);
                cmd.Parameters.AddWithValue("Price_min", vacancy.VacansySalaryMin);
                cmd.Parameters.AddWithValue("Price_max", vacancy.VacansySalaryMax);
                result = (int)cmd.ExecuteScalar();
                if (result == 0)
                {
                    MessageBox.Show("Insert Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert error: " + ex.Message + " " + ex.Data);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteAll()
        {
            try
            {
                conn.Open();
                sql = @"TRUNCATE Vacancies";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message + " " + ex.Data);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
