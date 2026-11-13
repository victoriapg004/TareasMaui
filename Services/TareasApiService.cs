using System.Net.Http.Json;
using System.Text.Json;
using TareasMaui.Models;

namespace TareasMaui.Services
{
    public record ResultadoApi(List<Tarea> Tareas, bool Exito, string Mensaje);

    public class TareasApiService
    {
        private readonly HttpClient _http;

        public TareasApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl),
                Timeout = TimeSpan.FromSeconds(ApiConfig.TimeoutSegundos)
            };
        }

        public async Task<ResultadoApi> ObtenerTareasAsync()
        {
            try
            {
                var tareas = await _http.GetFromJsonAsync<List<Tarea>>(ApiConfig.RutaTareas)
                             ?? new List<Tarea>();

                return new ResultadoApi(tareas, true,
                    $"Conectado a la API ({tareas.Count} tareas)");
            }
            catch (TaskCanceledException)
            {
                return Error("La API tardó demasiado en responder.");
            }
            catch (HttpRequestException ex)
            {
                var detalle = ex.StatusCode is not null
                    ? $"código {(int)ex.StatusCode}"
                    : "no se pudo conectar";
                return Error($"Error al consultar la API ({detalle}).");
            }
            catch (JsonException)
            {
                return Error("La API respondió con un formato inesperado.");
            }
        }

        // Datos de prueba locales, usados cuando la API no está disponible
        public async Task<List<Tarea>> CargarDatosEjemploAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("tareas_ejemplo.json");
                var tareas = await JsonSerializer.DeserializeAsync<List<Tarea>>(
                    stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return tareas ?? new List<Tarea>();
            }
            catch (Exception)
            {
                return new List<Tarea>();
            }
        }

        private static ResultadoApi Error(string mensaje) =>
            new(new List<Tarea>(), false, mensaje);
    }
}