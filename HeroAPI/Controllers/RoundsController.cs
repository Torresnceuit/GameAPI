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
    [RoutePrefix("api/Rounds")]
    public class RoundsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Get all rounds from database
        /// </summary>
        /// <returns>List of rounds</returns>
        [HttpGet]
        [Route("GetAll")]
        public List<Round> Get()
        {
            return db.Rounds.ToList();
        }
        /// <summary>
        /// Get all rounds by tournament Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllByTour/{id}")]
        public List<Round> GetByTour(string id)
        {
            return db.Rounds
                .Where(h => h.TourId == id)
                .OrderBy(h => h.Name)
                .ToList();
        }

        /// <summary>
        /// Get a round by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public Round Get(string id)
        {
            return db.Rounds
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update a round in database
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Round Post(Round round)
        {
            // find round in Rounds db
            var existRound = db.Rounds.Where(h => h.Id == round.Id).FirstOrDefault();
            if (existRound == null)
            {
                // if not exist, create a new one
                existRound = new Round();
                existRound.Id = round.Id ?? Guid.NewGuid().ToString();
                // add to Rounds db
                db.Rounds.Add(existRound);
            }
            // update a round
            existRound.Update(round);
            db.SaveChanges();
            // return the updated round
            return existRound;
        }


        /// <summary>
        /// Delete a round entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string id)
        {
            // find a round matches id
            var existRound = db.Rounds.Where(h => h.Id == id).FirstOrDefault();
            if (existRound == null)
                // return 404 NOT FOUND
                return NotFound();
            // remove from Rounds db
            db.Rounds.Remove(existRound);
            db.SaveChanges();
            // return 200 OK
            return Ok();
        }
    }
}
