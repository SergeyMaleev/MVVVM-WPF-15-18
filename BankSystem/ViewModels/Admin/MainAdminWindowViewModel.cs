using BankSystem.View;
using BankSystem.View.Admin;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankSystem.ViewModels.Admin
{
    class MainAdminWindowViewModel : ViewModelsBase
    {

        private NavigationServiceAdmin _navigationAdmin;

        private Page _page;


        public Page CurrenPageAdmin
        {
            get => _page;
            set => Set(ref _page, value);
        }

        public MainAdminWindowViewModel(NavigationServiceAdmin navigation)
        {

            navigation.OnPageChanged += Page => CurrenPageAdmin = Page; //подписываемся на навигацию для пооказа текущей страницы
            navigation.Navigate(new ListClientWindow());
            _navigationAdmin = navigation;

        }

        public ICommand ListClientPageCommand => new DelegateCommand(() =>
        {
            _navigationAdmin.Navigate(new ListClientWindow());
        });

        public ICommand ListCreditPageCommand => new DelegateCommand(() =>
        {
            _navigationAdmin.Navigate(new ListCreditWindow());
        });

        public ICommand ListDepositPageCommand => new DelegateCommand(() =>
        {
            _navigationAdmin.Navigate(new ListDepositWindow());
        });

       
        public ICommand DouwnPageCommand => new DelegateCommand(() =>
        {

            _navigationAdmin.Navigate(new ExitWindow());
        });


    }
}
