
using System.Collections.ObjectModel;
using TareasMaui.Models;

namespace TareasMaui.Datos
{
    public static class DatosTareas
    {
        public static ObservableCollection<Tarea> ListaTareas { get; } =
            new ObservableCollection<Tarea>();
    }
}
