namespace TareasMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TareaFormPage), typeof(TareaFormPage));
        }
    }
}
