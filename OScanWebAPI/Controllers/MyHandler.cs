using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using OScanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OScanWebAPI.Controllers
{
    public class MyHandler : WebHookHandler
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            string action = context.Actions.First();
            JObject data = context.GetDataOrDefault<JObject>();

            db.Assinatura.Add(new Assinatura
            {
                Nome = data.GetValue("issue").ToString()
            });

            return Task.FromResult(true);
        }
    }
}