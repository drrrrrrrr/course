using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace courseWORK.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles ="user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
        public string AddStatLose(string login)
        {
            using (SudokuDBEntities1 db = new SudokuDBEntities1())
            {
                User k = db.User.Where(x => x.Email == login).FirstOrDefault();
                State state = k.State;
                state.GameCount++;
                db.SaveChanges();
            }
            return "ok";
        }
        public string AddStatWin(string login)
        {

            using (SudokuDBEntities1 db = new SudokuDBEntities1())
            {
                User k = db.User.Where(x => x.Email == login).FirstOrDefault();
                State state = k.State;
                state.Point++;
                state.GameCount++;
                db.SaveChanges();
            }
            return "ok";
        }



        public string Andex()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return result;
        }
        public JsonResult GetLogin()
        {

            string result = "ya";
            if (User.Identity.IsAuthenticated)
            {
                result = User.Identity.Name;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}