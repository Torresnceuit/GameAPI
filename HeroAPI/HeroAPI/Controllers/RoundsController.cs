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
            //Console.WriteLine("id");
            //Guid playerID = new Guid(id);
            //string userId = User.Identity.GetUserId();
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
            //Console.WriteLine(hero.Positions.ToString());
            var existRound = db.Rounds.Where(h => h.Id == round.Id).FirstOrDefault();
            if (existRound == null)
            {
                existRound = new Round();
                existRound.Id = round.Id ?? Guid.NewGuid().ToString();
                db.Rounds.Add(existRound);
            }


            existRound.Update(round);

            //existHero.Age = hero.Age;

            db.SaveChanges();


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
            var existRound = db.Rounds.Where(h => h.Id == id).FirstOrDefault();
            if (existRound == null)
                return NotFound();

            db.Rounds.Remove(existRound);
            db.SaveChanges();
            return Ok(db.Rounds.ToList());
        }
    }
}
