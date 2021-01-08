using BankSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models.Status
{
    /// <summary>
    /// класс определяющий условия при статусе VIP
    /// </summary>
    class VIP : ICredit, IСontribution
    {

        public double MaxLimit { get; set; }
        public double MonthlyFee { get; set; }
        public int Period { get; set; }
        public double CreditRate { get; set; }
        public string Status { get; set; } = "VIP";

        public int ObjType { get; set; } = 3;


        //Поля вклада        
        public int TimeOfDeposit { get; set; }
        public double RateOfContribution { get; set; }

        public int ICreditID { get; set; }
        public int IСontributionID { get; set; }

        /// <summary>
        /// Метод вычисляющий кредитное предложение для клиента статуса standart
        /// </summary>
        /// <param name="client">Клиент</param>
        public void CreditOffer(Client client)
        {
            if (client is Legal) //если клиент юр.лицо
            {
                CreditRate = 14.0; //процентная ставка 

                MaxLimit = client.Profit * 30;

                Period = 10 * 12; //Период до 10 лет
            }
            else //Клиент физ.лицо
            {
                CreditRate = 13.0; //процентная ставка

                MaxLimit = client.Profit * 15;

                Period = ((65 - client.Age) > 10 ? 10 : (65 - client.Age)) * 12; //Период до 10 лет, или до 65 лет
            }

            MonthlyFee = (((CreditRate / 1200) * Math.Pow((1 + (CreditRate / 1200)), Period)) / ((Math.Pow((1 + (CreditRate / 1200)), Period)) - 1)) * MaxLimit; // Формула расчета аннуитетного платежа


        }

        public void СontributionOffer(Client client)
        {
            if (client is Legal) //если клиент юр.лицо
            {
                RateOfContribution = 10.0; //ставка по вкладу
                TimeOfDeposit = 6;

            }
            else //Клиент физ.лицо
            {
                RateOfContribution = 9.0; //ставка по вкладу
                TimeOfDeposit = 12;
            }
        }
    }
}
