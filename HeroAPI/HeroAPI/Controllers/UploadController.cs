﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PlayersAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
        /// <summary>
        /// Upload a file and return a url
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Image")]
        public HttpResponseMessage UploadJsonFile()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                int filecount = httpRequest.Files.Count; // get upload file count
                //show file count in custom response header
                HttpContext.Current.Response.AppendHeader("FileCount", filecount.ToString());
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string imgID = Guid.NewGuid().ToString();
                    String[] substring = postedFile.FileName.Split('.');
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/Upload/" + imgID + '.' + substring[substring.Length - 1]);
                    postedFile.SaveAs(filePath); // save file to specific file path.
                    docfiles.Add("/Content/Upload/" + imgID + '.' + substring[substring.Length - 1]); // add file url on server to response
                }
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
