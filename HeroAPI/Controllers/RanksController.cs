using PlayersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlayersAPI.Controllers
{
    [RoutePrefix("api/Ranks")]
    public class RanksController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Get all ranks from databse
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public List<Rank> Get()
        {
            return db.Ranks.ToList();
        }
        /// <summary>
        /// Get all ranks by tournament Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllByTour/{id}")]
        public List<Rank> GetByTour(string id)
        {
            return db.Ranks
                .Where(h => h.TourId == id)
                .OrderByDescending(l=>l.Points)
                .ToList();
        }

        /// <summary>
        /// Get a rank by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public Rank Get(string id)
        {            
            return db.Ranks
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update a rank in database
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Rank Post(Rank rank)
        {
            // find a rank in db
            var existRank = db.Ranks.Where(h => h.Id == rank.Id).FirstOrDefault();
            if (existRank == null)
            {
                // if not exist, create a new one
                existRank = new Rank();
                existRank.Id = rank.Id ?? Guid.NewGuid().ToString();
                // add to Ranks db
                db.Ranks.Add(existRank);
            }
            // update the rank
            existRank.Update(rank);
            db.SaveChanges();
            // return updated rank
            return existRank;
        }


        /// <summary>
        /// Delete a rank entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string id)
        {
            // find a rank in Ranks db
            var existRank = db.Ranks.Where(h => h.Id == id).FirstOrDefault();
            if (existRank == null)
                // return 404 NOT FOUND
                return NotFound();
            // remove from Ranks db
            db.Ranks.Remove(existRank);
            db.SaveChanges();
            // return 200 OK
            return Ok();
        }
    }
}
