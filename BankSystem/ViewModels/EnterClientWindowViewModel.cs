using BankSystem.Models;
using BankSystem.View;
using BankSystem.View.Client;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    class EnterClientWindowViewModel : ViewModelsBase
    {
        BankSystemDB bankSystemDB = new BankSystemDB();

        #region Поля вводимые клиентом
        private string _login;
        /// <summary>
        /// Логин клиента (представителя юр.лица)
        /// </summary>
        public string Login
        {
            get => _login;
            set
            {
                Set(ref _login, value);
                //LoginClientUnique();
            }
        }


        private string _password;
        /// <summary>
        /// Пароль клиента (представителя юр.лица)
        /// </summary>
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        private bool _loadingStatus = false;
        /// <summary>
        /// Статус показа полосы загрузки
        /// </summary>
        public bool LoadingStatus
        {
            get => _loadingStatus;
            set => Set(ref _loadingStatus, value);
        }



        private int _currentProgress = 0;
        /// <summary>
        /// Служит для анимации загрузочной строки
        /// </summary>
        public int CurrentProgress
        {
            get => _currentProgress;
            set => Set(ref _currentProgress, value);

        }

        #endregion

        private NavigationService _navigation;
       
        public EnterClientWindowViewModel(NavigationService navigation)
        {
            _navigation = navigation;
          
        }


        public ICommand DouwnPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new LoginClientWindow());
        });


        public ICommand ClientPageCommand => new DelegateCommand(() =>
        {
            //Запускаем 2 потока 
            var taskProgressBar = new Task(ProgressBar);
            taskProgressBar.Start();

            var taskClientEnter = new Task(ClientEnter);
            taskClientEnter.Start();

            LoadingStatus = true;
            OnPropertyChanged("LoadingStatus");
                                     
        });


        /// <summary>
        /// Метод перехода в главную страницу клиента
        /// </summary>
        private void BankSystemDB_ProgressChanged()
        {
           
            Application.Current.Dispatcher.Invoke(new Action(() => { _navigation.Navigate(new MainClientWimdow()); })); //выполняем в основном потоке
            Messenger.Default.Send(Login); //передаем логин подписаным viewModel
        }

        /// <summary>
        /// Метод поиска клиента в базе данных
        /// </summary>
        private void ClientEnter()
        {
           
            BankSystemDB bankSystemDB = new BankSystemDB();
            Thread.Sleep(2000); //задержка для отображения статуса загрузки
            var status = bankSystemDB.ClientUnique(Login, Password);          
            if (status)
            {
                BankSystemDB_ProgressChanged();
            }
            else
            {
                LoadingStatus = false;
                OnPropertyChanged("LoadingStatus");
            }

        }

        /// <summary>
        /// вспомогательный метод который меняет прогресс бар 
        /// </summary>        
        private void ProgressBar()
        {

            while (_loadingStatus)
            {

                for (int i = _currentProgress; i < 100; i++)
                {
                    _currentProgress++;
                    Thread.Sleep(10);
                    OnPropertyChanged("CurrentProgress");
                }

                for (int i = _currentProgress; i > 0; i--)
                {
                    _currentProgress--;
                    Thread.Sleep(10);
                    OnPropertyChanged("CurrentProgress");
                }

            }
        }

    }
}
