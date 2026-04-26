using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/"); 
        }

        public async Task<IActionResult> Index()
        {
            
            int totalProyectos = 0;
            int tareasPendientes = 0;
            int totalComponentes = 0;
            int proximasReuniones = 0;

            try
            {
                
                var resProj = await _httpClient.GetAsync("api/Project");
                if (resProj.IsSuccessStatusCode)
                {
                    var proyectos = JsonSerializer.Deserialize<List<dynamic>>(await resProj.Content.ReadAsStringAsync());
                    if (proyectos != null) totalProyectos = proyectos.Count;
                }

               
                var resTask = await _httpClient.GetAsync("api/ProjectTask"); 
                if (resTask.IsSuccessStatusCode)
                {
                    var tareas = JsonSerializer.Deserialize<List<dynamic>>(await resTask.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (tareas != null)
                    {
                       
                        tareasPendientes = tareas.Count(t =>
                        {
                            if (t is JsonElement json && (json.TryGetProperty("status", out var val) || json.TryGetProperty("Status", out val)))
                            {
                                return val.GetInt32() < 2;
                            }
                            return false;
                        });
                    }
                }

                
                var resComp = await _httpClient.GetAsync("api/Component");
                if (resComp.IsSuccessStatusCode)
                {
                    var componentes = JsonSerializer.Deserialize<List<dynamic>>(await resComp.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (componentes != null)
                    {
                        foreach (var comp in componentes)
                        {
                            if (comp is JsonElement json && (json.TryGetProperty("cantidad", out var val) || json.TryGetProperty("Cantidad", out val)))
                            {
                                totalComponentes += val.GetInt32();
                            }
                        }
                    }
                }

               
                var resMeet = await _httpClient.GetAsync("api/Meeting");
                if (resMeet.IsSuccessStatusCode)
                {
                    var reuniones = JsonSerializer.Deserialize<List<dynamic>>(await resMeet.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (reuniones != null)
                    {
                        proximasReuniones = reuniones.Count(r =>
                        {
                            if (r is JsonElement json && (json.TryGetProperty("estado", out var val) || json.TryGetProperty("Estado", out val)))
                            {
                                return val.GetString() == "Programada";
                            }
                            return false;
                        });
                    }
                }
            }
            catch
            {
               
            }

            
            ViewBag.TotalProyectos = totalProyectos;
            ViewBag.TareasPendientes = tareasPendientes;
            ViewBag.TotalComponentes = totalComponentes;
            ViewBag.ProximasReuniones = proximasReuniones;

            return View();
        }
    }
}