using AngleSharp.Html.Dom;

namespace TestTaskHHru.Parser
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
