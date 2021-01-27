using Lib.Data.Repository.Marketings;
using Lib.Data.Repository.Packages;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.TreeDatas;
using Lib.Data.Repository.User;
using Lib.Service.Service.CoinBase;
using Lib.Service.Service.Marketings;
using Lib.Service.Service.Packages;
using Lib.Service.Service.TreeDatas;
using Lib.Service.Service.Trons;
using Lib.Service.Service.User;
using Lib.Service.Service.Wallet;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using Web.SourceCoin.Controllers;

namespace Web.SourceCoin
{
    public static class IocConfigurator
    {
        public static void ConfiguratorIocUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            RegisterRepository(container);
            RegisterServices(container);

            DependencyResolver.SetResolver(new UnitDependencyResolver(container));

            container.RegisterType<WebapiController>();
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(container);
        }

        private static void RegisterRepository(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ITreeRepository, TreeRepository>();
            container.RegisterType<IMarketingRepository, MarketingRepository>();
            container.RegisterType<IPackagesRepository, PackagesRepository>();
            container.RegisterType<IWalletRepository, WalletRepository>();
            container.RegisterType<IDepositRepository, DepositRepository>();
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICoinService, CoinService>();
            container.RegisterType<ITreeService, TreeService>();
            container.RegisterType<IMarketingService, MarketingService>();
            container.RegisterType<IPackagesService, PackagesService>();
            container.RegisterType<IWalletService, WalletService>();
            container.RegisterType<IDepositService, DepositService>();
            //container.RegisterType<ITronService, TronService>();
        }
    }
}