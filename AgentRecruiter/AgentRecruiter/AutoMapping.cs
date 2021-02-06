using AgentRecruiter.Models;

using AutoMapper;

namespace AgentRecruiter
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RecruitmentService.Client.Candidate, Candidate>();
            CreateMap<RecruitmentService.Client.CandidateName, CandidateName>();
            CreateMap<RecruitmentService.Client.CandidatetTechnologyExperience, CandidateTechnologyExperience>();
            CreateMap<RecruitmentService.Client.Coordinates, Coordinates>();
            CreateMap<RecruitmentService.Client.Technology, Technology>();

            CreateMap<string, CandidateLanguage>()
                .ConvertUsing(l => new CandidateLanguage { Name = l });
        }
    }
}
