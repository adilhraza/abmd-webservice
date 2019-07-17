using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using XmlWebService.Services;
using XmlWebService.Services.Impl;

namespace XmlWebservice.Infrastructure
{
    public class DiConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<XmlParser>()
                    .As<IXmlParser>()
                    .InstancePerRequest();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}