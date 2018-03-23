using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class PlayerViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Hero name
        /// </summary>
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Positions { get; set; }
        public string Nationality { get; set; }
        public int Number { get; set; }


        public string TeamId { get; set; }
        public string Avatar { get; set; }
        
        public PlayerViewModel (Player player)
        {
            Id = player.Id;
            Name = player.Name;
            Age = player.Age;
            
            Positions = player.Position?.Split(',').ToList() ?? new List<string>();
            Nationality = player.Nationality;
            Number = player.Number;
            TeamId = player.TeamId;
            Avatar = player.Avatar;

        }

    }
}