using perFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using perFinance.Constants;
using System.Diagnostics;
using System.Net;

namespace perFinance.Generate
{


    public class Server
    {
        public IList<operationItem> getItemEnumerableForName(string name)
        {
            IList<operationItem> result = new List<operationItem>();
            var gen = new Generate();
            for (int i = 0; i < 500; i++)
            {
                var item = new operationItem
                {
                    userName = name,
                    place = gen.Place(),
                    type = gen.Type(),
                    price = gen.Price(),
                    time = gen.Time()
                };

                result.Add(item);

            }
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return result;// System.Web.Helpers.Json.Encode(result);
        }
    }
    public class Generate
    {


        Random rand;
        public Generate() {
            rand = new Random();
        }

        public int Price()
        {
            return rand.Next(10, 5000);
        }

        public DateTime Time()
        {
            var newDate = DateTime.MaxValue;
            while (newDate > DateTime.Now)
            {
                try
                {
                    newDate = new DateTime(rand.Next(2015, 2017), rand.Next(1, 12), rand.Next(1, 31), rand.Next(0, 23), rand.Next(0, 60), rand.Next(0, 60),7);  // что с 31 февряля ?
                }
                catch
                {
                    newDate = DateTime.MaxValue;
                }
            }
            return newDate;
                }

        public Category Type()
        {
            return (Category)rand.Next(0, 3);
        }

        public string Place()
        {
            return "место";
        }

        public string Name()
        {
            return "Вася";
        }
    }


}