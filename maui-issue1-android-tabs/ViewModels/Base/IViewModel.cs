using System;
namespace maui_issue1_android_tabs.ViewModels.Base;

public interface IViewModel
{
    public bool IsInitialized { get; set; }
    public bool IsBusy { get; }

    Task InitializeAsync();
}

