﻿using perFinance.Generate;
using perFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace perFinance.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class DataController : ApiController
    {
        
        public IList<operationItem> Get(string userName)
        {
            var serv = Server.Init();
            var str = serv.getItemEnumerableForName(userName);
            
            return str;
        }
    }
}
