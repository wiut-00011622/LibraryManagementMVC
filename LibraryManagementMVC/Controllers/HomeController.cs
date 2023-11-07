using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using LibraryManagementMVC.Models;

namespace LibraryManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public HomeController()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "http://localhost:5000/api"; // Use your API base URL here
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/books");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<Book>>(content);
                return View(books);
            }
            return View(new List<Book>());
        }
    }
}