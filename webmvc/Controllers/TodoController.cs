using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webmvc.Models;
using Todo.Data.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace webmvc.Controllers
{
    public class TodoController : Controller
    {
        private readonly IOptions<AppConfig> config;

        public TodoController(IOptions<AppConfig> config)
        {
            this.config = config;
        }

        public async Task<IActionResult> Index()
        {
            var model = new TodoViewModel();
            model.Todos = new List<Todos>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"{config.Value.TodoUri}/todos");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<Todos>>(jsonString);
                model.Todos = todos;
            }
            else
            {

            }
            return View(model);
        }
    }
}
