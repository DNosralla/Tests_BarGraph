using Autofac;
using Autofac.Integration.WebApi;
using BarGraph.Infrastructure;
using BarGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BarGraph
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<GraphDataService>().As<IGraphDataService>().SingleInstance();
            builder.RegisterType<GraphFileDataReader>().As<IGraphFileDataReader>().SingleInstance();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}