using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyBlog.Api.Controllers
{
    public class ContactController : ApiController
    {
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Save(MessageDTO obj)
        {
            using (MessageService messageService = new MessageService())
            {
                var result = messageService.SendMessage(obj);

                if (result)
                {
                    return Content(HttpStatusCode.OK, true);
                }
                else
                {
                    return InternalServerError();
                }
            }
        }
    }
}
