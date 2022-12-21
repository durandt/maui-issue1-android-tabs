using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_issue1_android_tabs.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);

    [ObservableProperty]
    private bool _isBusy;
    [ObservableProperty]
    private bool _isInitialized;
    [ObservableProperty]
    private bool _disposedValue;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task IsBusyFor(Func<Task> unitOfWork)
    {
        await _isBusyLock.WaitAsync();

        try
        {
            IsBusy = true;
            await unitOfWork();
        }
        finally
        {
            IsBusy = false;
            _isBusyLock.Release();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _isBusyLock?.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
