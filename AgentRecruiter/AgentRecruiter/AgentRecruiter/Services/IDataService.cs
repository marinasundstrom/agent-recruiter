
using AgentRecruiter.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public interface IDataService
    {
        Query Query { get; }

        Task<IEnumerable<Candidate>> GetAcceptedCandidatesAsync();

        Task<IEnumerable<Candidate>> GetRejectedCandidatesAsync();

        Task AddAcceptedCandidateAsync(Candidate candidate);

        Task AddRejectedCandidateAsync(Candidate candidate);

        Task UpdateTechnologiesAsync(IEnumerable<Technology> mappedTechnologies);

        Task LoadAsync();

        Task SaveAsync();

        Task<bool> HasTechnologiesAsync();

        Task<IEnumerable<Technology>> GetTechnologiesAsync();
    }
}