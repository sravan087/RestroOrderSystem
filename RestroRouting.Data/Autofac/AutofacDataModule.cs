using Autofac;
using Microsoft.EntityFrameworkCore;
using RestroRouting.Data.BoothRepository;
using RestroRouting.Data.BoothRepository.Interfaces;
using RestroRouting.Data.Infrastructure;
using RestroRouting.Data.Infrastructure.Interfaces;

namespace RestroRouting.Data.Autofac
{
    public class AutofacDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var dbContextoptions = new DbContextOptionsBuilder<RestroRoutingContext>()
                .UseInMemoryDatabase("RestoRoutingDB");


            builder.Register(c => { return new RestroRoutingContext(dbContextoptions.Options); })
                   .AsSelf()
                   .As<RestroRoutingContext>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<BoothData>()
                .As<IBoothData>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitofWork>()
                .As<IUnitofWork>()
                .InstancePerLifetimeScope();                


            base.Load(builder);
        }
    }
}
