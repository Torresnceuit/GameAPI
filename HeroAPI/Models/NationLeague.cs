using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class NationLeague
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string Logo { get; set; }
    }
}