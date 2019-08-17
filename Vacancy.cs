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
        private bool noPrice = false;

        string vacancyTitle;
        public string getVacancyTitle() { return vacancyTitle; }

        string vacancyName;
        public string getVacancyName() { return vacancyName; }

        string vacancyDescription;
        public string getVacancyDescription() { return vacancyDescription; }

        string vacancyPublicationDate;
        public string getVacancyPublicationDate() { return vacancyPublicationDate; }

        int vacansyPriceMin;
        public int getVacansyPriceMin() { return vacansyPriceMin; }

        int vacansyPriceMax;
        public int getVacansyPriceMax() { return vacansyPriceMax; }

        public Vacancy(string vacancyTitle, string vacancyName, string vacancyDescription, string vacancyPublicationDate, string vacancyPrice)
        {
            this.vacancyTitle = vacancyTitle;
            this.vacancyName = vacancyName;
            this.vacancyDescription = vacancyDescription;
            this.vacancyPublicationDate = vacancyPublicationDate;
            ProcessPriceString(vacancyPrice);
        }

        /*
        Method sorts string with price information
        Price can be in formats: 
        empty ("");
        min price (от 12 000 руб.); 
        max price (до 20 000 руб.); 
        range (10 000-12 000 руб.);
        */    
        private void ProcessPriceString(string vacancyPrice)
        {
            Regex regex = new Regex(@"( руб.)");
            Regex cleanceRegex = new Regex(@" ");
            if (vacancyPrice != null)
            {
                MatchCollection matches = regex.Matches(vacancyPrice);

                if (regex.IsMatch(vacancyPrice))
                {
                    //Getting rid of symbol " " between numbers
                    var sortedString = cleanceRegex.Replace(vacancyPrice, "");

                    //Getting rid of "руб."
                    sortedString = regex.Replace(sortedString, "");

                    Console.WriteLine(sortedString);
                    Regex rangedPrice = new Regex(@"(\d*)(?:-)(\d*)");
                    Regex singlePrice = new Regex(@"(?:от )(\n*)");


                }
            }
            else
            {
                noPrice = true;
            }
        }

        // Returns a list of all vacancy info
        public List<string> VacansyStrings()
        {
            return (new List<string>
            {
                vacancyTitle,
                vacancyName,
                vacancyDescription,
                vacancyPublicationDate,
                vacansyPriceMin.ToString(),
                vacansyPriceMax.ToString()
            });
        } 

        
    }
}
