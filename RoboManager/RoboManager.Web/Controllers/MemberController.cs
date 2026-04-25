using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient _httpClient;

        // Configuramos el cliente para hablar con tu API
        public MemberController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        // GET: /Member
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Member");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                // Usamos dynamic para hacerlo súper rápido sin tener que copiar los DTOs aquí
                var members = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(members);
            }

            return View(new List<dynamic>());
        }

        // 1. Muestra la pantalla del formulario vacío
        public IActionResult Create()
        {
            return View();
        }

        // 2. Recibe los datos del formulario y los manda a tu API
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string apellido, string correo, int rol)
        {
            // Empaquetamos los datos igual que en Swagger
            var nuevoMiembro = new
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                rol = rol,
                teamId = 1 // Lo asignamos al equipo 1 por defecto para evitar el error de llave foránea
            };

            var json = JsonSerializer.Serialize(nuevoMiembro);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Hacemos el POST a tu API
            var response = await _httpClient.PostAsync("api/Member", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Si se guardó, volvemos a la tabla
            }

            return View(); // Si algo falló, nos quedamos en el formulario
        }

        // 3. Borra un miembro y recarga la tabla
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Member/{id}");
            return RedirectToAction("Index");
        }

        // 4. Muestra el formulario de edición con los datos llenos
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Member/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var member = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(member); // Le pasamos los datos a la vista
            }
            return RedirectToAction("Index"); // Si no lo encuentra, vuelve a la tabla
        }

        // 5. Recibe los datos modificados y los envía a tu API (PUT)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string apellido, string correo, int rol)
        {
            var miembroEditado = new
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                rol = rol,
                teamId = 1 // Lo dejamos en el equipo 1 por defecto
            };

            var json = JsonSerializer.Serialize(miembroEditado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Hacemos el PUT a tu API
            var response = await _httpClient.PutAsync($"api/Member/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Volvemos a la tabla si todo sale bien
            }

            return RedirectToAction("Index");
        }
    }
}