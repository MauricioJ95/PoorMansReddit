using Newtonsoft.Json.Linq;
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
            List<string> names = new List<string>();
            for (int i = 0; i < people.Count; i++)
            {
                JToken person = people[i];
                string name = person["name"].ToString();
                names.Add(name);
            }
            ViewBag.Names = names;

            return View();
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

            List<JToken> people = personData["data"].ToList();

            return people;
        }
    }
}