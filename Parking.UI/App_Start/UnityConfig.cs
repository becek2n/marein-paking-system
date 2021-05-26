using AutoMapper;
using Parking.DAL;
using Parking.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Parking.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IParking, ParkingDAL>();
            container.RegisterType<ITransportation, TransportationDAL>();
            //automammper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            container.RegisterInstance(mapper);


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}