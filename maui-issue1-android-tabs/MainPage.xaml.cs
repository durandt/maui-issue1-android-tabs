using System.ComponentModel;
using maui_issue1_android_tabs.ViewModels;

namespace maui_issue1_android_tabs;

public partial class MainPage : ContentPage
{
    private MainViewModel MainViewModel { get; }

	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
        MainViewModel = mainViewModel;
    }

    protected override async void OnAppearing()
    {
        if (BindingContext is MainViewModel mainViewModel)
        {
            mainViewModel.OnAppearingCalledCount++;
        }

        if (BindingContext is ViewModels.Base.IViewModel { IsInitialized: false } viewModel)
        {
            viewModel.IsInitialized = true;
            await viewModel.InitializeAsync();
        }
    }

    private async void RefreshButton_OnClicked(object sender, EventArgs e)
    {
        MainViewModel.IsInitialized = true;
        await MainViewModel.InitializeAsync();
    }

    private void AddResultButton_OnClicked(object sender, EventArgs e)
    {
        MainViewModel.AddItem();
    }

    private void InvalidateMeasureButton_OnClicked(object sender, EventArgs e)
    {
        InvalidateMeasure();
    }

    private void VerticalStackLayout_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == HeightProperty.PropertyName)
        {
            (_ScrollView as IView)?.InvalidateMeasure();
        }
    }
}


