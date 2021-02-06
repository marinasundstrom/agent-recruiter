using AgentRecruiter.Data;
using AgentRecruiter.Models;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace AgentRecruiter.Services
{
    sealed class DataService : IDataService
    {
        private const string QueryFilePath = "query.json";
        private readonly ApplicationDbContext context;

        public DataService(ApplicationDbContext context)
        {
            Query = new Query();
            this.context = context;
        }

        public Query Query { get; private set; }

        public async Task AddAcceptedCandidateAsync(Candidate candidate)
        {
            candidate.IsAccepted = true;
            await context.AcceptedCandidates.AddAsync(candidate);
            await context.SaveChangesAsync();
        }

        public async Task AddRejectedCandidateAsync(Candidate candidate)
        {
            candidate.IsAccepted = false;
            await context.RejectedCandidates.AddAsync(candidate);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Candidate>> GetAcceptedCandidatesAsync()
        {
            return await context.AcceptedCandidates.Where(c => c.IsAccepted).ToListAsync();
        }

        public async Task<IEnumerable<Candidate>> GetRejectedCandidatesAsync()
        {
            return await context.RejectedCandidates.Where(c => !c.IsAccepted).ToListAsync();
        }

        public async Task<IEnumerable<Technology>> GetTechnologiesAsync()
        {
            return await context.Technologies.ToArrayAsync();
        }

        public async Task<bool> HasTechnologiesAsync()
        {
            return await context.Technologies.AnyAsync();
        }

        public Task LoadAsync()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, QueryFilePath);
            if (File.Exists(path))
            {
                this.Query = JsonConvert.DeserializeObject<Query>(File.ReadAllText(path));
            }

            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, QueryFilePath);
            File.WriteAllText(path, JsonConvert.SerializeObject(this.Query));

            return Task.CompletedTask;
        }

        public async Task UpdateTechnologiesAsync(IEnumerable<Technology> technologies)
        {
            foreach (var technology in technologies)
            {
                if (await context.Technologies.AnyAsync(t => t.Name == technology.Name))
                {
                    context.Technologies.Update(technology);
                }
                else
                {
                    context.Technologies.Add(technology);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
