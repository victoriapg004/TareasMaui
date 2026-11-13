namespace TareasMaui.Services
{
    public static class ApiConfig
    {
        private const int Puerto = 8000;

        public const string RutaTareas = "api/tareas/";

        public const int TimeoutSegundos = 8;

        public static string BaseUrl =>
            DeviceInfo.Platform == DevicePlatform.Android
                ? $"http://10.0.2.2:{Puerto}/"
                : $"http://localhost:{Puerto}/";
    }
}