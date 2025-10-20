using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace CourierSafetyAppDemo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _isLoggingIn;

        public MainViewModel()
        {
            Title = "Courier Safety App";
            LoginCommand = new Command(async () => await Login(), CanLogin);
        }

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set => SetProperty(ref _isLoggingIn, value);
        }

        public ICommand LoginCommand { get; }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && 
                   !string.IsNullOrWhiteSpace(Password) && 
                   !IsLoggingIn;
        }

        private async Task Login()
        {
            if (IsLoggingIn)
                return;

            IsLoggingIn = true;

            try
            {
                // Simulate login API call with animation
                await Task.Delay(1500);

                // For demo purposes, accept any credentials
                // In production, validate against your backend
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    // Clear password for security
                    Password = string.Empty;
                    
                    // Navigate to dashboard using absolute route
                    await Shell.Current.GoToAsync("//dashboard");
                }
                else
                {
                    await Shell.Current.DisplayAlert(
                        "Login Failed", 
                        "Invalid credentials. Please try again.", 
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error", 
                    $"Login failed: {ex.Message}", 
                    "OK");
            }
            finally
            {
                IsLoggingIn = false;
            }
        }
    }
}
