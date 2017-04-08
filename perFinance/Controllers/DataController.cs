using perFinance.Generate;
using perFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace perFinance.Controllers
{
    public class DataController : ApiController
    {
        
        public IList<operationItem> Get(string userName)
        {
            var serv = new Server();
            var str = serv.getItemEnumerableForName(userName);
            return str;
        }
    }
}
