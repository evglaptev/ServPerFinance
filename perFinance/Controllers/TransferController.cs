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
    public class TransferController : ApiController
    {

        Server serv = Server.Init();

        public bool? Post([FromBody] TransferInfo transferInfo)
        {
            if (transferInfo == null) return null;
           return this.serv.doTransfer(transferInfo);
        }

    }

    public class TransferInfo
    {
        public string from { get; set; }
        public string to { get; set; }
        public double balance { get; set; }
    }
    

}
