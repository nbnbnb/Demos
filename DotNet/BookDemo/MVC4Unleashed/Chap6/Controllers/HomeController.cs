using Chap6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap6.Controllers
{
    [ValidationRule("Rule3")]
    public class HomeController : RuleBasedController
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            IActionInvoker actionInvoker = base.CreateActionInvoker();
            if (actionInvoker is ControllerActionInvoker)
            {
                return new ParameterValidationActionInvoker();
            }
            else
            {
                return new ParameterValidationAsyncActionInvoker();
            }
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View("person2");
        }

        [HttpPost]
        public ActionResult Index(Person person)
        {
            return View("person", person);
        }

        [ValidationRule("Rule1")]
        public ActionResult Rule1()
        {
            return View("person", new Person());
        }

        [ValidationRule("Rule1")]
        [HttpPost]
        public ActionResult Rule1(Person person)
        {
            return View("person", person);
        }

        [ValidationRule("Rule2")]
        public ActionResult Rule2()
        {
            return View("person", new Person());
        }

        [ValidationRule("Rule2")]
        [HttpPost]
        public ActionResult Rule2(Person person)
        {
            return View("person", person);
        }

        public ActionResult Add(
            [Display(Name = "第一个操作数")]
            [Range(10, 20, ErrorMessage = "{0}必须在{1}和{2}之间!")]
            [ModelBinder(typeof(ParameterValidationModelBinder))]
            int operand1,
            [Display(Name = "第二个操作数")]
            [Range(10, 20, ErrorMessage = "{0}必须在{1}和{2}之间!")]
            [ModelBinder(typeof(ParameterValidationModelBinder))]
            int operand2
            )
        {
            double result = 0.00;
            if (ModelState.IsValid)
            {
                result = operand1 + operand2;
            }

            return View(new OperationData
            {
                Operand1 = operand1,
                Operand2 = operand2,
                Operator = "Add",
                Result = result
            });
        }

    }
}
