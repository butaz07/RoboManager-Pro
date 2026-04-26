using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient _httpClient;

        
        public MemberController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Member");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var members = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(members);
            }

            return View(new List<dynamic>());
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var response = await _httpClient.GetAsync("api/Team");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                ViewBag.Equipos = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string apellido, string correo, int rol, int? teamId)
        {
            var nuevoMiembro = new
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                rol = rol,
                teamId = teamId 
            };

            var json = JsonSerializer.Serialize(nuevoMiembro);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Member", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Member/{id}");
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseMiembro = await _httpClient.GetAsync($"api/Member/{id}");
            var responseEquipos = await _httpClient.GetAsync("api/Team");

            if (responseMiembro.IsSuccessStatusCode)
            {
                
                if (responseEquipos.IsSuccessStatusCode)
                {
                    var teamsJson = await responseEquipos.Content.ReadAsStringAsync();
                    ViewBag.Equipos = JsonSerializer.Deserialize<List<dynamic>>(teamsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                var jsonString = await responseMiembro.Content.ReadAsStringAsync();
                var member = JsonSerializer.Deserialize<dynamic>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(member);
            }
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string apellido, string correo, int rol, int? teamId)
        {
            var miembroEditado = new
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                rol = rol,
                teamId = teamId 
            };

            var json = JsonSerializer.Serialize(miembroEditado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Member/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}