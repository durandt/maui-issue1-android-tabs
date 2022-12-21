using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_issue1_android_tabs.ViewModels.Base;

namespace maui_issue1_android_tabs.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _guid = System.Guid.NewGuid().ToString("D")[..8];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OnAppearingCalledCountLabel))]
    private int _onAppearingCalledCount;

    public string OnAppearingCalledCountLabel => $"OnAppearing called {OnAppearingCalledCount} times";
    public string InitializationStatusLabel => $"Initialization status: { (IsBusy ? "Initializing..." : (IsInitialized ? "Initialized" : "None"))}";
    public string ItemsCountLabel => $"Results contain {Items.Count} item{(Items.Count > 1 ? "s" : "")}";

    public ObservableCollection<string> Items { get; } = new();

    private readonly string[] _data = { "The", "Quick", "Brown", "Fox", "Jumps", "Over", "The", "Lazy", "Dog" };

    public MainViewModel()
    {
        PropertyChanged += OnPropertyChanged;
        Items.CollectionChanged += (_, _) =>
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(ItemsCountLabel)));
        };
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsInitialized))
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(InitializationStatusLabel)));
        }
        else if (e.PropertyName == nameof(IsBusy))
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(InitializationStatusLabel)));
        }
    }

    public override async Task InitializeAsync()
    {
        await IsBusyFor(() => Task.Delay(500 + new Random().Next(500)).ContinueWith(_ =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Items.Clear();
                Enumerable.Range(0, 1 + new Random().Next(4)).ToList().ForEach(i => Items.Add(_data[i]));
            });
        }));
    }

    public void AddItem()
    {
        var currentCount = Items.Count;
        Items.Add(_data[currentCount % _data.Length]);
    }
}