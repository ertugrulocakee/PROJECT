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

            List<Advert> adverts = new List<Advert>(); // İlan listesi tanımlandı.

            var url = "https://www.sahibinden.com/"; // Site URL tanımlandı.

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(url);

           

            for (int i = 0; i<htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//span").Count;i++) // Ana sayfa ilanlarını dolaşması için bir for döngüsü oluşturuldu.
            {


                Advert advert = new Advert(); // Bir ilan nesnesi oluşturuldu.

                advert.name = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//span").ElementAt(i).InnerText; // Oluşturulan ilan nesnesinin ad özniteliğine sayfadaki ilgili değer atandı.

                advert.image = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a//img").ElementAt(i).GetAttributeValue("src", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fpbs.twimg.com%2Fprofile_images%2F716487122224439296%2FHWPluyjs.jpg&f=1&nofb=1"); // Oluşturulan ilan nesnesinin görsel özniteliğine sayfadaki ilgili değer atandı.

                advert.detailLink = htmlDocument.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li//a").ElementAt(i).GetAttributeValue("href", "test123test.com"); // Oluşturulan ilan nesnesinin detay linki özniteliğine sayfadaki ilgili değer atandı.

                advert.detailLink = "https://www.sahibinden.com" + advert.detailLink; // Oluşturulan ilan nesnesinin detay linki değeri doğru şekilde güncellendi. İlgili detay link değerinin kopyalandığı gibi tarayıcıda detay sayfasına gitmesi amaçlandı.

                advert.price = GetPrice.Price(advert.detailLink); // Static bir sınıfın static bir metodundan fiyat bilgisi talep edilmesini amaçladım. Bu kısım şu an maalesef istediğim gibi çalışmıyor. Son teslim zamanını ezmemek adına bunu ileri bir tarihte sağlıklı bir şekilde çalışır hale getirmeyi istiyorum.

                adverts.Add(advert); // Oluşturulan ilan nesnesi ilan dizisine eklendi.

            } // For döngüsü bitti. İlan listesi dönmeye (return edilmeye) hazır hale geldi.

            return Ok(adverts); // İlan dizisi başarılı bir şekilde dönerse api tarafında 200 (başarılı) http koduyla birlikte ilan sınıfı değerlerini gözlemleyebileceğiz.

        }


    }
}