using CourierSafetyAppDemo.ViewModels;

namespace CourierSafetyAppDemo;

public partial class NotificationsView : ContentPage
{
	public NotificationsView()
	{
		InitializeComponent();
		BindingContext = new NotificationsViewModel();
	}
}