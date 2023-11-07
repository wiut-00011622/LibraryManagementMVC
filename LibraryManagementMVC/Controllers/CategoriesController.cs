using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementMVC.Models;
using Newtonsoft.Json;

namespace LibraryManagementMVC.Controllers;
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string BaseUrl = "http://ec2-34-207-56-91.compute-1.amazonaws.com/api/categories";

        public CategoriesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(BaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync());
                return View(categories);
            }

            return View(new List<Category>());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }

            return NotFound();
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(
                    BaseUrl,
                    new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }

            return NotFound();
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsync(
                    $"{BaseUrl}/{id}",
                    new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle failure
                }
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }

            return NotFound();
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{BaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle failure
            }

            return RedirectToAction(nameof(Index));
        }
    }
