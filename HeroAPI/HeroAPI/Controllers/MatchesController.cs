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
            //Console.WriteLine(hero.Positions.ToString());
            var existMatch = db.Matches.Where(h => h.Id == match.Id).FirstOrDefault();
            if (existMatch == null)
            {
                existMatch = new Match();
                existMatch.Id = match.Id ?? Guid.NewGuid().ToString();
                db.Matches.Add(existMatch);
            }


            existMatch.Update(match);

            //existHero.Age = hero.Age;

            db.SaveChanges();


            return existMatch;
        }

        /// <summary>
        /// Update a given list of matches
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAll")]
        public IHttpActionResult UpdateAll(List<Match> match)
        {
            //Console.WriteLine(hero.Positions.ToString());
            for(int i = 0; i< match.Count; i++)
            {
                var matchId = match[i].Id;
                var existMatch = db.Matches.Where(h => h.Id == matchId).FirstOrDefault();
                if (existMatch == null)
                {
                    /*existMatch = new Match();
                    existMatch.Id = match[i].Id ?? Guid.NewGuid().ToString();
                    db.Matches.Add(existMatch);*/
                    return NotFound();
                    //return;
                }


                existMatch.Update(match[i]);
            }
            

            //existHero.Age = hero.Age;

            db.SaveChanges();

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
            var existTeam = db.Teams.Where(h => h.Id == id).FirstOrDefault();
            if (existTeam == null)
                return NotFound();

            db.Teams.Remove(existTeam);
            db.SaveChanges();
            return Ok(db.Teams.ToList());
        }
    }
}
