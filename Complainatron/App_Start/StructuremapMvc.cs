using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Complainatron.App_Start.StructuremapMvc), "Start")]

namespace Complainatron.App_Start 
{
    public static class StructuremapMvc 
    {
        public static void Start() 
        {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}