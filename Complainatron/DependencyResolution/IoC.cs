using StructureMap;
using System.Web.Mvc;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Services;
using Complainatron.Core.DTOs;

namespace Complainatron
{
    public static class IoC
    {
        public static void CommitAndDisposeHttpContextScopedObjects()
        {
            var context = ObjectFactory.Container.TryGetInstance<IDbContext>();

            if (context != null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.For<IFilterProvider>().Use<SmDependencyResolver>();

                            x.For<IDbContext>().HttpContextScoped().
                                Use<Complainatron.DataAccess.EntityFramework.ComplainatronDataContext>().Ctor<string>("nameOrConnectionString").
                                EqualToAppSetting("DataContextConnectionStringName");

                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.Assembly("Complainatron.Core");
                                        scan.Assembly("Complainatron.Services");
                                        scan.Assembly("Complainatron.DataAccess.EntityFramework");
                                        scan.ConnectImplementationsToTypesClosing(typeof(Complainatron.Core.Services.IService<>));
                                        scan.ConnectImplementationsToTypesClosing(typeof(Complainatron.Core.DataAccess.IDbContextSet<>));
                                        scan.AddAllTypesOf(typeof(Complainatron.Core.DataAccess.IDbPropertyValues));
                                        scan.WithDefaultConventions();
                                    });

                            x.SetAllProperties(p => p.OfType<Complainatron.Core.Services.IFacebookService>());
                            x.SetAllProperties(p => p.OfType<Complainatron.Core.Services.IUserService>());
                        });
            return ObjectFactory.Container;
        }
    }
}