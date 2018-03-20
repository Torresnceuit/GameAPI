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
            //Console.WriteLine("id");
            //Guid playerID = new Guid(id);
            //string userId = User.Identity.GetUserId();
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
            //Console.WriteLine(hero.Positions.ToString());
            var existRank = db.Ranks.Where(h => h.Id == rank.Id).FirstOrDefault();
            if (existRank == null)
            {
                existRank = new Rank();
                existRank.Id = rank.Id ?? Guid.NewGuid().ToString();
                db.Ranks.Add(existRank);
            }


            existRank.Update(rank);

            //existHero.Age = hero.Age;

            db.SaveChanges();


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
            var existRank = db.Ranks.Where(h => h.Id == id).FirstOrDefault();
            if (existRank == null)
                return NotFound();

            db.Ranks.Remove(existRank);
            db.SaveChanges();
            return Ok(db.Ranks.ToList());
        }
    }
}
