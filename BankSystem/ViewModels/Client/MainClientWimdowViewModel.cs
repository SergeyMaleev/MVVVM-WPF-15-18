using BankSystem.Models;
using BankSystem.View;
using BankSystem.View.Client;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankSystem.ViewModels.Client
{
    class MainClientWimdowViewModel : ViewModelsBase
    {
       
        private string _login; 
        public virtual string Login { get => _login;  set => Set(ref _login, value); }

        protected static string CurrentLogin { get; set; }

        private NavigationServiceClient _navigationClient;

        private Page _page;

      
        public Page CurrenPageClient
        {
            get => _page;
            set => Set(ref _page, value);
        }

        public MainClientWimdowViewModel(NavigationServiceClient navigation)
        {

            navigation.OnPageChanged += Page => CurrenPageClient = Page; //подписываемся на навигацию для показа текущей страницы
            navigation.Navigate(new CashAccountWindow());
           
            _navigationClient = navigation;
           
            Messenger.Default.Register<string>(this, (message) => Login = message);

        }

        public MainClientWimdowViewModel()
        { }

        public ICommand CashAccountViewModelPageCommand => new DelegateCommand(() =>
        {           
            _navigationClient.Navigate(new CashAccountWindow());
            Messenger.Default.Send(Login);
        });

        public ICommand CreditPageCommand => new DelegateCommand(() =>
        {
            _navigationClient.Navigate(new ClientCreditWindow());
            Messenger.Default.Send(Login);
        });

        public ICommand DepositPageCommand => new DelegateCommand(() =>
        {
            _navigationClient.Navigate(new ClientDepositWindow());
            Messenger.Default.Send(Login);
        });

        


        public ICommand DouwnPageCommand => new DelegateCommand(() =>
        {

            _navigationClient.Navigate(new ExitWindow());
        });
        
    }
}
