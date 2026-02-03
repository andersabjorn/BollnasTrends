using BollnasKris.ViewModels;

namespace BollnasKris.Views;

public partial class CrisisPage : ContentPage
{
    public CrisisPage(CrisisViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
