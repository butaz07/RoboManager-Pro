using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly HttpClient _httpClient;

        public TeamController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Team");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(teams);
            }
            return View(new List<dynamic>());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string especialidad)
        {
            // 🔥 AQUÍ ESTÁ LA MAGIA: Empaquetamos "especialidad" dentro de "Descripcion"
            var nuevo = new { Nombre = nombre, Descripcion = especialidad };
            var json = JsonSerializer.Serialize(nuevo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Team", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            TempData["Error"] = "No se pudo crear el equipo. Verifica la conexión con la API.";
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Team/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var team = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(team);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string especialidad)
        {
            // 🔥 Y AQUÍ TAMBIÉN: Empaquetamos "especialidad" dentro de "Descripcion"
            var editado = new { Nombre = nombre, Descripcion = especialidad };
            var json = JsonSerializer.Serialize(editado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Team/{id}", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            TempData["Error"] = $"Error al actualizar el equipo {id}.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Team/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo eliminar el equipo.";
            }
            return RedirectToAction("Index");
        }
    }
}