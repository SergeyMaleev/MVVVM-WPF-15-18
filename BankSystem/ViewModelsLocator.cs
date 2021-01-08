using BankSystem.View.Client;
using BankSystem.ViewModels;
using BankSystem.ViewModels.Admin;
using BankSystem.ViewModels.Base;
using BankSystem.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    /// <summary>
    /// Клас взаимодействия с XAML
    /// </summary>
    class ViewModelsLocator : ViewModelsBase
    {

       
        public MainWindowViewModel MainWindowViewModel => Dependency.Resolve<MainWindowViewModel>();
              
        public LoginClientWindowViewModel LoginClientWindowViewModel => Dependency.Resolve<LoginClientWindowViewModel>();

        public StartWindowViewModel StartWindowViewModel => Dependency.Resolve<StartWindowViewModel>();

        public EnterClientWindowViewModel EnterClientWindowViewModel => Dependency.Resolve<EnterClientWindowViewModel>();

        public EnterAdminWindowViewModel EnterAdminWindowViewModel => Dependency.Resolve<EnterAdminWindowViewModel>();

        public MainClientWimdowViewModel MainClientWimdowViewModel => Dependency.Resolve<MainClientWimdowViewModel>();

        public ClientCreditWindowViewModel ClientCreditWindowViewModel => Dependency.Resolve<ClientCreditWindowViewModel>();

        public ClientDepositWindowViewModel ClientDepositWindowViewModel => Dependency.Resolve<ClientDepositWindowViewModel>();

        public CashAccountWindowViewModel CashAccountWindowViewModel => Dependency.Resolve<CashAccountWindowViewModel>();

        public MainAdminWindowViewModel MainAdminWindowViewModel => Dependency.Resolve<MainAdminWindowViewModel>();

        public ListClientWindowViewModel ListClientWindowViewModel => Dependency.Resolve<ListClientWindowViewModel>();

        public ListCreditWindowViewModel ListCreditWindowViewModel => Dependency.Resolve<ListCreditWindowViewModel>();

        public ListDepositWindowViewModel ListDepositWindowViewModel => Dependency.Resolve<ListDepositWindowViewModel>();

        public ExitWindowViewModel ExitWindowViewModel => Dependency.Resolve<ExitWindowViewModel>(); //Промежуточная модель представления для выхлжа в главное меню



    } 
}
