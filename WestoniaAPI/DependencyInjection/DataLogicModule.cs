using Autofac;
using WestoniaAPI.Core;
using WestoniaAPI.DataLogic;

namespace WestoniaAPI.DependencyInjection
{
    public class DataLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<UserLogic>().As<IUserLogic>();
        }
    }
}
