using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        //
        // GET: /Dinners/

        public ActionResult Index(int page=0)
        {
            const int pageSize = 10;
            var upcomingDinners = dinnerRepository.FindUpcomingDinners();

            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page, pageSize);

            return View("Index", paginatedDinners);
        }

        //
        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            else
                return View("Details", dinner);
        }

        //
        // GET: /Dinners/Edit/2
        public ActionResult Edit(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            var countries = new[]
                {
                    "USA",
                    "China",
                    "Russ"
                };

            ViewData["Country"] = new SelectList(countries, dinner.Country);


            return View(dinner);
        }

        //
        // POST: /Dinners/Edit/2
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            // Retrieve existing dinner
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (TryUpdateModel(dinner))
            {
                // Persist changes back to database
                dinnerRepository.Save();

                // Perform HTTP redirect to details page for the saved Dinner
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }

            var countries = new[]
                {
                    "USA",
                    "China",
                    "Russ"
                };

            ViewData["Country"] = new SelectList(countries, dinner.Country);
            return View(dinner);
        }

        //
        // GET: /Dinners/Create
        public ActionResult Create()
        {
            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };

            return View(dinner);
        }

        //
        // POST: /Dinners/Create
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinner.HostedBy = "SomeUser";
                dinnerRepository.Add(dinner);
                dinnerRepository.Save();

                return RedirectToAction("Details", new { id = dinner.DinnerID }); 
            }

            return View(dinner);
        }

        //
        // GET: /Dinners/Delete/1
        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        //
        // POST: /Dinners/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }

        [HttpPost]
        public ActionResult TotalDinners()
        {
            return Content(String.Format("共{0}笔数据", dinnerRepository.FindAllDinners().Count()));
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Test2()
        {
            return View();
        }
    }
}
