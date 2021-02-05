using System;
using System.Collections.Generic;
using System.Linq;
using RecruitmentService.Client;
using static Bogus.DataSets.Name;

namespace WebAPI.Controllers
{
    internal class FakeData
    {
        private static IEnumerable<Candidate> candidates;
        private static IEnumerable<Technology> technologies;
        private static List<string> languages = new List<string> { "English", "Spanish", "Arabaic", "Italian", "Hebrew", "Swedish", "Danish", "Farsi", "Mandarin", "Hindi" };

        public static IEnumerable<Candidate> GetCandiates()
        {
            if (candidates == null)
            {
                Random rand = new Random((int)DateTime.Now.Ticks);

                var ids = 0;

                var faker = new Bogus.Faker<Candidate>()
                                .RuleFor(u => u.Id, (f, u) => (++ids).ToString())
                                .RuleFor(u => u.Guid, (f, u) => f.Random.Guid().ToString())
                                .RuleFor(u => u.Name, (f, u) => new CandidateName
                                {
                                    FirstName = f.Name.FirstName(Gender.Female),
                                    LastName = f.Name.LastName(Gender.Female)
                                })
                                .RuleFor(u => u.About, (f, u) => f.Lorem.Paragraph())
                                .RuleFor(u => u.FullResume, (f, u) => f.Lorem.Paragraph(5))
                                .RuleFor(u => u.Picture, (f, u) => new Uri(f.Image.PicsumUrl(400, 400)))
                                .RuleFor(u => u.CurrentCompany, (f, u) => f.Company.CompanyName())
                                .RuleFor(u => u.IsActive, (f, u) => f.Random.Bool())
                                .RuleFor(u => u.LastKnownLocation, (f, u) => new Coordinates() { Latitude = f.Address.Latitude().ToString(), Longitude = f.Address.Longitude().ToString() })
                                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name.FirstName, u.Name.LastName))
                                .RuleFor(u => u.Phone, (f, u) => f.Phone.PhoneNumber())
                                .RuleFor(u => u.Address, (f, u) => f.Address.FullAddress())
                                .RuleFor(u => u.EyeColor, (f, u) => f.Internet.Color())
                                .RuleFor(u => u.Age, (f, u) => f.Random.Int(18, 65))
                                .RuleFor(u => u.Technologies, (f, u) => f.Random.ListItems(GetTechnologies() as List<Technology>, 3).Select(x => new CandidatetTechnologyExperience()
                                {
                                    Name = x.Name,
                                    ExperienceYears = f.Random.Int(1, 10)
                                }).ToArray())
                                .RuleFor(u => u.Languages, (f, u) => f.Random.ListItems(languages, 3).ToArray());

                candidates = faker.Generate(50);
            }

            return candidates;
        }


        public static IEnumerable<Technology> GetTechnologies()
        {
            if (technologies == null)
            {
                Random rand = new Random((int)DateTime.Now.Ticks);

                technologies = Enumerable.Range(1, 10).Select(x => new Technology()
                {
                    Name = $"Technology {x}",
                    Votes = rand.Next(15)
                }).ToList();
            }

            return technologies;
        }
    }
}