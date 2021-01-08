using BankSystem.View;
using BankSystem.View.Admin;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    class EnterAdminWindowViewModel : ViewModelsBase
    {
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


        private NavigationService _navigation;
        public EnterAdminWindowViewModel(NavigationService navigation)
        {
            _navigation = navigation;

        }


        public ICommand DouwnPageCommand => new DelegateCommand(() =>
        {
            //Запускаем 2 потока 
            var taskProgressBar = new Task(ProgressBar);
            taskProgressBar.Start();
         
            LoadingStatus = true;
            OnPropertyChanged("LoadingStatus");


            _navigation.Navigate(new StartWindow());
        });

        public ICommand AdminPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new MainAdminWindow());
        });

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
