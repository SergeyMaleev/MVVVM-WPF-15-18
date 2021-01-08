
using BankSystem.Models;
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

namespace BankSystem.ViewModels.Client
{
    class CashAccountWindowViewModel : ViewModelsBase
    {

        BankSystemDB bankSystemDB = new BankSystemDB();
        public Models.Client CurrentClient { get; set; }

        private string _login;
        public virtual string Login
        { get => _login;


            set
            {
                Set(ref _login, value);
                Thread thread1 = new Thread(CurentClient); //Запускаем в фоновом потоке нахождение личного счета клиента
                thread1.Start();                           //Иначе приложение зависает на время обращения к БД
                thread1.IsBackground = true;

            }
        }



        private double _personalAccount;
        /// <summary>
        /// Лицевой счет клиента
        /// </summary>
        public double PersonalAccount
        {
            get => _personalAccount;
            set => Set(ref _personalAccount, value);
        }

        private bool _loadingStatus = true;
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


        private string _personalAccountAdd;
        /// <summary>
        /// Сумма к переводу или пополнению
        /// </summary>
        public string PersonalAccountAdd
        {
            get => _personalAccountAdd;
            set => Set(ref _personalAccountAdd, value);
        }


        private string _clientAccountTranslation;
        /// <summary>
        /// Логин клиента кому переводим денежные средства
        /// </summary>
        public string ClientAccountTranslation
        {
            get => _clientAccountTranslation;
            set => Set(ref _clientAccountTranslation, value);
        }

        public CashAccountWindowViewModel()
        {

            Messenger.Default.Register<string>(this, (message) => Login = message); //Получаем логин

            Thread thread = new Thread(ProgressBar); //Запускаем в фоновом потоке анимацию статуса загрузки
            thread.Start();
            thread.IsBackground = true;
           



        }

        #region Команды
        /// <summary>
        /// Команда добавления денежный средств на личный счет
        /// </summary>
        public ICommand MoneyAddCommand => new DelegateCommand(() =>
        {
            AddMoneyWindow add = new AddMoneyWindow();
            add.ShowDialog();

            if (add.DialogResult.Value)
            {
                PersonalAccount += add.MoneyAddClient;

                try
                {
                    CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == Login);
                    CurrentClient.PersonalAccount = PersonalAccount;
                    bankSystemDB.SaveChanges();

                }
                catch
                {
                    MessageBox.Show("Изменения не удалось сохранить в Базу данных");
                }
            }

        }, () => //Пока идет загрузка кнопка недоступна
        {
         if (!_loadingStatus) return true;
         return false;

        });

        /// <summary>
        /// Команда перевода денежных средств другому клиенту
        /// </summary>
        public ICommand TranslationMoneyCommand => new DelegateCommand(() =>
        {
            try
            {
                var cl = bankSystemDB.ClientsBank.Single(c => c.Login == ClientAccountTranslation);

                if (Convert.ToDouble(PersonalAccountAdd) <= PersonalAccount && Convert.ToDouble(PersonalAccountAdd) > 0)
                {
                    cl.PersonalAccount += Convert.ToDouble(PersonalAccountAdd);

                    CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == Login);

                    CurrentClient.PersonalAccount -= Convert.ToDouble(PersonalAccountAdd);
                    bankSystemDB.SaveChanges();

                    PersonalAccount -= Convert.ToDouble(PersonalAccountAdd);
                    OnPropertyChanged("PersonalAccount");

                    MessageBox.Show("Перевод успешно совершен");
                    ClientAccountTranslation = null;
                    PersonalAccountAdd = null;
                }
                else
                {
                    MessageBox.Show("Введена недопустимая сумма");
                    PersonalAccountAdd = null;
                }                   
            }
            catch (FormatException)
            {
                MessageBox.Show("Введены недопустимые символы");
                PersonalAccountAdd = null;
            }
            catch
            {
                MessageBox.Show("Клиента с таким логином не существует");
                ClientAccountTranslation = null;
            }


        }, () =>
        {
            if (!string.IsNullOrEmpty(ClientAccountTranslation)) return true; // Показываем когда введено поле логина
            return false;

        });

        #endregion
        
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

        /// <summary>
        ///метод полуения клиента
        /// </summary>
        private void CurentClient()
        {

            CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == Login);
            this.PersonalAccount = CurrentClient.PersonalAccount;           
            LoadingStatus = false;
            OnPropertyChanged("PersonalAccount");


        }

    }
}
