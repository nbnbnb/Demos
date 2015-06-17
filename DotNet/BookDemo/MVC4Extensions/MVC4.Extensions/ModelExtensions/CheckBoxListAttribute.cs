﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4.Extensions.ModelExtensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckBoxListAttribute : ListAttribute
    {
        public CheckBoxListAttribute(string listName)
            : base(listName)
        {

        }

        public override void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);

            metadata.TemplateHint = "CheckBoxList";
        }
    }
}