using RecruitmentService.Client;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace AgentRecruiter.Services
{
    class DataService : IDataService
    {
        private const string QueryFilePath = "query.json";
        private const string CandidatesAcceptedPath = "candidates-accepted.json";
        private const string CandidatesRejectedPath = "candidates-rejected.json";

        public DataService()
        {
            Query = new Query();
            AcceptedCandidates = new List<Candidate>();
            RejectedCandidates = new List<string>();
        }

        public Query Query { get; private set; }

        public IList<Candidate> AcceptedCandidates { get; private set; }

        public IList<string> RejectedCandidates { get; private set; }

        public Task LoadAsync()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, QueryFilePath);
            if (File.Exists(path))
            {
                this.Query = JsonSerializer.Deserialize<Query>(File.ReadAllText(path));
            }

            path = Path.Combine(FileSystem.AppDataDirectory, CandidatesAcceptedPath);
            if (File.Exists(path))
            {
                this.AcceptedCandidates = JsonSerializer.Deserialize<List<Candidate>>(File.ReadAllText(path));
            }

            path = Path.Combine(FileSystem.AppDataDirectory, CandidatesRejectedPath);
            if (File.Exists(path))
            {
                this.RejectedCandidates = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(path));
            }

            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, QueryFilePath);
            File.WriteAllText(path, JsonSerializer.Serialize(this.Query));

            path = Path.Combine(FileSystem.AppDataDirectory, CandidatesAcceptedPath);
            File.WriteAllText(path, JsonSerializer.Serialize(this.AcceptedCandidates));

            path = Path.Combine(FileSystem.AppDataDirectory, CandidatesRejectedPath);
            File.WriteAllText(path, JsonSerializer.Serialize(this.RejectedCandidates));

            return Task.CompletedTask;
        }
    }
}
