using CourierSafetyAppDemo.Views;

namespace CourierSafetyAppDemo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("login", typeof(MainPage));
            Routing.RegisterRoute("dashboard", typeof(DashBoardView));
            Routing.RegisterRoute("notifications", typeof(NotificationsView));
            Routing.RegisterRoute("routes", typeof(RoutesViewl));
            Routing.RegisterRoute("profile", typeof(ProfileView));
        }
    }
}
