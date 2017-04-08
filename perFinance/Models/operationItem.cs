using perFinance.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace perFinance.Models
{
    public class operationItem
    {
        public string userName { get; set; }
        public string place { get; set; }
        public Category type { get; set; }
        public int price { get; set; }
        public DateTime time { get; set; }
    }
}