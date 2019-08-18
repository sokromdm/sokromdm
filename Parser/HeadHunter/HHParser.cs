using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace TestTaskHHru.Parser.HeadHunter
{
    class HHParser : IParser<List<Vacancy>>
    {

        public List<Vacancy> Parse(IHtmlDocument document)
        {
            var list = new List<Vacancy>();
            string title = "";
            string name = "";
            string description = "";
            string date = "";
            string salary = "";


            // Search for vacancies
            var fullVacancy = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName == "vacancy-serp-item ");

            // Sort and save info from each vacancy. Strings will be transformed in the Vacancy class methods.
            foreach (var item in fullVacancy)
            {
                title = ExtraxtText("div", "resume-search-item__name", item);
                name = ExtraxtText("a", "bloko-link bloko-link_secondary", item);
                description = ExtraxtText("div", "g-user-content", item);
                date = ExtraxtText("span", "vacancy-serp-item__publication-date", item);
                salary = ExtraxtText("div", "vacancy-serp-item__compensation", item);

                list.Add(new Vacancy(title, name, description, date, salary));
            }

            return list;
        }

        private string ExtraxtText(string className, string contains, AngleSharp.Dom.IElement source)
        {
            string result = null;
            var search = source.QuerySelectorAll(className).Where(iTitle => iTitle.ClassName != null && iTitle.ClassName.Contains(contains));
            foreach (var item in search) { result = item.TextContent; }
            return result;
        }
    }
}
