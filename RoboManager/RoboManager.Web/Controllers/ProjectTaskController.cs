using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RoboManager.Web.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProjectTaskController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7169/");
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/ProjectTask");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tasks = JsonSerializer.Deserialize<List<dynamic>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(tasks);
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

           
            var resMembers = await _httpClient.GetAsync("api/Member");
            if (resMembers.IsSuccessStatusCode)
            {
                var jsonMem = await resMembers.Content.ReadAsStringAsync();
                ViewBag.Miembros = JsonSerializer.Deserialize<List<dynamic>>(jsonMem, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(string title, string description, DateTime deadline, int status, int projectId, int? assignedMemberId)
        {
            var nuevaTarea = new
            {
                Title = title,
                Description = description,
                Deadline = deadline,
                Status = status, 
                ProjectId = projectId,
                AssignedMemberId = assignedMemberId
            };

            var json = JsonSerializer.Serialize(nuevaTarea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ProjectTask", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            
            var errorDetalle = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"Error {response.StatusCode}: {errorDetalle}";

            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var resTask = await _httpClient.GetAsync($"api/ProjectTask/{id}");
            if (!resTask.IsSuccessStatusCode) return RedirectToAction("Index");
            var task = JsonSerializer.Deserialize<dynamic>(await resTask.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            
            var resProjects = await _httpClient.GetAsync("api/Project");
            if (resProjects.IsSuccessStatusCode)
                ViewBag.Proyectos = JsonSerializer.Deserialize<List<dynamic>>(await resProjects.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var resMembers = await _httpClient.GetAsync("api/Member");
            if (resMembers.IsSuccessStatusCode)
                ViewBag.Miembros = JsonSerializer.Deserialize<List<dynamic>>(await resMembers.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(task);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title, string description, DateTime deadline, int status, int projectId, int? assignedMemberId)
        {
            var tareaEditada = new
            {
                Title = title,
                Description = description,
                Deadline = deadline,
                Status = status,
                ProjectId = projectId,
                AssignedMemberId = assignedMemberId
            };

            var json = JsonSerializer.Serialize(tareaEditada);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/ProjectTask/{id}", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Index");

            TempData["Error"] = "No se pudo actualizar la tarea. Revisa la API.";
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ProjectTask/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al intentar eliminar la tarea.";
            }
            return RedirectToAction("Index");
        }
    }
}