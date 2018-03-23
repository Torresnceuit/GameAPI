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
    [RoutePrefix("api/Teams")]
    public class TeamsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Get all teams from database
        /// </summary>
        /// <returns>List of teams</returns>
        [HttpGet]
        [Route("GetAll")]
        public List<Team> Get()
        {
            return db.Teams.ToList();
        }
        /// <summary>
        /// Get all teams by tournament Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllByTour/{id}")]
        public List<Team> GetByTour(string id)
        {
            return db.Teams
                .Where(h => h.TourId == id)
                .ToList();
        }

        /// <summary>
        /// Get a team by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public Team Get(string id)
        {
            return db.Teams
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update a team in database
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Team Post(Team team)
        {
            // find a team in Teams db
            var existTeam = db.Teams.Where(h => h.Id == team.Id).FirstOrDefault();
            if (existTeam == null)
            {
                // if not exist, create a new one
                existTeam = new Team();
                existTeam.Id = team.Id ?? Guid.NewGuid().ToString();
                // add to Teams db
                db.Teams.Add(existTeam);
            }
            // update a team
            existTeam.Update(team);
            db.SaveChanges();
            // return updated team
            return existTeam;
        }


        /// <summary>
        /// Delete a team entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            // find team in Teams db matches id
            var existTeam = db.Teams.Where(h => h.Id == id).FirstOrDefault();
            if (existTeam == null)
                // return 404 NOT FOUND
                return NotFound();
            // remove from Teams db
            db.Teams.Remove(existTeam);
            db.SaveChanges();
            // return 200 OK
            return Ok();
        }
    }
}
