using BankSystem.Models;
using BankSystem.Models.Status;
using BankSystem.View;
using BankSystem.View.Client;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BankSystem.ViewModels
{
    /// <summary>
    /// Модель представления регистрации клиента
    /// </summary>
    public class LoginClientWindowViewModel : ViewModelsBase
    {

        #region Поля вводимые пользователем

        private bool _checkResult = false;
        /// <summary>
        /// Чекбокс физ. или юр. лицо
        /// </summary>
        public bool CheckResult
        {
            get => _checkResult;
            set => Set(ref _checkResult, value);

        }



        private bool _loginStatus = false;
        /// <summary>
        /// Статус Уникальности логина
        /// </summary>
        public bool LoginStatus
        {
            get => _loginStatus;
            set => Set(ref _loginStatus, value);
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




        private string _login;
        /// <summary>
        /// Логин клиента (представителя юр.лица)
        /// </summary>
        public string Login {
            get => _login;
            set
            {
                Set(ref _login, value);
                LoginClientUnique();
            }
        }


        private string _password;
        /// <summary>
        /// Пароль клиента (представителя юр.лица)
        /// </summary>
        public string Password
        { get => _password;
            set => Set(ref _password, value);
        }

        private string _tryPassword;
        /// <summary>
        /// Пароль подтверждения клиента (представителя юр.лица)
        /// </summary>
        public string TryPassword
        {
            get => _tryPassword;
            set
            {
                Set(ref _tryPassword, value);
                if (_password == _tryPassword) _statusPassword = "пароли равны"; //вспомогательна строка статуса пароля
                else if (_password != _tryPassword) _statusPassword = "пароли не равны";
                if (_tryPassword.Length < 1) _statusPassword = null;

                OnPropertyChanged("StatusPassword");
            }
        }

        private string _statusPassword;
        /// <summary>
        /// Статус правильности набора пароля
        /// </summary>
        public string StatusPassword
        {
            get => _statusPassword;
            set => Set(ref _statusPassword, value);
        }


        /// <summary>
        /// Имя клиента (представителя юр.лица)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия клиента (представителя юр.лица)
        /// </summary>
        public string LastName { get; set; }

        private string _age;
        /// <summary>
        /// Возраст клиента (существования юр.лица)
        /// </summary>
        public string Age
        {
            get => _age;
            set
            {
                try
                {
                    if (Convert.ToInt32(value) >= 18)
                        Set(ref _age, value);
                    else
                    {
                        MessageBox.Show("Возраст заемщика не может быть меньше 18 лет");
                        _age = null;
                    }
                }
                catch
                {
                    MessageBox.Show("Недопускается ввод символов и букв");
                }
            }
        }


        private string _phone;

        private string _phoneMask; //Для временного хранения телефона

        /// <summary>
        /// Телефон клиента 
        /// </summary>
        public string Phone
        {
            get => _phoneMask;
            set
            {
                if (value == _phone) return;
                _phone = value;
                PhoneMask();
                Set(ref _phone, value);
            }
        }

        public int PhoneLength { get; set; } //временное хранение количества символов

        /// <summary>
        /// метод создающий маску для телефона
        /// </summary>
        public void PhoneMask()
        {
            var newVal = Regex.Replace(_phone, @"[^0-9]", "");
            if (PhoneLength != newVal.Length && !string.IsNullOrEmpty(newVal))
            {
                PhoneLength = newVal.Length;
                _phoneMask = string.Empty;

                if (newVal.Length <= 1)
                {
                    _phoneMask = Regex.Replace(newVal, @"(\d{1})", "+$1");
                }
                else if (newVal.Length <= 4)
                {
                    _phoneMask = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+$1($2)");
                }
                else if (newVal.Length <= 7)
                {
                    _phoneMask = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+$1($2)$3");
                }
                else if (newVal.Length <= 9)
                {
                    _phoneMask = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4");
                }
                else if (newVal.Length > 9)
                {
                    _phoneMask = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                }
            }
        }


        public string NameOrganization { get; set; }

        private int _currentProgress = 50;
        /// <summary>
        /// Служит для анимации загрузочной строки
        /// </summary>
        public int CurrentProgress
        {
            get => _currentProgress;
            set => Set(ref _currentProgress, value);

        }

        #endregion

        BankSystemDB bankSystemDB = new BankSystemDB();

        private NavigationService _navigation;
        public LoginClientWindowViewModel(NavigationService navigation)
        {
            _navigation = navigation;
            
        }
             
        #region Команды
        public ICommand DouwnPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new StartWindow());
        });

        public ICommand UpEnterClientCommand => new DelegateCommand(() =>
        {
            
                _navigation.Navigate(new EnterClientWindow());
           
            
        });



        /// <summary>
        /// Команда регистрации нового клиента
        /// </summary>
        public ICommand ClientPageCommand => new DelegateCommand(() =>
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Поле Логин не может быть пустым");
            }
            else if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("Поле Имя не может быть пустым");
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("Поле Фамилия не может быть пустым");
            }
            else if (string.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Поле Телефон не может быть пустым");
            }
            else if (string.IsNullOrEmpty(Age))
            {
                MessageBox.Show("Поле Возраст не может быть пустым");
            }
            else if (CheckResult == true && string.IsNullOrEmpty(NameOrganization))
            {
                MessageBox.Show("Поле Название организации не может быть пустым");
            }
            else if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(TryPassword))
            {
                MessageBox.Show("Введите и подтвердите пароль");
            }
            else if (Password != TryPassword)
            {
                MessageBox.Show("Подтвердите пароль");
            }
            else if (_loginStatus) MessageBox.Show("Введите уникальный логин");
            else
            {   //Демонстрация работы с созданием потоков          
                Thread thread = new Thread(AddClient); //Запускаем в фоновом потоке работу с БД
                thread.Start();
                thread.IsBackground = true;
                Thread thread2 = new Thread(ProgressBar); //Запускаем в фоновом потоке анимацию статуса загрузки
                thread2.Start();
                thread2.IsBackground = true;
            }
        });
        #endregion

        /// <summary>
        /// Метод перехода в главную страницу клиента после добавления в БД
        /// </summary>
        private void BankSystemDB_ProgressChanged()
        {
            
            Application.Current.Dispatcher.Invoke(new Action(() => { _navigation.Navigate(new MainClientWimdow()); })); //выполняем в основном потоке
            Messenger.Default.Send(Login); //передаем логин подписаным viewModel
        }
       
        /// <summary>
        /// вспомогательный метод который меняет прогресс бар 
        /// </summary>        
        private void ProgressBar()
        {
            LoadingStatus = true;
            OnPropertyChanged("LoadingStatus");

            while (true)
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
        /// Определяем асинхронный метод поиска логинов
        /// </summary>
        private async void LoginClientUnique()
        {
            _loginStatus = await Task<bool>.Run(() => bankSystemDB.ClientUnique(Login));
            OnPropertyChanged("LoginStatus");
        }
           
        /// <summary>
        /// Метод создания клиента
        /// </summary>
        private void AddClient()
        {
            Models.Client Client;
            if (_checkResult) Client = new Legal(Login, Password, NameOrganization, FirstName, LastName, Convert.ToInt32(Age), Phone);
           
            else Client = new Physical(Login, Password, FirstName, LastName, Convert.ToInt32(Age), Phone);
                             
            try
            {
                bankSystemDB.ClientsBank.Add(Client);
                bankSystemDB.SaveChanges();

                BankSystemDB_ProgressChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");

            }
        }


       

    }
}
