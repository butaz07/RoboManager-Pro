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
            // Asegúrate de que este sea tu puerto de Swagger
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        // 1. Mostrar la tabla del Inventario
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

        // ==========================================
        // 2. MUESTRA LA PANTALLA DE CREAR (¡Faltaba esto!)
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. RECIBE LOS DATOS DEL FORMULARIO Y GUARDA
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, int tipo, int cantidad, int estado)
        {
            // Usamos la primera letra mayúscula para coincidir con tu DTO en la API
            var nuevoComponente = new
            {
                Nombre = nombre,
                Tipo = tipo,
                Cantidad = cantidad,
                Estado = estado.ToString() // Enviamos como texto para evitar el error de JSON
            };

            var json = JsonSerializer.Serialize(nuevoComponente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Component", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Todo perfecto, volvemos a la tabla
            }

            // Si falla, mostramos el error en la alerta roja
            var errorDeLaApi = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"La API rechazó el guardado. Detalles: {errorDeLaApi}";
            return RedirectToAction("Create");
        }

        // ==========================================
        // 4. MUESTRA LA PANTALLA DE EDITAR (¡Faltaba esto!)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Component/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var component = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(component); // Abre Edit.cshtml con los datos pre-llenados
            }
            return RedirectToAction("Index");
        }

        // 5. RECIBE LOS DATOS ACTUALIZADOS Y LOS MANDA A LA API
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

        // 6. ELIMINAR COMPONENTE
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Component/{id}");
            return RedirectToAction("Index");
        }
    }
}