using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceHolder.API.Version
{
    internal class VersionByNamespaceConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var hasAttributeRouteModels = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);

                if (!hasAttributeRouteModels)
                {
                    var apiVersion = controller.Attributes.FirstOrDefault(t => t is ApiVersionAttribute) as ApiVersionAttribute;

                    if (apiVersion == null)
                    {
                        throw new ArgumentNullException(nameof(AreaAttribute));
                    }

                    controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                    {
                        Template = $"api/v{apiVersion.RouteValue}"
                    };
                }
            }
        }
    }
}
