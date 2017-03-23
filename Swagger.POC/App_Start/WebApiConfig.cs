﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Swagger.POC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            SetFormatters(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public static void SetFormatters(HttpConfiguration config)
        {
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            if (config != null)
            {
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();

                config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

                config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                     Newtonsoft.Json.Formatting.Indented;

                config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                    (new Newtonsoft.Json.Converters.StringEnumConverter());
            }
        }
    }
}
