using BankSystem.Models;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BankSystem.ViewModels.Client
{
    class ClientCreditWindowViewModel : ViewModelsBase
    {
        #region Поля отображения предлагаемого кредитного продукта

        /// <summary>
        /// Процентная ставка по кредиту
        /// </summary>
        public double CreditRateCurrentClient { get; set; }

        /// <summary>
        /// Максимальная сумма кредита
        /// </summary>
        public double MaxMoneyCurrentClient { get; set; }

        /// <summary>
        /// Максимальный срок
        /// </summary>
        public int MaxPeriodCurrentClient { get; set; }

        /// <summary>
        /// Ужемесячный платеж
        /// </summary>
        public double MonthlyFeeCurrentClient { get; set; }

        /// <summary>
        /// По какому статусу считаем кредмт 
        /// </summary>
        public string StatusCurrentClient { get;  set; }


        #endregion

        
        private double _profitCurrentClient;
        /// <summary>
        /// Зарплата клиента, нужна для расчета возможного кредита
        /// </summary>
        public double ProfitCurrentClient
        {
            get => _profitCurrentClient;
            set => Set(ref _profitCurrentClient, value);
        }

        private string _currentLoginClient;
        public virtual string CurrentLoginClient
        { 
            get => _currentLoginClient;
            set
            {
                Set(ref _currentLoginClient, value);
                Thread thread1 = new Thread(CreditCurentClient); //Запускаем в фоновом потоке нахождение кредитов клиента
                thread1.Start();                           //Иначе приложение зависает на время обращения к БД
                thread1.IsBackground = true;
               

            }
        }


        //Для отображеия индивидуального кредита
        private double _individualLimit;
        
        public double IndividualLimit
        {
            get => _individualLimit;
            set 
            {
                Set(ref _individualLimit, value);
                CreditOffer(CreditRateCurrentClient, IndividualLimit, IndividualPeriod);
            }
        } //лимит выбранный пользователем

        private int _individualPeriod;
        public int IndividualPeriod
        {
            get => _individualPeriod;
            set
            {
                Set(ref _individualPeriod, value);
                CreditOffer(CreditRateCurrentClient, IndividualLimit, IndividualPeriod);
            }
        }//период выбранный пользователем

        private double _individualMonthlyFee; //ежемесячный платеж 

        public double IndividualMonthlyFee
        {
            get => _individualMonthlyFee;
            set => Set(ref _individualMonthlyFee, value);

        }

        private List<ExistingLoan> _existingLoanCurrentClient;
        /// <summary>
        /// коллекция для хранения имеющихся кредитов
        /// </summary>
        public List<ExistingLoan> ExistingLoanCurrentClient
        {
            get => _existingLoanCurrentClient;
            set
            {
                Set(ref _existingLoanCurrentClient, value);


            }
        }
        public Models.Client CurrentClient { get; set; }

        BankSystemDB bankSystemDB = new BankSystemDB();
        public ClientCreditWindowViewModel()
        {
            Messenger.Default.Register<string>(this, (message) => CurrentLoginClient = message); //Принимаем логин 

            ExistingLoanCurrentClient = new List<ExistingLoan>();
        }

        public ICommand CreditOfferCommand => new DelegateCommand(() =>
        {
            try
            {
                CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient);
                CurrentClient.Profit = ProfitCurrentClient;

                CurrentClient.Credit.CreditOffer(CurrentClient); //Подбираем предложение для клиента

                this.CreditRateCurrentClient = CurrentClient.Credit.CreditRate;
                OnPropertyChanged("CreditRateCurrentClient");
                this.MaxMoneyCurrentClient = CurrentClient.Credit.MaxLimit;
                OnPropertyChanged("MaxMoneyCurrentClient");
                this.MaxPeriodCurrentClient = CurrentClient.Credit.Period;
                OnPropertyChanged("MaxPeriodCurrentClient");
                this.MonthlyFeeCurrentClient = CurrentClient.Credit.MonthlyFee;
                OnPropertyChanged("MonthlyFeeCurrentClient");
                this.StatusCurrentClient = CurrentClient.Credit.Status;
                OnPropertyChanged("StatusCurrentClient");
            }
            catch
            {
                MessageBox.Show("Не удалось соединиться с базой данных, попробуйте еще раз");

            }

            
        });
        
        
        /// <summary>
        /// Команда выдачи кредита при согласии с предложением и добавления в список клиентов банка
        /// </summary>
        public ICommand ICreditCreditCommand => new DelegateCommand(() =>
        {
            CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient);

            CurrentClient.ExistingLoan.Add(new ExistingLoan(MaxMoneyCurrentClient, MaxPeriodCurrentClient, MonthlyFeeCurrentClient)); //выдаем кредит клиенту 

            CurrentClient.PersonalAccount += MaxMoneyCurrentClient; //Добавляем с лицевой счет сумму кредита
          
            bankSystemDB.SaveChanges();

            CreditCurentClient();


        }, () => MaxMoneyCurrentClient > 0);

        /// <summary>
        /// Команда выдачи кредита на индивидуальных условиях и добавления в список клиентов банка
        /// </summary>
        public ICommand ICreditPersonalCommand => new DelegateCommand(() =>
        {

            CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient);


            CurrentClient.ExistingLoan.Add(new ExistingLoan(IndividualLimit, IndividualPeriod, IndividualMonthlyFee)); //выдаем кредит клиенту

            CurrentClient.PersonalAccount += IndividualLimit; //Добавляем с лицевой счет сумму кредита            
            bankSystemDB.SaveChanges();

            CreditCurentClient();




        }, () => IndividualLimit > 0 && IndividualPeriod > 0);

        /// <summary>
        /// Метод вычисляющий сумму ежемесячного платежа 
        /// </summary>
        /// <param name="CreditRate"> ставка процентов готовых</param>
        /// <param name="IndividualLimit"></param>
        /// <param name="IndividualPeriod"></param>
        /// <returns></returns>
        private void CreditOffer(double CreditRate, double IndividualLimit, int IndividualPeriod)
        {
            _individualMonthlyFee = (((CreditRate / 1200) * Math.Pow((1 + (CreditRate / 1200)), IndividualPeriod)) / ((Math.Pow((1 + (CreditRate / 1200)), IndividualPeriod)) - 1)) * IndividualLimit;
            CreditCurentClient();
            OnPropertyChanged("IndividualMonthlyFee");
        }

        /// <summary>
        ///метод полуения кредитов клиента
        /// </summary>
        private void CreditCurentClient()
        {
            var _id = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient).ClientID;

            ExistingLoanCurrentClient = bankSystemDB.ExistingsLoan.Where(c => c.ClientID == _id).ToList();
            OnPropertyChanged("ExistingLoanCurrentClient");
        }
    }
}
