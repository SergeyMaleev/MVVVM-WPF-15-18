using BankSystem.Models;
using BankSystem.View;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    class StartWindowViewModel : ViewModelsBase
    {
        private NavigationService _navigation;


        public Models.BankSystemDB BankSystemDB = new BankSystemDB();

        public StartWindowViewModel(NavigationService navigation)
        {

            _navigation = navigation;

          

        }


        /// <summary>
        /// Команда входа в ветвь клиента
        /// </summary>
        public ICommand LoginStaffCommand => new DelegateCommand(() =>
        {

            _navigation.Navigate(new LoginClientWindow()); //Переходим на страницу регистрации сотрудника

        });

        /// <summary>
        /// Команда входа в ветвь администратора
        /// </summary>
        public ICommand LoginAdminCommand => new DelegateCommand(() =>
        {

            _navigation.Navigate(new EnterAdminWindow()); //Переходим на страницу регистрации сотрудника

        });



    }
}
