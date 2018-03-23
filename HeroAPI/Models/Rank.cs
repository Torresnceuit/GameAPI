using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class Rank
    {
        public string   Id        { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(Tour))]
        public string   TourId    { get; set; }
        [ForeignKey(nameof(Team))]
        public string   TeamId    { get; set; }
        public int      Games     { get; set; }
        public int      Won       { get; set; }
        public int      Lost      { get; set; }
        public int      Draw      { get; set; }
        public int      Goals     { get; set; }
        public int      Concede   { get; set; }
        public int      Points    { get; set; }

        public virtual Tournament Tour { get; set; }
        public virtual Team       Team { get; set; }

        // update all rank members
        public void Update(Rank rank)
        {
            TourId = rank.TourId;
            TeamId = rank.TeamId;
            Won = rank.Won;
            Lost = rank.Lost;
            Draw = rank.Draw;
            Goals = rank.Goals;
            Concede = rank.Concede;
            Points = rank.Points;

        }
        // reset all members' value
        public void Reset()
        {
            Games = 0;
            Won = 0;
            Lost = 0;
            Draw = 0;
            Goals = 0;
            Concede = 0;
            Points = 0;

        }


    } 
}