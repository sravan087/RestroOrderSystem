using Autofac;
using RestroRouting.Domain;
using RestroRouting.Service.Logic;
using RestroRouting.Service.Logic.Interfaces;

namespace RestroRouting.Service.Autofac
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DesertAreaProcesser>()
                   .As<IMenuItemProcesser>()
                   .Keyed<IMenuItemProcesser>(MenuItemType.Desert)
                   //.WithParameter()
                   .InstancePerLifetimeScope();


            builder.RegisterType<DrinkAreaProcesser>()
                   .As<IMenuItemProcesser>()
                   .Keyed<IMenuItemProcesser>(MenuItemType.Drink)
                   .InstancePerLifetimeScope();


            builder.RegisterType<FriesAreaProcesser>()
                   .As<IMenuItemProcesser>()
                   .Keyed<IMenuItemProcesser>(MenuItemType.Fries)
                   .InstancePerLifetimeScope();



            builder.RegisterType<GrillAreaProcesser>()
                   .As<IMenuItemProcesser>()
                   .Keyed<IMenuItemProcesser>(MenuItemType.Grill)
                   .InstancePerLifetimeScope();



            builder.RegisterType<SaladAreaProcesser>()
                   .As<IMenuItemProcesser>()
                   .Keyed<IMenuItemProcesser>(MenuItemType.Salad)
                   .InstancePerLifetimeScope();


            builder.RegisterType<BoothService>()
                   .As<IBoothService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<MenuItemFactoryProcesser>()
                  .As<IMenuItemFactoryProcesser>()
                  .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
