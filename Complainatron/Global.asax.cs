using System.Web.Mvc;
using System.Web.Routing;
using Complainatron.Filters;
using System;
using Complainatron.Core.Services;
using Complainatron.Core.DTOs;
using StructureMap;

namespace Complainatron
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Complaint", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = (IContainer)IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));

            var logger = container.GetInstance<ILogService>();

            logger.Create(new Domain.Log() { DateCreated = DateTime.UtcNow, Id = Guid.NewGuid(), Message = "Starting ..." });
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            IoC.CommitAndDisposeHttpContextScopedObjects();
        }
    }
}