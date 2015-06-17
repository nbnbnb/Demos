using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BasicMvcApp.Controllers
{
    public class SimpleController : Controller
    {
        //
        // GET: /Simple/

        public ViewResult Index()
        {
            return View("MyView");
        }

        public ViewResult ShowAge(DateTime birthDate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if ((now.Month * 100 + now.Day) < (birthDate.Month * 100 + birthDate.Day))
            {
                age -= 1;  // Haven't had birthday yet this year
            }

            ViewBag.age = age;

            return View();
        }

        public RedirectToRouteResult RegisterForUpdates(string emailAddress)
        {
            if (!IsValidEmail(emailAddress))
            {
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("RegisterationCompleted");
            }
        }

        private bool IsValidEmail(string emailAddress)
        {
            return Regex.Match(emailAddress,
                @"^(\w)+(\.\w+)*@(\w)+((\.\w{2,3}){1,3})$").Success;
        }

        public ViewResult HomePage()
        {
            if (IncomingHasVisitedBeforeCookie == null)
            {
                ViewData["IsFirstVisit"] = true;
                // Set the cookie so we'll remember the visitor next time
                OutgoingHasVisitedBeforeCookie = new HttpCookie("HasVisitedBefore", bool.TrueString);
            }
            else
            {
                ViewData["IsFirstVisit"] = false;
            }

            return View();
        }

        public virtual HttpCookie IncomingHasVisitedBeforeCookie
        {
            get
            {
                return Request.Cookies["HasVisitedBefore"];
            }
        }

        public virtual HttpCookie OutgoingHasVisitedBeforeCookie
        {
            get
            {
                return Response.Cookies["HasVisitedBefore"];
            }
            set
            {
                Response.Cookies.Remove("HasVisitedBefore");
                Response.Cookies.Add(value);
            }
        }
    }
}
