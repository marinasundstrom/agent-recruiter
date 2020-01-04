using Microsoft.Extensions.DependencyInjection;

using System;

namespace AgentRecruiter.ViewModels
{
    static class ViewModelLocator
    {
        public static CandidatesViewModel Candidates => ServiceProvider.GetService<CandidatesViewModel>();
        public static MatchCriteriaViewModel MatchCriteria => ServiceProvider.GetService<MatchCriteriaViewModel>();
        public static SwipeMatchesViewModel SwipeMatches => ServiceProvider.GetService<SwipeMatchesViewModel>();

        public static IServiceProvider ServiceProvider { get; internal set; }
    }
}
