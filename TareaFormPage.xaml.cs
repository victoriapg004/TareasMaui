
using TareasMaui.Models;
using TareasMaui.Datos;

namespace TareasMaui
{
    public partial class TareaFormPage : ContentPage
    {
        public TareaFormPage()
        {
            InitializeComponent();
        }

        private async void OnGuardarClicked(object? sender, EventArgs e)
        {
            string titulo = txtTitulo.Text?.Trim() ?? "";
            string descripcion = txtDescripcion.Text?.Trim() ?? "";
            string prioridad = pickerPrioridad.SelectedItem?.ToString() ?? "";

            lblError.IsVisible = false;

            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(prioridad))
            {
                lblError.Text = "Debe completar todos los campos.";
                lblError.IsVisible = true;
                return;
            }

            if (titulo.Length < 3)
            {
                lblError.Text = "El título debe tener al menos 3 caracteres.";
                lblError.IsVisible = true;
                return;
            }

            if (descripcion.Length < 10)
            {
                lblError.Text = "La descripción debe tener al menos 10 caracteres.";
                lblError.IsVisible = true;
                return;
            }

            Tarea nuevaTarea = new Tarea
            {
                Titulo = titulo,
                Descripcion = descripcion,
                Prioridad = prioridad
            };

            DatosTareas.ListaTareas.Add(nuevaTarea);

            await DisplayAlertAsync(
                "Correcto",
                "La tarea se guardó correctamente.",
                "Aceptar");

            await Shell.Current.GoToAsync("..");
        }
    }
}
