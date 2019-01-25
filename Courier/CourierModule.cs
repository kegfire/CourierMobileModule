using Autofac;
using Courier.API;
using Courier.ViewMoodel;

namespace Courier
{
	public class CourierModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<StartPageViewModel>().AsSelf().SingleInstance();
			builder.RegisterType<CustomHttpClient>().As<ICustomHttpClient>().SingleInstance();
		}
	}
}