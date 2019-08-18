using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestTaskHHru
{
    class Vacancy
    {
        public string VacancyTitle {get; }
        public string VacancyName { get; }
        public string VacancyDescription { get; }
        public string VacancyPublicationDate { get; }
        public int VacansySalaryMin { get; set; }
        public int VacansySalaryMax { get; set; }

        public Vacancy(string vacancyTitle, string vacancyName, string vacancyDescription, string vacancyPublicationDate, string vacancyPrice)
        {
            this.VacancyTitle = vacancyTitle;
            this.VacancyName = vacancyName;
            this.VacancyDescription = vacancyDescription;
            this.VacancyPublicationDate = vacancyPublicationDate;
            ProcessSalaryString(vacancyPrice);
        }

        /*Method sorts string with price information
        Price can be in formats: 
        empty ("");
        min price (от 12 000 руб.); 
        max price (до 20 000 руб.); 
        range (10 000-12 000 руб.);*/
        private void ProcessSalaryString(string vacancyPrice)
        {
            Regex regex = new Regex(@"( руб.)");
            if (vacancyPrice != null)
            {
                MatchCollection matches = regex.Matches(vacancyPrice);
                if (regex.IsMatch(vacancyPrice))
                {
                    //Getting rid of symbol " " between numbers
                    Regex cleanceRegex = new Regex(@" ");
                    var sortedString = cleanceRegex.Replace(vacancyPrice, "");

                    //Getting rid of " руб."
                    sortedString = regex.Replace(sortedString, "");

                    // range 
                    regex = new Regex(@"(?<min>\d*)(?:-)(?<max>\d*)");
                    matches = regex.Matches(sortedString);
                    if (regex.IsMatch(vacancyPrice))
                    {
                        foreach (Match match in regex.Matches(sortedString))
                        {
                            VacansySalaryMin = Int32.Parse(match.Groups["min"].Value);
                            VacansySalaryMax = Int32.Parse(match.Groups["max"].Value);
                        }
                    }

                    //min
                    regex  = new Regex(@"(?:от )(?<min>\d*)");
                    matches = regex.Matches(sortedString);
                    if (regex.IsMatch(vacancyPrice))
                    {
                        foreach (Match match in regex.Matches(sortedString))
                        {
                            VacansySalaryMin = Int32.Parse(match.Groups["min"].Value);
                            VacansySalaryMax = 0;
                        }
                    }

                    //max
                    regex = new Regex(@"(?:до )(?<max1>\d*)");
                    matches = regex.Matches(sortedString);
                    if (regex.IsMatch(vacancyPrice))
                    {
                        foreach (Match match in regex.Matches(sortedString))
                        {
                            VacansySalaryMin = 0;
                            VacansySalaryMax = Int32.Parse(match.Groups["max1"].Value);
                        }
                    }
                }
            }
            else
            {
                VacansySalaryMin = VacansySalaryMax = 0;
            }
        }   
    }
}
