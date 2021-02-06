using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentService.Client;
using static Bogus.DataSets.Name;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Candidate> Get()
        {
            return FakeData.GetCandiates();
        }
    }
}