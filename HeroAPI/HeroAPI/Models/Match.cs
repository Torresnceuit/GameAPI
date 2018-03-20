using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class Match
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool IsDone   { get; set; }

        [ForeignKey("homeTeam")]
        public string HomeId { get; set; }

        [ForeignKey("awayTeam")]
        public string AwayId { get; set; }

        [ForeignKey("Round")]
        public string RoundId { get; set; }

        [ForeignKey("Tour")]
        public string TourId { get; set; }

        public virtual Team homeTeam { get; set; }
        public virtual Team awayTeam { get; set; }
        public virtual Tournament Tour { get; set; }
        public virtual Round Round { get; set; }

        public void Update(Match match)
        {
            HomeScore = match.HomeScore;
            AwayScore = match.AwayScore;
            HomeId = match.HomeId;
            AwayId = match.AwayId;
            RoundId = match.RoundId;
            TourId = match.TourId;
            IsDone = match.IsDone;

        }

    }
}