using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap4.Models
{
    public class ExtendedDataAnnotationsProviders :
        CachedDataAnnotationsModelMetadataProvider
    {
        protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes,
            Type containerType, Type modelType, string propertyName)
        {
            CachedDataAnnotationsModelMetadata modelMetadata =
                base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);

            DisplayTextAttribute displayTextAttribute =
                attributes.OfType<DisplayTextAttribute>().FirstOrDefault();

            if (null != displayTextAttribute)
            {
                displayTextAttribute.SetDisplayName(modelMetadata);
            }

            return modelMetadata;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype,
            Func<object> modelAccessor)
        {
            CachedDataAnnotationsModelMetadata modelMetadata =
                base.CreateMetadataFromPrototype(prototype, modelAccessor);

            modelMetadata.DisplayName = prototype.DisplayName;

            return modelMetadata;
        }
    }
}