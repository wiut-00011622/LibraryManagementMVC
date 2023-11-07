using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementMVC.Models;
using Newtonsoft.Json;

namespace LibraryManagementMVC.Controllers;
    public class AuthorsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string BaseUrl = "http://your-api-endpoint/api/authors";

        public AuthorsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(BaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var authors = JsonConvert.DeserializeObject<List<Author>>(await response.Content.ReadAsStringAsync());
                return View(authors);
            }

            return View(new List<Author>());
        }

        // GET: Authors/Details/5
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
                var author = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }

            return NotFound();
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(
                    BaseUrl,
                    new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(author);
        }

        // GET: Authors/Edit/5
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
                var author = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }

            return NotFound();
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsync(
                    $"{BaseUrl}/{id}",
                    new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle failure
                }
            }
            return View(author);
        }

        // GET: Authors/Delete/5
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
                var author = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }

            return NotFound();
        }

        // POST: Authors/Delete/5
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
