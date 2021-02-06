using Microsoft.EntityFrameworkCore;

namespace AgentRecruiter.Models
{
    [Owned]
    public class Coordinates
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
}