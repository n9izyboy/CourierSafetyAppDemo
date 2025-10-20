using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourierSafetyAppDemo.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private ObservableCollection<Notification> _notifications = new();
        private int _unreadCount;
        private bool _isRefreshing;

        public NotificationsViewModel()
        {
            Title = "Notifications";
            LoadNotifications();
            MarkAsReadCommand = new Command<Notification>(MarkAsRead);
            ClearAllCommand = new Command(ClearAll);
            RefreshCommand = new Command(async () => await RefreshNotifications());
            NavigateToDashboardCommand = new Command(async () => await NavigateToDashboard());
        }

        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set => SetProperty(ref _notifications, value);
        }

        public int UnreadCount
        {
            get => _unreadCount;
            set => SetProperty(ref _unreadCount, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ICommand MarkAsReadCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }

        private void LoadNotifications()
        {
            // Sample notifications - replace with actual data loading
            Notifications = new ObservableCollection<Notification>
            {
                new Notification
                {
                    Id = 1,
                    Title = "High-Risk Area Alert",
                    Message = "Route contains a high-risk area. Exercise caution in Downtown District.",
                    Timestamp = DateTime.Now.AddMinutes(-15),
                    IsRead = false,
                    Type = NotificationType.Alert,
                    Priority = NotificationPriority.High
                },
                new Notification
                {
                    Id = 2,
                    Title = "New Route Assigned",
                    Message = "You have been assigned to Suburban Route #42 starting at 1:00 PM.",
                    Timestamp = DateTime.Now.AddHours(-1),
                    IsRead = false,
                    Type = NotificationType.Info,
                    Priority = NotificationPriority.Medium
                },
                new Notification
                {
                    Id = 3,
                    Title = "Weather Alert",
                    Message = "Heavy rain expected between 2:00 PM - 4:00 PM. Plan accordingly.",
                    Timestamp = DateTime.Now.AddHours(-2),
                    IsRead = false,
                    Type = NotificationType.Warning,
                    Priority = NotificationPriority.Medium
                },
                new Notification
                {
                    Id = 4,
                    Title = "Delivery Completed",
                    Message = "Successfully completed 12 deliveries on Downtown Route.",
                    Timestamp = DateTime.Now.AddHours(-3),
                    IsRead = true,
                    Type = NotificationType.Success,
                    Priority = NotificationPriority.Low
                },
                new Notification
                {
                    Id = 5,
                    Title = "Safety Training",
                    Message = "Mandatory safety training scheduled for next Monday at 9:00 AM.",
                    Timestamp = DateTime.Now.AddDays(-1),
                    IsRead = true,
                    Type = NotificationType.Info,
                    Priority = NotificationPriority.Low
                }
            };

            UpdateUnreadCount();
        }

        private void MarkAsRead(Notification notification)
        {
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                UpdateUnreadCount();
            }
        }

        private async void ClearAll()
        {
            bool hasUnread = Notifications.Any(n => !n.IsRead);
            
            if (hasUnread)
            {
                await Shell.Current.DisplayAlert(
                    "Clear All", 
                    "Mark all notifications as read first.", 
                    "OK");
            }
            else
            {
                Notifications.Clear();
                UnreadCount = 0;
            }
        }

        private void UpdateUnreadCount()
        {
            UnreadCount = Notifications.Count(n => !n.IsRead);
        }

        private async Task RefreshNotifications()
        {
            IsRefreshing = true;
            await Task.Delay(1000); // Simulate API call
            LoadNotifications();
            IsRefreshing = false;
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
    }

    public class Notification : BaseViewModel
    {
        private bool _isRead;

        public int Id { get; set; }
        public new string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; }

        public bool IsRead
        {
            get => _isRead;
            set => SetProperty(ref _isRead, value);
        }

        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - Timestamp;
                if (timeSpan.TotalMinutes < 1) return "Just now";
                if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes}m ago";
                if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours}h ago";
                if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays}d ago";
                return Timestamp.ToString("MMM dd");
            }
        }
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Alert,
        Success
    }

    public enum NotificationPriority
    {
        Low,
        Medium,
        High
    }
}
