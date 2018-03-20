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
            //Console.WriteLine("id");
            //Guid playerID = new Guid(id);
            //string userId = User.Identity.GetUserId();
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
            //Console.WriteLine(hero.Positions.ToString());
            var existTeam = db.Teams.Where(h => h.Id == team.Id).FirstOrDefault();
            if (existTeam == null)
            {
                existTeam = new Team();
                existTeam.Id = team.Id ?? Guid.NewGuid().ToString();
                db.Teams.Add(existTeam);
            }


            existTeam.Update(team);

            //existHero.Age = hero.Age;

            db.SaveChanges();


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
            var existTeam = db.Teams.Where(h => h.Id == id).FirstOrDefault();
            if (existTeam == null)
                return NotFound();

            db.Teams.Remove(existTeam);
            db.SaveChanges();
            return Ok(db.Teams.ToList());
        }
    }
}
