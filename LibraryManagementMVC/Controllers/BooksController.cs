using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementMVC.Models;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace LibraryManagementMVC.Controllers;
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string BaseUrl = "http://your-api-endpoint/api/";

        public BooksController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}books");

            if (response.IsSuccessStatusCode)
            {
                var books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
                return View(books);
            }

            return View(new List<Book>()); // Or handle errors as appropriate
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            return NotFound();
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Isbn,AuthorId,CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(
                    $"{BaseUrl}books",
                    new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            return NotFound();
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Isbn,AuthorId,CategoryId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsync(
                    $"{BaseUrl}books/{id}",
                    new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle failure
                }
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{BaseUrl}books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            return NotFound();
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{BaseUrl}books/{id}");

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
