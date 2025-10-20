using CourierSafetyAppDemo.ViewModels;
using Microsoft.Maui.Controls;

namespace CourierSafetyAppDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
