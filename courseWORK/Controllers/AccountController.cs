using courseWORK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace courseWORK.Controllers
{
    public class AccountController : Controller
    {
        SudokuDBEntities1 db = new SudokuDBEntities1();
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = db.User.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);

                if (user == null)
                {
                    User regUser = new User { Email = model.Name, Password = model.Password, RoleId = 2};
                    State st = new State();
                    st.Id = db.State.Count();
                    st.Point = 0;
                    st.GameCount = 0;
                    db.State.Add(st);
                    db.User.Add(new User { Email = model.Name, Password = model.Password, RoleId = 2 ,StateId=st.Id});
                    db.SaveChanges();
                    user = db.User.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserInfo(string login)
        {
            using (SudokuDBEntities1 db = new SudokuDBEntities1())
            {
                User k = db.User.Where(x => x.Email == login).FirstOrDefault();
                State m = db.User.Where(x => x.Email == login).FirstOrDefault().State;
                UserState us = new UserState(k.Email, m.GameCount.ToString(), m.Point.ToString());
                return View(us);
            }
        }

    }
}
