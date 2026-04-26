using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProjectController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        
        private async Task<IActionResult> VolverALaTablaConError(string mensajeError)
        {
            ViewBag.Error = mensajeError;
            var response = await _httpClient.GetAsync("api/Project");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var projects = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View("Index", projects);
            }
            return View("Index", new List<dynamic>());
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Project");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var projects = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(projects);
            }
            return View(new List<dynamic>());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            var nuevo = new { Nombre = nombre, Descripcion = descripcion, FechaInicio = fechaInicio, FechaFin = fechaFin };
            var json = JsonSerializer.Serialize(nuevo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Project", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            TempData["Error"] = $"No se pudo guardar. Detalle: {await response.Content.ReadAsStringAsync()}";
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Project/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var project = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(project);
            }

            
            return await VolverALaTablaConError($"La API rechazó buscar el proyecto. Respondió código: {response.StatusCode}. ¿Te falta el método GET por ID en tu API?");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            var editado = new { Nombre = nombre, Descripcion = descripcion, FechaInicio = fechaInicio, FechaFin = fechaFin };
            var json = JsonSerializer.Serialize(editado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Project/{id}", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            return await VolverALaTablaConError($"Error al actualizar. Status API: {response.StatusCode}. Detalle: {await response.Content.ReadAsStringAsync()}");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Project/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return await VolverALaTablaConError($"La API rechazó eliminar el ID {id}. Status: {response.StatusCode}");
            }

            
            TempData["Error"] = $"La API dijo que SÍ eliminó el ID {id} con éxito. Si el proyecto sigue en la tabla, el problema está en tu IProjectService (probablemente te falta el SaveChangesAsync).";

            return RedirectToAction("Index");
        }
    }
}