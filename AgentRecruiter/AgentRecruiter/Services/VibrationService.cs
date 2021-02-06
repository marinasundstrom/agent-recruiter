using System.Threading.Tasks;

using Xamarin.Essentials;

namespace AgentRecruiter.Services
{
    sealed class VibrationService : IVibrationService
    {
        public Task VibrateAsync()
        {
            Vibration.Vibrate();

            return Task.CompletedTask;
        }
    }
}
