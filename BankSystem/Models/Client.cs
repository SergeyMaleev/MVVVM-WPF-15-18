using BankSystem.Interface;
using BankSystem.Models.Json;
using BankSystem.Models.Status;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    /// <summary>
    /// Родительский класс клиентов
    /// </summary>
    [JsonConverter(typeof(BaseConverterClient))]
    public abstract class Client
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public int ClientID { get; set; }
        
        /// <summary>
        /// Логин клиента
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// пароль клиента
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата регистрации клиента
        /// </summary>
        public DateTime DateTime { get; set; } 

        /// <summary>
        /// Имя клиента (представителя юр.лица)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия клиента (представителя юр.лица)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age { get; set; }

        
        /// <summary>
        /// Контактный телефон
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Доход
        /// </summary>
        public double Profit { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public double PersonalAccount { get; set; }



        /// <summary>
        /// Предлагаемый кредит на условиях в зависимости от статуса клиента (изначально у всех Standart)
        /// </summary>
        public ICredit Credit { get; set; } = new Standart();//(в дальнейшем можно продумать условия смены при выполнении определенных условий)


        /// <summary>
        /// Предлагаемый вклад на условиях в зависимости от статуса клиента (изначально у всех Standart)
        /// </summary>
        public IСontribution Сontribution { get; set; } = new Standart();//(в дальнейшем можно продумать условия смены при выполнении определенных условий)

        /// <summary>
        /// коллекция для хранения имеющихся кредитов
        /// </summary>
        public ObservableCollection<ExistingLoan> ExistingLoan { get; set; } = new ObservableCollection<ExistingLoan>();


        /// <summary>
        /// коллекция для хранения имеющихся вкладов
        /// </summary>
        public ObservableCollection<ExistingContribution> ExistingContributions { get; set; } = new ObservableCollection<ExistingContribution>();



        /// <summary>
        /// Тип наследника для дессирилизации json
        /// </summary>
        public int ObjType { get; set; }



        /// <summary>
        /// Конструктор клиента для ручного заполнения
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Telephone">Телефон</param>
        public Client(string Login, string Password,string FirstName, string LastName, int Age, string Telephone)
        {
            this.DateTime = DateTime.Now;

            this.Login = Login;

            this.Password = Password;

            this.FirstName = FirstName;

            this.LastName = LastName;

            this.Age = Age;
          
            this.Telephone = Telephone;

        }


        /// <summary>
        /// Конструктор клиента для автоматического заполнения
        /// </summary>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Profit">Доход</param>
        /// <param name="Telephone">Телефон</param>
        public Client(string FirstName, string LastName, int Age, double Profit, string Telephone)
        {
            this.DateTime = DateTime.Now;

            this.FirstName = FirstName;

            this.LastName = LastName;

            this.Age = Age;

            this.Profit = Profit;

            this.Telephone = Telephone;

        }

        public Client() { }
    }
}
