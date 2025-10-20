using CourierSafetyAppDemo.ViewModels;

namespace CourierSafetyAppDemo.Views;

public partial class DashBoardView : ContentPage
{
	public DashBoardView()
	{
		InitializeComponent();
		BindingContext = new DashBoardViewModel();
	}
}