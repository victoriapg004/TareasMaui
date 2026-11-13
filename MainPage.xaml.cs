using TareasMaui.Datos;
using TareasMaui.Services;

namespace TareasMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly TareasApiService _api = new();
        private bool _cargaInicialHecha;

        public MainPage()
        {
            InitializeComponent();
            listaTareas.ItemsSource = DatosTareas.ListaTareas;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (!_cargaInicialHecha)
            {
                _cargaInicialHecha = true;
                await CargarTareasAsync();
            }
        }

        private async void OnNuevaTareaClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TareaFormPage));
        }

        private async void OnActualizarClicked(object? sender, EventArgs e)
        {
            await CargarTareasAsync();
        }

        private async void OnRefreshing(object? sender, EventArgs e)
        {
            await CargarTareasAsync();
            refreshView.IsRefreshing = false;
        }

        private async Task CargarTareasAsync()
        {
            MostrarCargando(true);

            var resultado = await _api.ObtenerTareasAsync();

            // Quita las tareas que venían de la API (conserva las locales)
            for (int i = DatosTareas.ListaTareas.Count - 1; i >= 0; i--)
            {
                if (!DatosTareas.ListaTareas[i].EsLocal)
                    DatosTareas.ListaTareas.RemoveAt(i);
            }

            if (resultado.Exito)
            {
                foreach (var tarea in resultado.Tareas)
                    DatosTareas.ListaTareas.Add(tarea);

                MostrarEstado(resultado.Mensaje, exito: true);
            }
            else
            {
                var mensaje = resultado.Mensaje;

                if (DatosTareas.ListaTareas.Count == 0)
                {
                    var ejemplo = await _api.CargarDatosEjemploAsync();
                    foreach (var tarea in ejemplo)
                    {
                        tarea.EsLocal = true;
                        DatosTareas.ListaTareas.Add(tarea);
                    }
                    mensaje += " Mostrando datos de ejemplo.";
                }

                MostrarEstado(mensaje, exito: false);
            }

            MostrarCargando(false);
        }

        private void MostrarCargando(bool cargando)
        {
            indicadorCarga.IsVisible = cargando;
            indicadorCarga.IsRunning = cargando;
            btnActualizar.IsEnabled = !cargando;

            if (cargando)
            {
                bannerEstado.BackgroundColor = Color.FromArgb("#EDE7F6");
                lblEstado.TextColor = Color.FromArgb("#30244F");
                lblEstado.Text = "Cargando tareas...";
                bannerEstado.IsVisible = true;
            }
        }

        private void MostrarEstado(string mensaje, bool exito)
        {
            bannerEstado.BackgroundColor = Color.FromArgb(exito ? "#E8F5E9" : "#FFF3E0");
            lblEstado.TextColor = Color.FromArgb(exito ? "#1B5E20" : "#E65100");
            lblEstado.Text = mensaje;
            bannerEstado.IsVisible = true;
        }
    }
}