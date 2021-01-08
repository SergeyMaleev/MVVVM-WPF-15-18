using BankSystem.Models;
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
    class ClientDepositWindowViewModel : ViewModelsBase
    {


        private double _personalAccount;
        /// <summary>
        /// Лицевой счет клиента
        /// </summary>
        public double PersonalAccount
        {
            get => _personalAccount;
            set => Set(ref _personalAccount, value);
        }

        private string _currentLoginClient;
        public virtual string CurrentLoginClient 
        { get => _currentLoginClient;
            set
            {               
                Set(ref _currentLoginClient, value); //Как только получаем сообщение с логином находим его лицевой счет
                try
                {
                PersonalAccount = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient).PersonalAccount;
                ContributionCurentClient(); 
                }
                catch
                {
                    MessageBox.Show("Отсутствует соединение с базой данных");
                }
                           
            }
        
        }

        /// <summary>
        /// Выбранная сумма  для открытия депозита
        /// </summary>
        public double DepositAmountCurrentClient { get; set; }


        /// <summary>
        /// Минимальный срок вклада (Месяцев)
        /// </summary>
        public int TimeOfDepositCurrentClient { get; set; }

        /// <summary>
        /// Ставка вклада
        /// </summary>
        public double RateOfContributionCurrentClient { get; set; }


        /// <summary>
        /// По какому статусу считаем 
        /// </summary>
        public string StatusCurrentClient { get; set; }


        private List<ExistingContribution> _existingContributionCurrentClient;
        /// <summary>
        /// коллекция для хранения имеющихся вкладов
        /// </summary>
        public List<ExistingContribution> ExistingContributionCurrentClient
        {
            get => _existingContributionCurrentClient;
            set
            {
                Set(ref _existingContributionCurrentClient, value);

            }
        }


        public Models.Client CurrentClient { get; set; }

        BankSystemDB bankSystemDB = new BankSystemDB();
        public ClientDepositWindowViewModel()
        {
            Messenger.Default.Register<string>(this, (message) => CurrentLoginClient = message); //Принимаем логин 

            ExistingContributionCurrentClient = new List<ExistingContribution>();
        }


        /// <summary>
        /// Команда расчета условий открытия вклада
        /// </summary>
        public ICommand IDepositCommand => new DelegateCommand(() =>
        {
            CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient);

            CurrentClient.Сontribution.СontributionOffer(CurrentClient);

            this.StatusCurrentClient = CurrentClient.Сontribution.Status;
            OnPropertyChanged("StatusCurrentClient");
            this.TimeOfDepositCurrentClient = CurrentClient.Сontribution.TimeOfDeposit;
            OnPropertyChanged("TimeOfDepositCurrentClient");
            this.RateOfContributionCurrentClient = CurrentClient.Сontribution.RateOfContribution;
            OnPropertyChanged("RateOfContributionCurrentClient");
        });

        /// <summary>
        /// Команда открытия вклада при согласии с предложением и добавления в список клиентов банка
        /// </summary>
        public ICommand IOpenDepositCommand => new DelegateCommand(() =>
        {
            CurrentClient = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient);

            if (DepositAmountCurrentClient < CurrentClient.PersonalAccount)
            {
                CurrentClient.ExistingContributions.Add(new ExistingContribution(DepositAmountCurrentClient, RateOfContributionCurrentClient, TimeOfDepositCurrentClient)); //открываем вклад клиенту 

                CurrentClient.PersonalAccount -= DepositAmountCurrentClient; //Вычитаем с лицевого счета сумму депозита у клиента
              
                bankSystemDB.SaveChanges();

                ContributionCurentClient();
                MessageBox.Show("Вклад успешно открыт");
                PersonalAccount -= DepositAmountCurrentClient; //Вычитаем с лицевого счета сумму депозита
                OnPropertyChanged("PersonalAccount");
                DepositAmountCurrentClient = 0;
                OnPropertyChanged("DepositAmountCurrentClient");
            }
            else if(DepositAmountCurrentClient < 0)
            {
                MessageBox.Show("Некорректная сумма");
            }
            else 
            {
                MessageBox.Show("Недостаточно средств");
            }

            


        });

     
        /// <summary>
        ///метод полуения вкладов клиента
        /// </summary>
        private void ContributionCurentClient()
        {
            var _id = bankSystemDB.ClientsBank.Single(c => c.Login == CurrentLoginClient).ClientID;

            ExistingContributionCurrentClient = bankSystemDB.ExistingContribution.Where(c => c.ClientID == _id).ToList();
            OnPropertyChanged("ExistingContributionCurrentClient");
        }
    }
}
