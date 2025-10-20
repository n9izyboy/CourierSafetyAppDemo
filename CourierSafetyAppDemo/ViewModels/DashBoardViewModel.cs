using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourierSafetyAppDemo.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        private string _courierName = string.Empty;
        private int _activeDeliveries;
        private int _completedToday;
        private double _totalDistance;

        public DashBoardViewModel()
        {
            Title = "Dashboard";
            CourierName = "Courier OP-2847";
            ActiveDeliveries = 3;
            CompletedToday = 12;
            TotalDistance = 47.8;

            NavigateToRoutesCommand = new Command(async () => await NavigateToRoutes());
            NavigateToNotificationsCommand = new Command(async () => await NavigateToNotifications());
            NavigateToProfileCommand = new Command(async () => await NavigateToProfile());
        }

        public string CourierName
        {
            get => _courierName;
            set => SetProperty(ref _courierName, value);
        }

        public int ActiveDeliveries
        {
            get => _activeDeliveries;
            set => SetProperty(ref _activeDeliveries, value);
        }

        public int CompletedToday
        {
            get => _completedToday;
            set => SetProperty(ref _completedToday, value);
        }

        public double TotalDistance
        {
            get => _totalDistance;
            set => SetProperty(ref _totalDistance, value);
        }

        public ICommand NavigateToRoutesCommand { get; }
        public ICommand NavigateToNotificationsCommand { get; }
        public ICommand NavigateToProfileCommand { get; }

        private async Task NavigateToRoutes()
        {
            try
            {
                await Shell.Current.GoToAsync("//routes");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Navigation failed: {ex.Message}", "OK");
            }
        }

        private async Task NavigateToNotifications()
        {
            try
            {
                await Shell.Current.GoToAsync("//notifications");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Navigation failed: {ex.Message}", "OK");
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
                await Shell.Current.DisplayAlert("Error", $"Navigation failed: {ex.Message}", "OK");
            }
        }
    }
}
