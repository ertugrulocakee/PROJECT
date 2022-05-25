using HtmlAgilityPack;
using System.Linq;

namespace PROJECTwebapi.Model
{
    public static class GetPrice
    {

        public static string Price(string detailLink)
        {

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(detailLink);


            if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='classifiedInfo']//h3") != null)
            {

                return htmlDocument.DocumentNode.SelectSingleNode("//div[@class='classifiedInfo']//h3").InnerText;

            }


            return "0 TL";
        }

    }
}
