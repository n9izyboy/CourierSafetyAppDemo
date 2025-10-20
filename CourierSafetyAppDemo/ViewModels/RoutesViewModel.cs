using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourierSafetyAppDemo.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {
        private ObservableCollection<DeliveryRoute> _routes = new();
        private DeliveryRoute? _selectedRoute;
        private bool _isLoadingRoutes;

        public RoutesViewModel()
        {
            Title = "Routes";
            LoadRoutes();
            StartRouteCommand = new Command<DeliveryRoute>(StartRoute, CanStartRoute);
            CompleteRouteCommand = new Command<DeliveryRoute>(CompleteRoute, CanCompleteRoute);
            ViewRouteDetailsCommand = new Command<DeliveryRoute>(ViewRouteDetails);
            RefreshCommand = new Command(async () => await RefreshRoutes());
            NavigateToDashboardCommand = new Command(async () => await NavigateToDashboard());
            NavigateToRoutesCommand = new Command(async () => await NavigateToRoutes());
            NavigateToAlertsCommand = new Command(async () => await NavigateToAlerts());
            NavigateToProfileCommand = new Command(async () => await NavigateToProfile());
        }

        public ObservableCollection<DeliveryRoute> Routes
        {
            get => _routes;
            set => SetProperty(ref _routes, value);
        }

        public DeliveryRoute? SelectedRoute
        {
            get => _selectedRoute;
            set => SetProperty(ref _selectedRoute, value);
        }

        public bool IsLoadingRoutes
        {
            get => _isLoadingRoutes;
            set => SetProperty(ref _isLoadingRoutes, value);
        }

        public ICommand StartRouteCommand { get; }
        public ICommand CompleteRouteCommand { get; }
        public ICommand ViewRouteDetailsCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToRoutesCommand { get; }
        public ICommand NavigateToAlertsCommand { get; }
        public ICommand NavigateToProfileCommand { get; }

        private void LoadRoutes()
        {
            // Sample routes - replace with actual data loading
            Routes = new ObservableCollection<DeliveryRoute>
            {
                new DeliveryRoute
                {
                    Id = 1,
                    Name = "Downtown Route #15",
                    Status = RouteStatus.InProgress,
                    TotalStops = 15,
                    CompletedStops = 8,
                    EstimatedDistance = 25.5,
                    ActualDistance = 12.3,
                    EstimatedDuration = TimeSpan.FromHours(3),
                    StartTime = DateTime.Today.AddHours(9),
                    ActualStartTime = DateTime.Today.AddHours(9).AddMinutes(5),
                    SafetyRating = "MODERATE",
                    Priority = RoutePriority.High
                },
                new DeliveryRoute
                {
                    Id = 2,
                    Name = "Suburban Route #42",
                    Status = RouteStatus.Pending,
                    TotalStops = 20,
                    CompletedStops = 0,
                    EstimatedDistance = 45.2,
                    EstimatedDuration = TimeSpan.FromHours(4.5),
                    StartTime = DateTime.Today.AddHours(13),
                    SafetyRating = "SAFE",
                    Priority = RoutePriority.Medium
                },
                new DeliveryRoute
                {
                    Id = 3,
                    Name = "Industrial Zone #8",
                    Status = RouteStatus.Pending,
                    TotalStops = 12,
                    CompletedStops = 0,
                    EstimatedDistance = 18.7,
                    EstimatedDuration = TimeSpan.FromHours(2.5),
                    StartTime = DateTime.Today.AddHours(15),
                    SafetyRating = "HIGH RISK",
                    Priority = RoutePriority.Low
                },
                new DeliveryRoute
                {
                    Id = 4,
                    Name = "Residential Area #23",
                    Status = RouteStatus.Completed,
                    TotalStops = 18,
                    CompletedStops = 18,
                    EstimatedDistance = 32.1,
                    ActualDistance = 30.8,
                    EstimatedDuration = TimeSpan.FromHours(3.5),
                    StartTime = DateTime.Today.AddHours(7),
                    ActualStartTime = DateTime.Today.AddHours(7).AddMinutes(10),
                    ActualEndTime = DateTime.Today.AddHours(10).AddMinutes(25),
                    SafetyRating = "SAFE",
                    Priority = RoutePriority.Medium
                }
            };
        }

        private bool CanStartRoute(DeliveryRoute? route)
        {
            return route != null && route.Status == RouteStatus.Pending;
        }

        private async void StartRoute(DeliveryRoute? route)
        {
            if (route != null)
            {
                bool confirm = await Shell.Current.DisplayAlert(
                    "Start Route",
                    $"Start {route.Name}?",
                    "Yes",
                    "No");

                if (confirm)
                {
                    route.Status = RouteStatus.InProgress;
                    route.ActualStartTime = DateTime.Now;
                    await Shell.Current.DisplayAlert(
                        "Route Started",
                        $"{route.Name} is now in progress.",
                        "OK");
                }
            }
        }

        private bool CanCompleteRoute(DeliveryRoute? route)
        {
            return route != null && route.Status == RouteStatus.InProgress;
        }

        private async void CompleteRoute(DeliveryRoute? route)
        {
            if (route != null)
            {
                bool confirm = await Shell.Current.DisplayAlert(
                    "Complete Route",
                    $"Mark {route.Name} as completed?",
                    "Yes",
                    "No");

                if (confirm)
                {
                    route.Status = RouteStatus.Completed;
                    route.ActualEndTime = DateTime.Now;
                    route.CompletedStops = route.TotalStops;
                    await Shell.Current.DisplayAlert(
                        "Route Completed",
                        $"{route.Name} has been completed successfully!",
                        "OK");
                }
            }
        }

        private async void ViewRouteDetails(DeliveryRoute? route)
        {
            if (route != null)
            {
                SelectedRoute = route;
                string details = $"Route: {route.Name}\n" +
                                $"Status: {route.Status}\n" +
                                $"Stops: {route.CompletedStops}/{route.TotalStops}\n" +
                                $"Distance: {route.EstimatedDistance:F1} km\n" +
                                $"Duration: {route.EstimatedDuration.TotalHours:F1} hours\n" +
                                $"Safety: {route.SafetyRating}\n" +
                                $"Priority: {route.Priority}";

                await Shell.Current.DisplayAlert(
                    "Route Details",
                    details,
                    "OK");
            }
        }

        private async Task RefreshRoutes()
        {
            IsLoadingRoutes = true;
            await Task.Delay(1000); // Simulate API call
            LoadRoutes();
            IsLoadingRoutes = false;
        }

        private async Task NavigateToDashboard()
        {
            try
            {
                await Shell.Current.GoToAsync("//dashboard");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error",
                    $"Navigation failed: {ex.Message}",
                    "OK");
            }
        }

        private async Task NavigateToRoutes()
        {
            // Already on routes page
            await Task.CompletedTask;
        }

        private async Task NavigateToAlerts()
        {
            try
            {
                await Shell.Current.GoToAsync("//notifications");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error",
                    $"Navigation failed: {ex.Message}",
                    "OK");
            }
        }

        private async Task NavigateToProfile()
        {
            try
            {
                await Shell.Current.GoToAsync("//profile");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error",
                    $"Navigation failed: {ex.Message}",
                    "OK");
            }
        }
    }

    public class DeliveryRoute : BaseViewModel
    {
        private RouteStatus _status;
        private int _completedStops;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalStops { get; set; }
        public double EstimatedDistance { get; set; }
        public double ActualDistance { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public string SafetyRating { get; set; } = "SAFE";
        public RoutePriority Priority { get; set; }

        public RouteStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public int CompletedStops
        {
            get => _completedStops;
            set
            {
                SetProperty(ref _completedStops, value);
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(ProgressPercentage));
            }
        }

        public double Progress => TotalStops > 0 ? (double)CompletedStops / TotalStops : 0;
        public int ProgressPercentage => (int)(Progress * 100);

        public string StatusColor => Status switch
        {
            RouteStatus.Pending => "#00FFFF",
            RouteStatus.InProgress => "#FF0080",
            RouteStatus.Completed => "#00FF41",
            RouteStatus.Cancelled => "#6B7280",
            _ => "#FFFFFF"
        };

        public string SafetyColor => SafetyRating switch
        {
            "SAFE" => "#00FF41",
            "MODERATE" => "#00FFFF",
            "HIGH RISK" => "#FF0080",
            _ => "#FFFFFF"
        };
    }

    public enum RouteStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum RoutePriority
    {
        Low,
        Medium,
        High
    }
}
