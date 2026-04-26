using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class ComponentController : Controller
    {
        private readonly HttpClient _httpClient;

        public ComponentController()
        {
            _httpClient = new HttpClient();
            
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Component");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var components = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(components);
            }
            return View(new List<dynamic>());
        }

        
        
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, int tipo, int cantidad, int estado)
        {
            
            var nuevoComponente = new
            {
                Nombre = nombre,
                Tipo = tipo,
                Cantidad = cantidad,
                Estado = estado.ToString() 
            };

            var json = JsonSerializer.Serialize(nuevoComponente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Component", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); 
            }

            
            var errorDeLaApi = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"La API rechazó el guardado. Detalles: {errorDeLaApi}";
            return RedirectToAction("Create");
        }

       
        
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Component/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var component = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(component); 
            }
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, int tipo, int cantidad, int estado)
        {
            var compEditado = new
            {
                Nombre = nombre,
                Tipo = tipo,
                Cantidad = cantidad,
                Estado = estado.ToString()
            };

            var json = JsonSerializer.Serialize(compEditado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Component/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Component/{id}");
            return RedirectToAction("Index");
        }
    }
}