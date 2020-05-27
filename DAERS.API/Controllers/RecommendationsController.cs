using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController:ControllerBase
    {
        public string BaseUrl { get; set; }
        public new HttpWebRequest Request { get; set; }
        public WebResponse WebResponse { get; set; } 
        public RecommendationsController()
        {
            BaseUrl = "http://localhost:5005/api/v1.0/csharp_python_restfulapi_json";
            Request  = (HttpWebRequest)WebRequest.Create(BaseUrl);
            Request.Method = "POST";
        }
        [HttpGet]
        public bool GetResponse(){
            WebResponse = Request.GetResponse();
            return true;
            
        }
    }
}