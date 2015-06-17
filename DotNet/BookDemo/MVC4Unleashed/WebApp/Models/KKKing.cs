using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class KKKing : ModelValidator
    {
        public KKKing(ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {

        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return new List<ModelValidationResult>();
        }
    }
}