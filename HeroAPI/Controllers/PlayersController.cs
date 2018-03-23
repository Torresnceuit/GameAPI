using PlayersAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace PlayersAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Players")]
    public class PlayersController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();




        /// <summary>
        /// Get players and return a list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public List<PlayerViewModel> Get()
        {
            string userId = User.Identity.GetUserId();
            
            return db.Players
                
                .ToList()
                .Select(h=> new PlayerViewModel(h))
                .ToList();

        }

        /// <summary>
        /// Get all players by team Id
        /// </summary>
        /// <returns></returns>

        [Route("GetAllByTeam/{id}")]
        public List<PlayerViewModel> GetByTeam(String id)
        {
            

            return db.Players
                .Where(h=>h.TeamId == id)
                .ToList()
                .Select(h => new PlayerViewModel(h))
                .ToList();

        }

        /// <summary>
        /// Get a player by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public PlayerViewModel Get(string id)
        {
            Console.WriteLine("id");
            //Guid playerID = new Guid(id);
            string userId = User.Identity.GetUserId();
            return new PlayerViewModel(db.Players
                .Where(h =>  h.Id == id).FirstOrDefault());
        }
        /// <summary>
        /// Update a player in database
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public PlayerViewModel Post(PlayerViewModel player)
        {
            Console.WriteLine(player.Positions.ToString());
            // query for player exists or not
            var existPlayer = db.Players.Where(h => h.Id == player.Id).FirstOrDefault();
            if (existPlayer == null)
            {
                // if not exist, create a new player
                existPlayer = new Player();
                existPlayer.Id = player.Id ?? Guid.NewGuid().ToString();
                // add to Players db
                db.Players.Add(existPlayer);
            }
            
            // update player
            existPlayer.Update(player);

            db.SaveChanges();
            PlayerViewModel newView = new PlayerViewModel(existPlayer);

            return newView ;
        }

       
        /// <summary>
        /// Delete a player entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            // find player matchs id
            var existHero = db.Players.Where(h => h.Id == id).FirstOrDefault();
            if (existHero == null)
                // return 404 NOT FOUND
                return NotFound();     
            // remove player from Players database
            db.Players.Remove(existHero);          
            db.SaveChanges();
            // return 200 OK
            return Ok();
        }

        /// <summary>
        /// Upload a file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public HttpResponseMessage UploadJsonFile()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            // check number of files
            if (httpRequest.Files.Count > 0)
            {
                // get upload file count
                int filecount = httpRequest.Files.Count; 
                //show file count in custom response header
                HttpContext.Current.Response.AppendHeader("FileCount", filecount.ToString());
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    // create GUID for image
                    string imgID = Guid.NewGuid().ToString();
                    String[] substring = postedFile.FileName.Split('.');
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/Upload/" + imgID+'.'+substring[substring.Length-1]);
                    // save file to specific file path.
                    postedFile.SaveAs(filePath); 
                    // add the url to file
                    docfiles.Add("/Content/Upload/" + imgID+'.'+ substring[substring.Length - 1]);
                }
                // create response contains file url
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
