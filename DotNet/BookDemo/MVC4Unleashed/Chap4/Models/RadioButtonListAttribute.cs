﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap4.Models
{
     [AttributeUsage(AttributeTargets.Property)]
    public class RadioButtonListAttribute:ListBoxAttribute
    {
         public RadioButtonListAttribute(string listName)
             :base(listName)
         {

         }

         public override void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
         {
             base.OnMetadataCreated(metadata);
             metadata.TemplateHint = "RadioButtonList";
         }
    }
}