using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using Enteties;

namespace WebService.Controllers
{
    public class MessageController : ApiController
    {
        DataLayer d = new DataLayer();

        [HttpGet]
        public IEnumerable<MessageEntities> GetMessages(string user)
        {
            return d.GetLast5Messeges(user);
        }
    }
}
