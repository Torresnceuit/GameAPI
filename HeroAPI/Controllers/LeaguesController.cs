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
            // Find a league in database
            var existLeague = db.Leagues.Where(h => h.Id == league.Id).FirstOrDefault();
            if (existLeague == null)
            {
                // if not exist, create a new League
                existLeague = new League();
                existLeague.Id = league.Id ?? Guid.NewGuid().ToString();
                // add to Leagues db
                db.Leagues.Add(existLeague);
            }
            //Update league and save
            existLeague.Update(league);
            db.SaveChanges();
            
            // return the updated league
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
            // find league in database
            var existLeague = db.Leagues.Where(h => h.Id == id).FirstOrDefault();
            if (existLeague == null)
                //return 404 NOT FOUND 
                return NotFound();
            // remove league from database
            db.Leagues.Remove(existLeague);
            db.SaveChanges();
            // return 200 OK
            return Ok();
        }

    }
}
