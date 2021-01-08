using BankSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models.Status
{
    /// <summary>
    /// класс определяющий условия при статусе Gold
    /// </summary>
    class Gold : ICredit, IСontribution
    {


        //Поля кредита
        public double MaxLimit { get; set; }
        public double MonthlyFee { get; set; }
        public int Period { get; set; }
        public double CreditRate { get; set; }
        public string Status { get;  set; } = "Gold";

        public int ObjType { get; set; } = 2;

        //Поля вклада        
        public int TimeOfDeposit { get; set; }

        public double RateOfContribution { get; set; }

        public int ICreditID { get; set; }
        public int IСontributionID { get; set; }

        /// <summary>
        /// Метод вычисляющий кредитное предложение для клиента статуса Gold
        /// </summary>
        /// <param name="client">Клиент</param>
        public void CreditOffer(Client client)
        {
            if (client is Legal) //если клиент юр.лицо
            {
                CreditRate = 16.0; //процентная ставка 

                MaxLimit = client.Profit * 25;

                Period = 10 * 12; //Период до 10 лет
            }
            else //Клиент физ.лицо
            {
                CreditRate = 14.0; //процентная ставка

                MaxLimit = client.Profit * 13;

                Period = ((63 - client.Age) > 8 ? 8 : (63 - client.Age)) * 12; //Период до 8 лет, или до 63 лет
            }

            MonthlyFee = (((CreditRate / 1200) * Math.Pow((1 + (CreditRate / 1200)), Period)) / ((Math.Pow((1 + (CreditRate / 1200)), Period)) - 1)) * MaxLimit; // Формула расчета аннуитетного платежа


        }

        public void СontributionOffer(Client client)
        {
            if (client is Legal) //если клиент юр.лицо
            {
                RateOfContribution = 9.0; //ставка по вкладу
                TimeOfDeposit = 12;

            }
            else //Клиент физ.лицо
            {
                RateOfContribution = 8.0; //ставка по вкладу
                TimeOfDeposit = 18;
            }
        }
    }
}
