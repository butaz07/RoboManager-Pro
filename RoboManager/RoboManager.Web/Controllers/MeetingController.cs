using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class MeetingController : Controller
    {
        private readonly HttpClient _httpClient;

        public MeetingController()
        {
            _httpClient = new HttpClient();
            
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Meeting");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var reuniones = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(reuniones);
            }
            return View(new List<dynamic>());
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var resProjects = await _httpClient.GetAsync("api/Project");
            if (resProjects.IsSuccessStatusCode)
            {
                var jsonProj = await resProjects.Content.ReadAsStringAsync();
                ViewBag.Proyectos = JsonSerializer.Deserialize<List<dynamic>>(jsonProj, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(string titulo, string proposito, DateTime fechaHora, string ubicacion, int projectId)
        {
            var nuevaReunion = new
            {
                Titulo = titulo,
                Proposito = proposito,
                FechaHora = fechaHora,
                Ubicacion = ubicacion,
                Estado = "Programada",
                ProjectId = projectId
            };

            var json = JsonSerializer.Serialize(nuevaReunion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Meeting", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            TempData["Error"] = "No se pudo agendar la reunión. Verifica la conexión con la API.";
            return RedirectToAction("Create");
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Meeting/{id}");
            return RedirectToAction("Index");
        }
    }
}