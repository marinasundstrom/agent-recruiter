using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public interface IAlertService
    {
        Task DisplayAlertAsync(string title, string message, string cancel);
    }
}