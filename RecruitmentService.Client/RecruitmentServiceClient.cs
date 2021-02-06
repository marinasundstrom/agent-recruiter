using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            var str = await httpClient.GetStringAsync("/api/candidates");
            return JsonConvert.DeserializeObject<IEnumerable<Candidate>>(str);
        }

        public async Task<IEnumerable<Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default)
        {
            var str = await httpClient.GetStringAsync("/api/technologies");
            return JsonConvert.DeserializeObject<IEnumerable<Technology>>(str);
        }
    }

}
