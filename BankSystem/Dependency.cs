using BankSystem.View.Client;
using BankSystem.ViewModels;
using BankSystem.ViewModels.Admin;
using BankSystem.ViewModels.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public static class Dependency
    {

        private static readonly ServiceProvider _provider;
        static Dependency()
        {

            var services = new ServiceCollection();

            services.AddSingleton<NavigationService>(); //основная навигация
           
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<StartWindowViewModel>();

            services.AddTransient<LoginClientWindowViewModel>();

            services.AddTransient<EnterClientWindowViewModel>();

            services.AddTransient<EnterAdminWindowViewModel>();

            services.AddSingleton<ExitWindowViewModel>();
            
            //Клиенские страницы           
            services.AddSingleton<NavigationServiceClient>(); //Навигация в кабинете клиента

            services.AddTransient<MainClientWimdowViewModel>();

            services.AddTransient<ClientCreditWindowViewModel>();

            services.AddTransient<ClientDepositWindowViewModel>();

            services.AddTransient<CashAccountWindowViewModel>();

            //Администраторские страницы           
            services.AddSingleton<NavigationServiceAdmin>(); //Навигация в кабинете администраторов

            services.AddTransient<MainAdminWindowViewModel>();

            services.AddTransient<ListClientWindowViewModel>();

            services.AddTransient<ListCreditWindowViewModel>();

            services.AddTransient<ListDepositWindowViewModel>();

            _provider = services.BuildServiceProvider();


        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
