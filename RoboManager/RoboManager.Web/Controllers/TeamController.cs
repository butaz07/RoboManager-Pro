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

        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
           
            var responseTeam = await _httpClient.GetAsync($"api/Team/{id}");
            if (!responseTeam.IsSuccessStatusCode) return RedirectToAction("Index");

            var teamJson = await responseTeam.Content.ReadAsStringAsync();
            var team = JsonSerializer.Deserialize<dynamic>(teamJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

           
            var responseMembers = await _httpClient.GetAsync("api/Member");
            var miembrosDelEquipo = new List<dynamic>();

            if (responseMembers.IsSuccessStatusCode)
            {
                var membersJson = await responseMembers.Content.ReadAsStringAsync();
                var allMembers = JsonSerializer.Deserialize<List<dynamic>>(membersJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var member in allMembers)
                {
                    if (member is System.Text.Json.JsonElement mJson)
                    {
                        
                        if ((mJson.TryGetProperty("teamId", out var tId) || mJson.TryGetProperty("TeamId", out tId)) && tId.ValueKind != System.Text.Json.JsonValueKind.Null)
                        {
                            if (tId.GetInt32() == id)
                            {
                                miembrosDelEquipo.Add(member);
                            }
                        }
                    }
                }
            }

            
            ViewBag.Miembros = miembrosDelEquipo;
            return View(team);
        }
    }
}