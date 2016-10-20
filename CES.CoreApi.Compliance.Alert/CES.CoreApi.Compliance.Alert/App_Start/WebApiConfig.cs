﻿using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CES.CoreApi.Compliance.Alert
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "alert/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			FluentValidationModelValidatorProvider.Configure(config);
		}
    }
}