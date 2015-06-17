using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
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
            // 将数据源中的键存储在 ModelMetadata 附加的数据中
            metadata.AdditionalValues.Add("ListName", this.ListName);
        }
    }
}