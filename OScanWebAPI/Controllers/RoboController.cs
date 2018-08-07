using OScanWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OScanWebAPI.Controllers
{
    public class RoboController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        [Route("api/Robo/Teste/{issue}")]
        public void Teste(string issue)
        {
            db.Assinatura.Add(new Assinatura()
            {
                Nome = issue
            });
        }
    }
}
