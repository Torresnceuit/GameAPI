using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class Team
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Logo { get; set; }
        [ForeignKey("Tournament")]
        public string TourId { get; set; }
        public virtual Tournament Tournament{get; set; }

        public void Update(Team team)
        {
            Name = team.Name;
            Logo = team.Logo;
            TourId = team.TourId;
            

        }
    }
}