using Integrate_WebApi_In_AspDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Integrate_WebApi_In_AspDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private string url = "https://localhost:7147/api/CRUDAPI/";
        private HttpClient client= new HttpClient();


        //--------Show List through API
        public IActionResult Index()
        {
            List<loginTbl> logList = new List<loginTbl>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string Result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<loginTbl>>(Result);
                if (data != null)
                {
                    logList = data;
                }
            }
            return View(logList);
        }




        //Create 
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(loginTbl tbl)
        {
            string data = JsonConvert.SerializeObject(tbl);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }






        //--------Edit Get
        [HttpGet]
        public IActionResult Edit(int id)
        {
            loginTbl Editdata = new loginTbl();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string Result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<loginTbl>(Result);
                if (data != null)
                {
                    Editdata = data;
                }
            }
            return View(Editdata);

        }




        //--------Edit Post
        [HttpPost]
        public IActionResult Edit(loginTbl tbl)
        {
            string data = JsonConvert.SerializeObject(tbl);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + tbl.id, Content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["updt_msg"] = "Record Updated...";
                return RedirectToAction("Index");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            loginTbl Deldata = new loginTbl();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string Result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<loginTbl>(Result);
                if (data != null)
                {
                    Deldata = data;
                }
            }
            return View(Deldata);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
          
            return View();
        }
    }
}
