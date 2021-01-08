using BankSystem.Models.Status;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    /// <summary>
    /// Статический класс генерации данных о персонале
    /// </summary>
    public static class ClientRandomGeneration
    {
        public static Random random = new Random();

        private static int _numberOrganization;


        /// <summary>
        /// Метод возращающий массив из [0] - имя [1] - Фамилия одного пола, [2]-Телефон
        /// </summary>
        /// <returns></returns>
        public static string[] GetRandName()
        {
            //Список хранящий имя и фамилмю
            string[] firstNamelastName = new string[3];

            var mansFirstNames = new string[]
         {
            "Владимир",
            "Петр",
            "Анатолий",
            "Геннадий",
            "Роман",
            "Никита",
            "Илья",
            "Сергей",
            "Тимофей",
            "Любомир",
            "Максим",
            "Степан",
            "Всеволод",
            "Ефим",
            "Павел",
            "Артем",
            "Евгений",
            "Валерий"
         };

            var mansLastNames = new string[]
         {
            "Царёв",
            "Борисов",
            "Минеев",
            "Ворожцов",
            "Тимирёв",
            "Ястребов",
            "Чекмарёв",
            "Федосов",
            "Чегодаев",
            "Сыров"
         };

            var womansFirstNames = new string[]
         {
            "Зинаида",
            "Наталья",
            "Лилия",
            "Залина",
            "Елена",
            "Светлана",
            "Лидия",
            "Татьяна",
            "Янина",
            "Елизавета"
         };

            var womansLastNames = new string[]
         {
            "Лосевская",
            "Огородникова",
            "Кутичева",
            "Цой",
            "Кунаковская",
            "Саянкина",
            "Кобелева",
            "Водопьянова",
            "Карандашова",
            "Явчуновская"
         };

            var telephonNumber = new string[]
         {
            "+7 (987) 914-22-18",
            "+7 (929) 915-25-11",
            "+7 (937) 664-55-51",
            "+7 (812) 955-35-17",
            "+7 (929) 656-22-41"            
         };

            var manOrWoman = random.Next(0, 2);
            //Мужина или женщина
            switch (manOrWoman)
            {
                case 0:
                    firstNamelastName[0] = (mansFirstNames[random.Next(0, mansFirstNames.Length)]);
                    firstNamelastName[1] = (mansLastNames[random.Next(0, mansLastNames.Length)]);
                    firstNamelastName[2] = (telephonNumber[random.Next(0, telephonNumber.Length)]);
                    return firstNamelastName;
                case 1:
                    firstNamelastName[0] = (womansFirstNames[random.Next(0, womansFirstNames.Length)]);
                    firstNamelastName[1] = (womansLastNames[random.Next(0, womansLastNames.Length)]);
                    firstNamelastName[2] = (telephonNumber[random.Next(0, telephonNumber.Length)]);
                    return firstNamelastName;
                default:
                    return null;
            }

        }

        /// <summary>
        /// Метод автоматически заполняющий коллекцию клиентов
        /// </summary>
        /// <param name="Clients"></param>
        /// <returns></returns>
        public static Client AutoClient()
        {
           Client client;
            ++_numberOrganization;

            var ClientInfo = GetRandName();

            

            int index = random.Next(0, 2);

            switch (index)
            {
                case 0:
                    client = new Legal(
                        $"Организация №{_numberOrganization}",
                        ClientInfo[0],
                        ClientInfo[1],
                        random.Next(19, 55),
                        random.Next(180000, 2000000),
                        ClientInfo[2]
                        );
               return StatusClient(client);
                    
                    
                case 1:
                    client = new Physical(                       
                        ClientInfo[0],
                        ClientInfo[1],
                        random.Next(19, 55),
                        random.Next(18000, 200000),
                        ClientInfo[2]
                        );
                    return StatusClient(client);

                default: 
                    return null;

            }
        
        }

        /// <summary>
        /// Вспомогательный метод выдачи кредита с учетом статуса клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        private static Client StatusClient(Client client)
        {
            var index = random.Next(0, 21);

            if (index <= 10)
            {
                client.Credit = new Standart();
                client.Credit.CreditOffer(client);
                client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                client.PersonalAccount += client.Credit.MaxLimit;

            }
            else if (index > 10 && index < 17)
            {

                client.Credit = new Gold();
                client.Credit.CreditOffer(client);
                client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                client.PersonalAccount += client.Credit.MaxLimit;

                //Возможно получить 2 кредит, но с условием что ежемесячный платеж минусуется от зарплаты
                if (random.Next(0, 2) == 1)
                {
                    client.Profit = client.Profit - client.Credit.MonthlyFee;
                    client.Credit.CreditOffer(client);
                    client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                    client.PersonalAccount += client.Credit.MaxLimit;
                }    

            }
            else
            {
                client.Credit = new VIP();
                client.Credit.CreditOffer(client);
                client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                client.PersonalAccount += client.Credit.MaxLimit;

                //Возможно получить 2 кредит, но с условием что ежемесячный платеж минусуется от зарплаты
                if (random.Next(0, 2) == 1)
                {
                    client.Profit = client.Profit - client.Credit.MonthlyFee;
                    client.Credit.CreditOffer(client);
                    client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                    client.PersonalAccount += client.Credit.MaxLimit;

                    //Возможно получить 3 кредит, но с условием что ежемесячный платеж минусуется от зарплаты
                    if (random.Next(0, 2) == 1)
                    {
                        client.Profit = client.Profit - client.Credit.MonthlyFee;
                        client.Credit.CreditOffer(client);
                        client.ExistingLoan.Add(new ExistingLoan(client.Credit.MaxLimit, client.Credit.Period, client.Credit.MonthlyFee));
                        client.PersonalAccount += client.Credit.MaxLimit;

                    }
                }

            }

            return client;

        }

        
    }
}
