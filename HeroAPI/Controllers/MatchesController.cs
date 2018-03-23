using PlayersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlayersAPI.Controllers
{
    [RoutePrefix("api/Matches")]
    public class MatchesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get all matches from database
        /// </summary>
        /// <returns>Match[]</returns>
        [HttpGet]
        [Route("GetAll")]
        public List<Match> Get()
        {
            return db.Matches.ToList();
        }
        /// <summary>
        /// Get all matches by roundId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllByRound/{id}")]
        public List<Match> GetByRound(string id)
        {
            return db.Matches
                .Where(h => h.RoundId == id)
                .ToList();
        }

        /// <summary>
        /// Get a match by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public Match Get(string id)
        {
            
            return db.Matches
                .Where(h => h.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// Update a match in database
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Match Post(Match match)
        {
            // find a match 
            var existMatch = db.Matches.Where(h => h.Id == match.Id).FirstOrDefault();
            if (existMatch == null)
            {
                // if not exist, create a new one
                existMatch = new Match();
                existMatch.Id = match.Id ?? Guid.NewGuid().ToString();
                // add to Matches db
                db.Matches.Add(existMatch);
            }
            // update and save
            existMatch.Update(match);
            db.SaveChanges();
            // return updated match
            return existMatch;
        }

        /// <summary>
        /// Update a given list of matches
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAll")]
        public IHttpActionResult UpdateAll(List<Match> matches)
        {
            foreach(Match match in matches)
            {
                var matchId = match.Id;
                var existMatch = db.Matches.Where(h => h.Id == matchId).FirstOrDefault();
                if (existMatch == null)
                {
                   // return 404 NOT FOUND
                    return NotFound();
                }
                // update a match
                existMatch.Update(match);
            }
            db.SaveChanges();
            // return 200 OK
            return Ok();

            
        }


        /// <summary>
        /// Delete a match entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string id)
        {
            // find a tem matches by id
            var existTeam = db.Teams.Where(h => h.Id == id).FirstOrDefault();
            if (existTeam == null)
                // if null, return 404 NOT FOUND
                return NotFound();
            // remove from database
            db.Teams.Remove(existTeam);
            db.SaveChanges();
            //return 200 OK
            return Ok();
        }
    }
}
