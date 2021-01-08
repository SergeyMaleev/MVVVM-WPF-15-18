using BankSystem.View;
using BankSystem.View.Client;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    class ExitWindowViewModel
    {
        private NavigationService _navigation;
        public ExitWindowViewModel(NavigationService navigation)
        {

            _navigation = navigation;


        }

        /// <summary>
        /// Команда выхода на главную страницу
        /// </summary>
        public ICommand ExitPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new StartWindow());
        });


        /// <summary>
        /// Команда отмены выхода 
        /// </summary>
        public ICommand CancelPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new MainClientWimdow());
        });

    }
}
