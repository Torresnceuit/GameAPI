using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/Leagues")]
    public class LeaguesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get all leagues
        /// </summary>
        /// <returns>a list of league (League[])</returns>
        [HttpGet]
        [Route("GetAll")]
        public List<League> Get()
        {
            //string userId = User.Identity.GetUserId();

            return db.Leagues.ToList();

        }

        /// <summary>
        /// Get a league by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public League Get(string id)
        {
            Console.WriteLine("id");
            //Guid playerID = new Guid(id);
            //string userId = User.Identity.GetUserId();
            return db.Leagues
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update league in database
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public League Post(League league)
        {
            
            var existLeague = db.Leagues.Where(h => h.Id == league.Id).FirstOrDefault();
            if (existLeague == null)
            {
                existLeague = new League();
                existLeague.Id = league.Id ?? Guid.NewGuid().ToString();
                db.Leagues.Add(existLeague);
            }


            existLeague.Update(league);

            //existHero.Age = hero.Age;

            db.SaveChanges();
            

            return existLeague;
        }


        /// <summary>
        /// Delete a league entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            var existLeague = db.Leagues.Where(h => h.Id == id).FirstOrDefault();
            if (existLeague == null)
                return NotFound();

            db.Leagues.Remove(existLeague);
            db.SaveChanges();
            return Ok(db.Leagues.ToList());
        }

    }
}
