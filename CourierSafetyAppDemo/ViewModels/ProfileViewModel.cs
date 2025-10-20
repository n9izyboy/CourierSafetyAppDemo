using System.Windows.Input;

namespace CourierSafetyAppDemo.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _vehicleType = string.Empty;
        private string _licensePlate = string.Empty;
        private string _profileImageUrl = string.Empty;
        private bool _isOnDuty;
        private DateTime _memberSince;
        private string _courierId = string.Empty;

        public ProfileViewModel()
        {
            Title = "Profile";
            LoadProfileData();
            
            EditProfileCommand = new Command(async () => await EditProfile());
            ToggleDutyStatusCommand = new Command(ToggleDutyStatus);
            ChangePasswordCommand = new Command(async () => await ChangePassword());
            LogoutCommand = new Command(async () => await Logout());
            UpdateProfilePictureCommand = new Command(async () => await UpdateProfilePicture());
            NavigateToDashboardCommand = new Command(async () => await NavigateToDashboard());
            NavigateToRoutesCommand = new Command(async () => await NavigateToRoutes());
            NavigateToAlertsCommand = new Command(async () => await NavigateToAlerts());
            NavigateToProfileCommand = new Command(async () => await NavigateToProfile());
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public string CourierId
        {
            get => _courierId;
            set => SetProperty(ref _courierId, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string VehicleType
        {
            get => _vehicleType;
            set => SetProperty(ref _vehicleType, value);
        }

        public string LicensePlate
        {
            get => _licensePlate;
            set => SetProperty(ref _licensePlate, value);
        }

        public string ProfileImageUrl
        {
            get => _profileImageUrl;
            set => SetProperty(ref _profileImageUrl, value);
        }

        public bool IsOnDuty
        {
            get => _isOnDuty;
            set
            {
                SetProperty(ref _isOnDuty, value);
                OnPropertyChanged(nameof(DutyStatus));
            }
        }

        public string DutyStatus => IsOnDuty ? "On Duty" : "Off Duty";

        public DateTime MemberSince
        {
            get => _memberSince;
            set => SetProperty(ref _memberSince, value);
        }

        public ICommand EditProfileCommand { get; }
        public ICommand ToggleDutyStatusCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand UpdateProfilePictureCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToRoutesCommand { get; }
        public ICommand NavigateToAlertsCommand { get; }
        public ICommand NavigateToProfileCommand { get; }

        private void LoadProfileData()
        {
            // Sample data - replace with actual data loading
            FirstName = "John";
            LastName = "Doe";
            CourierId = "#CD-2024-1547";
            Email = "john.doe@saferoute.com";
            PhoneNumber = "+1 (555) 123-4567";
            VehicleType = "Delivery Van";
            LicensePlate = "ABC-1234";
            IsOnDuty = true;
            MemberSince = new DateTime(2024, 1, 1);
            ProfileImageUrl = "dotnet_bot.png"; // Default MAUI image
        }

        private async Task EditProfile()
        {
            // Navigate to edit profile page or show edit dialog
            await Shell.Current.DisplayAlert(
                "Edit Profile", 
                "Profile editing functionality will be available soon.", 
                "OK");
        }

        private void ToggleDutyStatus()
        {
            IsOnDuty = !IsOnDuty;
        }

        private async Task ChangePassword()
        {
            // Navigate to change password page or show dialog
            await Shell.Current.DisplayAlert(
                "Change Password", 
                "Password change functionality will be available soon.", 
                "OK");
        }

        private async Task Logout()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Logout", 
                "Are you sure you want to logout?", 
                "Yes", 
                "No");

            if (confirm)
            {
                // Perform logout logic - clear any cached data
                IsOnDuty = false;
                
                // Navigate back to login page
                await Shell.Current.GoToAsync("//dashboard");
                
                // Show a toast or message
                await Shell.Current.DisplayAlert(
                    "Logged Out", 
                    "You have been successfully logged out.", 
                    "OK");
            }
        }

        private async Task UpdateProfilePicture()
        {
            try
            {
                var result = await Shell.Current.DisplayAlert(
                    "Update Profile Picture",
                    "Profile picture update will be available soon.",
                    "OK",
                    "Cancel");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error", 
                    $"Unable to update photo: {ex.Message}", 
                    "OK");
            }
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
            try
            {
                await Shell.Current.GoToAsync("//routes");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error", 
                    $"Navigation failed: {ex.Message}", 
                    "OK");
            }
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
            // Already on profile page, do nothing or refresh
            await Task.CompletedTask;
        }
    }
}
