using perFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using perFinance.Constants;
using System.Diagnostics;
using System.Net;
using perFinance.Controllers;
using System.IO;

namespace perFinance.Generate
{


    public class Server
    {
        private static Server serv;
        private  IDictionary<string,IList<operationItem>> operationsDic;
        private string[] names;
        private IDictionary<string, string> loginInfo;

        internal bool accNumberIsContains(string accNumber)
        {
            return this.accNumberBalance.ContainsKey(accNumber);
        }

        private IDictionary<string, double> accNumberBalance;

        void log(string str)
        {/*
            string writePath = "E://log.txt";
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {

                sw.WriteLine(str);
            }
            */

        }
        internal bool doTransfer(TransferInfo transferInfo)
        {
           
                log("***doTransfer***");

                string fromNumber= transferInfo.from;
                double balanceFrom;
                string toNumber = transferInfo.to;
                double value = transferInfo.balance;
                log("to: " + toNumber + "   from: " + fromNumber + "   value: " + value);
            if (!this.accNumberBalance.ContainsKey(fromNumber)) return false;
            if (!this.accNumberBalance.ContainsKey(toNumber)) return false;
            if (!this.accNumberBalance.TryGetValue(fromNumber, out balanceFrom)) return false;
                log("from number out balance is ok" + balanceFrom);
                if (!(balanceFrom > value && value > 0)) return false;
                log("balance control is passed");
                this.accNumberBalance[fromNumber] = balanceFrom - value;
                this.accNumberBalance[toNumber] += value;
                return true;
            
        }

        private IDictionary<string, string> accNumberName;
        private IDictionary<string, string> nameAccNumber;

        internal Controllers.UserInfo getUserInfoByName(string userName)
        {
            string accNumber;
            double accBalance;
            if (!this.nameAccNumber.TryGetValue(userName, out accNumber)) return null;
            if (!this.accNumberBalance.TryGetValue(accNumber, out accBalance)) return null;
            return new Controllers.UserInfo { name = userName, accountNumber = accNumber, balance = accBalance };
        }

        public static Server Init()
        {
            if (Server.serv == null)
            {
                Server.serv = new Server();
            }
            return Server.serv;
        }
        private Server()
        {
            this.operationsDic = new Dictionary<string, IList<operationItem>>();
            this.names = new string[6] { "Вася", "Игорь", "Костя", "Галина", "Евгений","Алексей" };
          this.accNumberBalance = new Dictionary<string, double>();
            this.accNumberName = new Dictionary<string, string>();
         this.nameAccNumber = new Dictionary<string, string>();
            this.loginInfo = new Dictionary<string, string>();
            Random rand = new Random();
            foreach(var item in this.names)
            {
                this.loginInfo.Add(item, item);
                this.operationsDic.Add(item, this.generateForName(item));
                this.nameAccNumber.Add(item, rand.Next(100000001, 999999999).ToString());
            }
            foreach(var item in this.nameAccNumber)
            {
                this.accNumberName.Add(item.Value, item.Key);
                this.accNumberBalance.Add(item.Value, rand.Next(10000, 300000));
            }
        }   
        public bool isLogin(string name, string password)
        {
            string realPass;
            if (this.loginInfo.TryGetValue(name, out realPass))
                return realPass == password;
            return false;
        }
        public IList<operationItem> getItemEnumerableForName(string name)
        {
            IList<operationItem> result;
             this.operationsDic.TryGetValue(name, out result);
            return result;
        }

        private IList<operationItem> generateForName(string name)
            {


            IList<operationItem> result = new List<operationItem>();
            var gen = new Generate();
            bool isCurrentDay = false;
            for (int i = 0; i < 520; i++)
            {
                DateTime t;
                if (isCurrentDay) {
                    t = gen.Time(isCurrentDay);
                        }
                else
                {
                    t = gen.Time(isCurrentDay);
                    isCurrentDay = true;
                }
                var item = new operationItem
                {
                    userName = name,
                    place = gen.Place(),
                    type = gen.Type(),
                    price = gen.Price(),
                    time = t
                };

                result.Add(item);

            }
            // HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return result.OrderByDescending(item => item.time).ToList<operationItem>();// System.Web.Helpers.Json.Encode(result);
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

        public DateTime Time(bool isCurrentDay)
        {
            var newDate = DateTime.MaxValue;
            while (newDate > DateTime.Now)
            {
                try
                {
                    newDate = new DateTime(rand.Next(2017, 2018), rand.Next(1, 12), rand.Next(1, 31), rand.Next(0, 23), rand.Next(0, 60), rand.Next(0, 60),7);  // что с 31 февряля ?
                }
                catch
                {
                    newDate = DateTime.MaxValue;
                }
            }
            if (isCurrentDay)
            {
                return newDate;
            }
            else
            {
                isCurrentDay = true;
                return new DateTime(2017,4,11);
            }
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