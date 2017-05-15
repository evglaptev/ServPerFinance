using perFinance.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace perFinance.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserInfoController : ApiController
    {
        Server serv = Server.Init();
        public UserInfoController()
        {


            //HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }




        public UserInfo Get(string userName)
        {
            return this.serv.getUserInfoByName(userName);
           // return new UserInfo() { name = userName, accountNumber = "234234", balance = 23424 };
        }

    }

    public class UserInfo
    {
        public string name { get; set; }
        public string accountNumber { get; set; }
        public double balance { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
    }

}
