using MvcAppModelValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelValidation.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            ModelMetadata salaryMetadata = ModelMetadataProviders.Current
                .GetMetadataForProperty(() => new Employee(), typeof(Employee), "Salary");

            IEnumerable<ModelValidator> validators = salaryMetadata.GetValidators(base.ControllerContext);

            return View(validators.ToArray());
        }

    }
}
