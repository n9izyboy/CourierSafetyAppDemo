using CourierSafetyAppDemo.ViewModels;

namespace CourierSafetyAppDemo.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView()
	{
		InitializeComponent();
		BindingContext = new ProfileViewModel();
	}
}