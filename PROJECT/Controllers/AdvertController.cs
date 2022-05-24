using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECTwebapi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetAdverts()
        {

            List<Advert> adverts = new List<Advert>();

            var url = "https://www.sahibinden.com/";

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(url);

           

            for (int i = 0; i<htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//span").Count;i++)
            {


                Advert advert = new Advert();

                advert.name = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//span").ElementAt(i).InnerText;

                advert.image = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//img").ElementAt(i).GetAttributeValue("src", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fpbs.twimg.com%2Fprofile_images%2F716487122224439296%2FHWPluyjs.jpg&f=1&nofb=1");

                advert.detailLink = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a").ElementAt(i).GetAttributeValue("href", "test123test.com");

                advert.detailLink = "https://www.sahibinden.com" + advert.detailLink;    

                adverts.Add(advert);

            }

            return Ok(adverts);

        }


    }
}