using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     
        private List<MunicipalityDisplayInformation> objviewmodel;

        private IHostingEnvironment hostingEnv;

        private IConfiguration iconfiguration;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env, IConfiguration _iconfiguration)
        {
            _logger = logger;
            this.hostingEnv = env;
            iconfiguration = _iconfiguration;
            objviewmodel = new List<MunicipalityDisplayInformation>();
        }
        
        public async Task<IActionResult> IndexAsync()
        {

            //Hosted web API REST Service base url  
            string Baseurl = "http://localhost:59120/";

            List<MunicipalityDisplayModel> resultInfo = new List<MunicipalityDisplayModel>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/MunicipalityInfo");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    resultInfo = JsonConvert.DeserializeObject<List<MunicipalityDisplayModel>>(EmpResponse);

                }
                //returning the employee list to view  
                return View(resultInfo);
            }            
        }

        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            List<MunicipalityDisplayModel> resultInfo = new List<MunicipalityDisplayModel>();
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');

                filename = hostingEnv.WebRootPath + @"\" + filename;
                size += file.Length;

                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                // read JSON directly from a file
                using (StreamReader abc = System.IO.File.OpenText(filename))
                using (JsonTextReader reader = new JsonTextReader(abc))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);

                    foreach(var items in o2)
                    {
                        var jsonObj = JsonConvert.DeserializeObject<JObject>(items.Value.ToString()).First.First;

                        MunicipalityInputModel deserializedModel = JsonConvert.DeserializeObject<MunicipalityInputModel>(jsonObj.ToString());

                        resultInfo.Add(new MunicipalityDisplayModel {Id=0,Name=deserializedModel.Name,ReleaseDate=Convert.ToDateTime(deserializedModel.Date),Result=deserializedModel.TaxType});
                    }                    
                }

                //// deserialize JSON directly from a file
                //using (StreamReader xy = System.IO.File.OpenText(filename))
                //{
                //    JsonSerializer serializer = new JsonSerializer();
                //    MunicipalityInputModel inputData = (MunicipalityInputModel)serializer.Deserialize(xy, typeof(MunicipalityInputModel));
                //}                
            }

            ViewBag.Message = $"{files.Count} file(s) / { size}  bytes uploaded successfully!";

            return View("View", resultInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
