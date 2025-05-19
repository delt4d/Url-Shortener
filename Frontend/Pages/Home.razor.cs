using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Home : ComponentBase
{
    private class ErrorMessageValue
    {
        private string? _value = string.Empty;
        private CancellationTokenSource? CancellationTokenSource { get; set; }

        public string? Value { get => _value; }

        public async Task CancelAsync()
        {
            if (CancellationTokenSource is not null)
            {
                await CancellationTokenSource.CancelAsync();
                CancellationTokenSource = null;
            }
        }

        public async Task UpdateValueAsync(string? value, TimeSpan delay)
        {
            await CancelAsync();

            _value = value;

            Task.Run(async () =>
            {
                using (CancellationTokenSource = new CancellationTokenSource())
                {
                    await Task.Delay(delay);
                    _value = null;
                }

                CancellationTokenSource = null;
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