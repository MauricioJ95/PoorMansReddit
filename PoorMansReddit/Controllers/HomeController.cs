using Newtonsoft.Json.Linq;
using PoorMansReddit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace PoorMansReddit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            List<JToken> people = GetPage();
            AboutModel model = new AboutModel();
            model.Items = new List<RedditModel>();
            foreach (var item in people)
            {
                model.Items.Add(MapRedditModel(item));
            }

            return View(model);
        }

        private RedditModel MapRedditModel(JToken item)
        {
            var token = item["data"];

            return new RedditModel()
            {
                Title = token.SelectToken("title").ToString(),
                Image = token.SelectToken("thumbnail").ToString(),
                Link = token.SelectToken("url").ToString()
            };
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<JToken> GetPage()
        {
            string URL = "https://www.reddit.com/r/aww/.json";

            HttpWebRequest request = WebRequest.CreateHttp(URL);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());

            string APIText = sr.ReadToEnd();

            JToken personData = JToken.Parse(APIText);

            var data = personData.SelectToken("data.children");

            return data.ToList();
        }
    }
}