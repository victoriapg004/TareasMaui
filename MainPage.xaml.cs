
using TareasMaui.Datos;

namespace TareasMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            listaTareas.ItemsSource = DatosTareas.ListaTareas;
        }

        private async void OnNuevaTareaClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TareaFormPage));
        }
    }
}
