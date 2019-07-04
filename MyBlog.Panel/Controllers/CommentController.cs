using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            using (CommentService commentService = new CommentService())
            {
                var model = commentService.GetComments(id);

                return View(model);
            }         
        }

        
        public ActionResult Confirmed(int id)
        {
            using (CommentService commentService = new CommentService())
            {
                var result = commentService.GetConfirmed(id);
                return RedirectToAction("Index","Blog");
            }
        }
    }
}