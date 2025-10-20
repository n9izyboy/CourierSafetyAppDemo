using CourierSafetyAppDemo.ViewModels;

namespace CourierSafetyAppDemo.Views;

public partial class RoutesViewl : ContentPage
{
	public RoutesViewl()
	{
        InitializeComponent();
		BindingContext = new RoutesViewModel();
	}
}