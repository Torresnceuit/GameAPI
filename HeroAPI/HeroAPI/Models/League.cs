using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class League
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        
        public string Logo { get; set; }

        public void Update(League league)
        {
            Name = league.Name;
            Logo = league.Logo;

        }

    }

}