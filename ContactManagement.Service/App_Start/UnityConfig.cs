using ContactManagement.Service.BLL.Services;
using ContactManagement.Service.BLL.Services.Impl;
using ContactManagement.Service.DLL.Models;
using ContactManagement.Service.DLL.Repository;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ContactManagement.Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<DbContext, ContactsManagementDbContext>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<IContactService, ContactService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}