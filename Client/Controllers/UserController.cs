using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7131/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Main").Result;

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            string data = response.Content.ReadAsStringAsync().Result;
            List<TriviaQuestion> model = (List<TriviaQuestion>)Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(List<TriviaQuestion>));

            return View(model);
        }
        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form(TriviaQuestionDTO question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            var data = JsonConvert.SerializeObject(question);
            var payload = new StringContent(data, Encoding.UTF8, "application/json");
            client.PostAsync(client.BaseAddress + "/Main", payload);

            return RedirectToAction(nameof(Index));
        }
    }
}
