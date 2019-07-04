using MyBlog.DTO;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Panel.Controllers
{
    public class UserController : Controller
    {
        //List: FullName, CityName, TownName, EmailAddress,UserTypeName, Detay
        //Save
        //IsEmailVerified=true
        //UserTypeID=2
        public ActionResult Index()
        {
            using (UserService service = new UserService())
            {
                var result = service.List();

                return View(result);
            }

        }

        [HttpGet]
        public ActionResult Save()
        {
            using (DefinitionService service = new DefinitionService())
            {
                var result = service.GetCities();
                return View(result);
            }

        }

        [HttpGet]
        public JsonResult GetTowns(int id)
        {
            using (DefinitionService service = new DefinitionService())
            {
                var result = service.GetTownsByCity(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Save(UserDTO obj)
        {

            using (UserService service = new UserService())
            {
                obj.IsEmailVerified = true;
                obj.UserTypeID = 2;
                var result = service.Register(obj);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hatalı işlem yaptınız lütfen tekrar deneyin";
                    return RedirectToAction("Save");
                }
            }
        }

        public ActionResult Get(int id)
        {
            using (UserService service = new UserService())
            {
                var result = service.Get(id);

                return View(result);
            }
        }


        public ActionResult Edit(int id)
        {
            using (DefinitionService service = new DefinitionService())
            {
                ViewBag.RecordStasuses = service.GetRecordStatuses();
            }

            using (UserService service = new UserService())
            {
                var result = service.Get(id);

                return View(result);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserDTO obj)
        {
            using (UserService service = new UserService())
            {
                var result = service.Update(obj);
                if (result)
                {

                    return RedirectToAction("Get", new { id = obj.UserId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Bir hata oluştu.";
                    return View();
                }
            }
        }
    }
}