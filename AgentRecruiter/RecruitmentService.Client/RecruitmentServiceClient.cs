using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentService.Client
{
    public class RecruitmentServiceClient : IRecruitmentServiceClient
    {
        private readonly HttpClient httpClient;

        public RecruitmentServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync(CancellationToken cancellationToken = default)
        {
            var stream = await httpClient.GetStreamAsync("/candidates");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Candidate>>(stream, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default)
        {
            var stream = await httpClient.GetStreamAsync("/technologies");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Technology>>(stream, cancellationToken: cancellationToken);
        }
    }

}
