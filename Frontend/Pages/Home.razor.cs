using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Home : ComponentBase
{
    private class ErrorMessageValue
    {
        private CancellationTokenSource? CancellationTokenSource { get; set; }

        public string? Value { get; private set; } = string.Empty;

        public async Task ClearAsync()
        {
            try
            {
                if (CancellationTokenSource is not null)
                    await CancellationTokenSource.CancelAsync();
            }
            catch (ObjectDisposedException)
            {
            }
            finally
            {
                CancellationTokenSource = null;
                Value = null;
            }
        }

        public async Task UpdateValueAsync(string? value, TimeSpan delay, Action afterDelayAction)
        {
            await ClearAsync();
            Value = value;
            RegisterClearTimeout(delay, afterDelayAction);
        }

        private async void RegisterClearTimeout(TimeSpan delay, Action afterDelayAction)
        {
            try
            {
                using (CancellationTokenSource = new CancellationTokenSource())
                {
                    await Task.Delay(delay, CancellationTokenSource.Token);
                }
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                CancellationTokenSource = null;
                Value = null;
                afterDelayAction();
            }
        }
    }
    
    private class HomeState
    {
        public string OriginalUrl { get; set; } = string.Empty;
        public string? ShortenUrl { get; set; }
        public ErrorMessageValue ErrorMessage { get; } = new();
        public bool IsLoading { get; set; } = false;
    }
}