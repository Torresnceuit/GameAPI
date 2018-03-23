using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class Player
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Hero name
        /// </summary>
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string Nationality { get; set; }
        public int Number { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }
        public string Avatar { get; set; }

        public virtual Team Team { get; set; }
        // update members
        public void Update(PlayerViewModel player)
        {
            Name = player.Name;
            Age = player.Age;
            Position = string.Join(",",player.Positions);
            Nationality = player.Nationality;
            Number = player.Number;
            TeamId = player.TeamId;
            Avatar = player.Avatar;

        }
    }
}