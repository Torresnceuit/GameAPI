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
            //Console.WriteLine(hero.Positions.ToString());
            var existTour = db.Tournaments.Where(h => h.Id == tour.Id).FirstOrDefault();
            if (existTour == null)
            {
                existTour = new Tournament();
                existTour.Id = tour.Id ?? Guid.NewGuid().ToString();
                db.Tournaments.Add(existTour);
            }


            existTour.Update(tour);

            //existHero.Age = hero.Age;

            db.SaveChanges();


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
            var existTour = db.Tournaments.Where(h => h.Id == id).FirstOrDefault();
            if (existTour == null)
                return NotFound();

            db.Tournaments.Remove(existTour);
            db.SaveChanges();
            return Ok(db.Tournaments.ToList());
        }
    }
}
