using PlayersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlayersAPI.Controllers
{

    [RoutePrefix("api/Generator")]
    public class GeneratorController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private const int BYE = -1;
        /// <summary>
        /// Generate a rank for a tournament, return OK 200
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generateRank/{id}")]
        public IHttpActionResult GenerateRank(string id)
        {
            List<Team> teams = db.Teams.Where(h => h.TourId == id).ToList();
            foreach (Team team in teams)
            {
                var teamId = team.Id;
                List<Match> matches = db.Matches
                                        .Where(h => h.TourId == id && (h.HomeId == teamId || h.AwayId == teamId)
                                         && h.IsDone == true)
                                        .ToList();
                //check if rank exist, each rank has unique teamId
                var existRank = db.Ranks.Where(h => h.TeamId == teamId && h.TourId == id).FirstOrDefault();
                if (existRank == null)
                {
                    existRank = new Rank();
                    existRank.TeamId = teamId;
                    existRank.TourId = id;
                    db.Ranks.Add(existRank);
                }
                //restart the value of rank
                existRank.Reset();
                foreach (Match match in matches)
                {
                    //Team is Home Team
                    if (match.HomeId == teamId)
                    {
                        // if won, points + 3
                        if (match.HomeScore > match.AwayScore)
                        {
                            existRank.Won += 1;
                            existRank.Goals += match.HomeScore;
                            existRank.Concede += match.AwayScore;
                            existRank.Points += 3;
                        }
                        // if lost, points not changed
                        if (match.HomeScore < match.AwayScore)
                        {
                            existRank.Lost += 1;
                            existRank.Goals += match.HomeScore;
                            existRank.Concede += match.AwayScore;
                        }
                        // if draw, points + 1
                        if (match.HomeScore == match.AwayScore)
                        {
                            existRank.Draw += 1;
                            existRank.Goals += match.HomeScore;
                            existRank.Concede += match.AwayScore;
                            existRank.Points += 1;
                        }

                    }
                    if (match.AwayId == teamId)
                    {
                        // if won, +3 to points
                        if (match.AwayScore > match.HomeScore)
                        {
                            existRank.Won += 1;
                            existRank.Goals += match.AwayScore;
                            existRank.Concede += match.HomeScore;
                            existRank.Points += 3;

                        }
                        // if lost, points not changed
                        if (match.AwayScore < match.HomeScore)
                        {
                            existRank.Lost += 1;
                            existRank.Goals += match.AwayScore;
                            existRank.Concede += match.HomeScore;
                        }
                        // if draw, +1 to points
                        if (match.HomeScore == match.AwayScore)
                        {
                            existRank.Draw += 1;
                            existRank.Goals += match.AwayScore;
                            existRank.Concede += match.HomeScore;
                            existRank.Points += 1;
                        }
                    }
                    existRank.Games = matches.Count;
                }
            }
            db.SaveChanges();
            // return 200
            return Ok();
        }
        /// <summary>
        /// Draw new season fixtures
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("drawfixture/{id}")]
        public IHttpActionResult DrawFixture(string id)
        {
			List<Round> lRounds = db.Rounds.Where(h => h.TourId == id).ToList();
            List<Match> lMatches = db.Matches.Where(h => h.TourId == id).ToList();
            List<Rank> lRanks = db.Ranks.Where(h => h.TourId == id).ToList(); 
            // delete all data for the tournament first			
            foreach (Rank rank in lRanks)
            {
                db.Ranks.Remove(rank);
            }
            foreach (Round round in lRounds)
            {
                db.Rounds.Remove(round);
            }
            foreach (Match match in lMatches)
            {
                db.Matches.Remove(match);
            }

            db.SaveChanges();
            //Start generate fixture after empty it
            int num_teams = db.Teams
                            .Where(h => h.TourId == id)
                            .Count();
            List<Team> lTeams = db.Teams
                                   .Where(h => h.TourId == id)
                                    .ToList();

            int[,] results = GenerateRoundRobin(num_teams);
            int n = 0;
			// n = 2, each team plays againts others 2 times: Home and Away
            while (n < 2)
            {
				
                for (int rounds = 0; rounds < num_teams - 1; rounds++)
                {
					// Create a round
                    Round round = new Round();
                    round.Name = (num_teams - 1) * n + rounds + 1;
                    round.TourId = id;
					// add to Rounds db
                    db.Rounds.Add(round);
                    db.SaveChanges();
					// each round, we generate pairs of teams
                    for (int teams = 0; teams < num_teams; teams++)
                    {
						// Create a new match
                        Match match = new Match();
                        match.TourId = id;
						// fist leg round
                        if (n == 0)
                        {
							// assign Home team
                            match.HomeId = lTeams.ElementAt(teams).Id;
                            if (results[teams, rounds] >= 0)
								// assign Away team
                                match.AwayId = lTeams.ElementAt(results[teams, rounds]).Id;
                        }
						 // 2nd leg round
                        else
                        {
							// assgin Away team
                            match.AwayId = lTeams.ElementAt(teams).Id;
                            if (results[teams, rounds] >= 0)
								// assign Home team
                                match.HomeId = lTeams.ElementAt(results[teams, rounds]).Id;
                        }
                        match.RoundId = round.Id;
						// check if match does not exist
                        if (db.Matches
                            .Where(h => (h.HomeId == match.HomeId || h.HomeId == match.AwayId) && h.RoundId == match.RoundId)
                            .ToList().Count == 0)
                        {
							// add match to Matches db
                            db.Matches.Add(match);
                            db.SaveChanges();

                        }
                    }                   
                }
                n++;
            }
			// return 200
            return Ok();
        }
        
        // Generate Round Robin Algorithm
        public int[,] GenerateRoundRobin(int num_teams)
        {
			// Even number of teams
            if (num_teams % 2 == 0)
                return GenerateRoundRobinEven(num_teams);
			// Odd number of teams
            else
                return GenerateRoundRobinOdd(num_teams);
        }

        // Round Robin For Odd number of teams
        public int[,] GenerateRoundRobinOdd(int num_teams)
        {
            int n2 = (int)((num_teams - 1) / 2);
            int[,] results = new int[num_teams, num_teams];

            // Initialize the list of teams.
            int[] teams = new int[num_teams];
            for (int i = 0; i < num_teams; i++) teams[i] = i;


            // Start the rounds.
            for (int round = 0; round < num_teams; round++)
            {
                for (int i = 0; i < n2; i++)
                {
                    int team1 = teams[n2 - i];
                    int team2 = teams[n2 + i + 1];
                    results[team1, round] = team2;
                    results[team2, round] = team1;
                }
                // Set the team with the bye.
                results[teams[0], round] = BYE;
                // Rotate the array.
                RotateArray(teams);
            }
            return results;
        }

        // Rotate Array/List to the right by 1 element
        private void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }
        static void RotateList<T>(IList<T> list, int places)
        {
            // circular.. Do Nothing
            if (places % list.Count == 0)
                return;
            T[] copy = new T[list.Count];
            list.CopyTo(copy, 0);
            for (int i = 0; i < list.Count; i++)
            {
                // % used to handle circular indexes and places > count case
                int index = (i + places) % list.Count;
                list[i] = copy[index];
            }
        }
        // Round Robin For Even number of teams
        public int[,] GenerateRoundRobinEven(int num_teams)
        {
            // Generate the result for one fewer teams.
            int[,] results = GenerateRoundRobinOdd(num_teams - 1);

            // Copy the results into a bigger array,
            // replacing the byes with the extra team.
            int[,] results2 = new int[num_teams, num_teams - 1];
            for (int team = 0; team < num_teams - 1; team++)
            {
                for (int round = 0; round < num_teams - 1; round++)
                {
                    if (results[team, round] == BYE)
                    {
                        // Change the bye to the new team.
                        results2[team, round] = num_teams - 1;
                        results2[num_teams - 1, round] = team;
                    }
                    else
                    {
                        results2[team, round] = results[team, round];
                    }
                }
            }

            return results2;
        }

    }
}
