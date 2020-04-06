using EXLEmployeeSearchUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace EXLEmployeeSearchUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string searchData, int startYear, int endYear)
        {
            IEnumerable<EmployeeViewModel> employees = null;
            if (string.IsNullOrWhiteSpace(searchData))
                searchData = "_";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49583/api/");
               
                var response = await client.GetAsync($"employees/search/{HttpUtility.UrlEncode(searchData)}/{startYear}/{endYear}");

                var result = response;
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(content);
                    }
                    catch
                    {

                    }
                    
                }
                else //web api sent error response 
                {
                    _logger.LogError($"EmployeeAPI sent code {result.StatusCode}");

                    employees = new List<EmployeeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
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
