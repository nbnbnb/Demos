using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap4.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ListAttribute : Attribute, IMetadataAware
    {
        public string ListName { get; private set; }

        public ListAttribute(string listName)
        {
            this.ListName = listName;
        }

        public virtual void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("ListName", this.ListName);
        }
    }
}