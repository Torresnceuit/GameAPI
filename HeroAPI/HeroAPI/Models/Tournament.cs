using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
   
    public class Tournament
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int NoOfTeam { get; set; }
        public string Logo { get; set; }
        public bool IsDone { get; set; }

        [ForeignKey(nameof(League))]
        public string LeagueId { get; set; }

        public virtual League League { get; set; }
        // update members
        public void Update(Tournament tour)
        {
            Name = tour.Name;
            Logo = tour.Logo;
            NoOfTeam = tour.NoOfTeam;
            LeagueId = tour.LeagueId;

        }
    }
}