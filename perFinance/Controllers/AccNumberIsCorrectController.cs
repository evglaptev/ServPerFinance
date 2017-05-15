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

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class AccNumberIsCorrectController : ApiController
    {
        Server serv = Server.Init(); 
        public bool get(string accNumber)
        {
          return  this.serv.accNumberIsContains(accNumber);
        }

    }
}
