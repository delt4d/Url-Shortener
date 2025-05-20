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
            if (CancellationTokenSource is not null)
            {
                if (CancellationTokenSource.Token.CanBeCanceled)
                    await CancellationTokenSource.CancelAsync();

                CancellationTokenSource = null;
            }

            Value = null;
        }

        public async Task UpdateValueAsync(string? value, TimeSpan delay, Action onTimeout)
        {
            await ClearAsync();
            Value = value;
            RegisterClearTimeout(delay, onTimeout);
        }

        private void RegisterClearTimeout(TimeSpan delay, Action onTimeout)
        {
            Task.Run(async () =>
            {
                using (CancellationTokenSource = new CancellationTokenSource())
                    await Task.Delay(delay, CancellationTokenSource.Token);
                await ClearAsync();
                onTimeout();
            });
        }
    }
    
    private class HomeState
    {
        public string OriginalUrl { get; set; } = string.Empty;
        public string? ShortenUrl { get; set; }
        public ErrorMessageValue ErrorMessage { get; set; } = new();
    }
}