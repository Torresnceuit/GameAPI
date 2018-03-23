using PlayersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlayersAPI.Controllers
{   
    [Authorize]
    [RoutePrefix("api/Tournaments")]
    public class TournamentsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Get all tournaments from database
        /// </summary>
        /// <returns>List of tournaments</returns>
        [HttpGet]
        [Route("GetAll")]
        public List<Tournament> Get()
        {
            return db.Tournaments.ToList();
        }
        /// <summary>
        /// Get all tournament by leagueID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllByLeague/{id}")]
        public List<Tournament> GetByLeague(string id)
        {
            return db.Tournaments
                .Where(h => h.LeagueId == id)
                .ToList();           
        }

        /// <summary>
        /// Get a tournament by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public Tournament Get(string id)
        {           
            return db.Tournaments
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update a tournament in database
        /// </summary>
        /// <param name="tour"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Tournament Post(Tournament tour)
        { 
            // find a tournament in Tournaments db 
            var existTour = db.Tournaments.Where(h => h.Id == tour.Id).FirstOrDefault();
            if (existTour == null)
            {
                // if not exist, create a new one
                existTour = new Tournament();
                existTour.Id = tour.Id ?? Guid.NewGuid().ToString();
                // add to Tournaments db
                db.Tournaments.Add(existTour);
            }
            // update to db
            existTour.Update(tour);
            db.SaveChanges();
            // return the update one
            return existTour;
        }


        /// <summary>
        /// Delete a tournament entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            // find a tournment in db matches id
            var existTour = db.Tournaments.Where(h => h.Id == id).FirstOrDefault();
            if (existTour == null)
                // return 404
                return NotFound();
            // remove from Tournaments db
            db.Tournaments.Remove(existTour);
            db.SaveChanges();
            // return 200
            return Ok();
        }
    }
}
