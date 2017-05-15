using perFinance.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace perFinance.Controllers
{
   public class Auth
    {
        public Auth(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public string name;
        public string password;
    }


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        Auth[] listUsers;
        Server serv = Server.Init();
        public AuthController()
        {
          //  System.Web.HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            
        }
        [HttpPost]
        public bool Post([FromBody]Auth value)
        {
           return this.serv.isLogin(value.name, value.password);

        }

        public string Get()
        {
            return "sdfs";
        }

        
    }
}
