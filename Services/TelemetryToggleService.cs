using Microsoft.Extensions.Options;
using OpenTelemetryAIDemo.Handlers;
using OpenTelemetryAIDemo.Services;

public class TelemetryToggleService : ITelemetryToggleService
{
   private bool _isEnabled;

   public TelemetryToggleService(IOptions<TelemetryConfig> options)
   {
      _isEnabled = options.Value.IsAutomaticTelemetryEnabled;
   }

   public bool IsEnabled => _isEnabled;

   public void Toggle()
   {
      _isEnabled = !_isEnabled;
   }
}
