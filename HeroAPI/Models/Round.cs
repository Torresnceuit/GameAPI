using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayersAPI.Models
{
    public class Round
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Name { get; set; }
        
        public bool IsDone { get; set; }
        [ForeignKey("Tournament")]
        public string TourId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public void Update(Round round)
        {
            if(round.Name>0)
                Name = round.Name;
            if(round.TourId!=null)
                TourId = round.TourId;

            IsDone = round.IsDone;
            

        }
    }
}