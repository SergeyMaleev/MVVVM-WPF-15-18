using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    /// <summary>
    /// Класс открытого вклада у клиента
    /// </summary>
    public class ExistingContribution
    {

        public int ExistingContributionID { get; set; }
        /// <summary>
        /// Исходная сумма вклада
        /// </summary>
        public double DepositAmount { get; set; }


        private double _currentDepositAmount; //для хранения текущей суммы по вкладу

        /// <summary>
        /// Следующий месяц подсчета процентов
        /// </summary>
        private int _month; //При открытии подсчет будет в первый месяц


        /// <summary>
        /// Текущая сумма вклада
        /// </summary>
        public double CurrentDepositAmount
        {
            get
            {
                var nMonth = (int)(DateTime.Now.Subtract(DateTime).TotalDays / (365.25 / 12))
                    + (DateTime.Now.AddDays(1).Month == DateTime.Now.Month ? 0 : 1); //получаем количество месяцев

                if (nMonth > _month) //каждый месяц будет пересчет суммы вклада
                {
                    _month++;
                    _currentDepositAmount = _currentDepositAmount * ((RateOfDeposit * 0.01) / 365 * 30);
                    return _currentDepositAmount;
                }

                return _currentDepositAmount;
            }

        }


        /// <summary>
        /// Минимальный срок вклада
        /// </summary>
        public int MinDepositPeriod { get; set; }


        /// <summary>
        /// Процентная ставка вклада
        /// </summary>
        public double RateOfDeposit { get; set; }


        /// <summary>
        /// Дата открытия вклада
        /// </summary>
        public DateTime DateTime { get; set; }


        /// <summary>
        /// Дата возможного закрытия вклада
        /// </summary>
        public DateTime DateTimeClose { get; set; }


        public int ClientID { get; set; }


        /// <summary>
        /// Конструктор открытого вклада
        /// </summary>
        /// <param name="DepositAmount">Внесенная сумма</param>
        /// <param name="RateOfDeposit">Ставка по вкладу</param>
        /// <param name="MinDepositPeriod">Минимальный срок открытия</param>
        public ExistingContribution(double DepositAmount, double RateOfDeposit, int MinDepositPeriod)
        {
            this.DateTime = DateTime.Now;

            this.RateOfDeposit = RateOfDeposit;

            this.DepositAmount = DepositAmount;

            this.MinDepositPeriod = MinDepositPeriod;

            this.DateTimeClose = DateTime.AddMonths(this.MinDepositPeriod);

            _currentDepositAmount = DepositAmount; //по умолчанию 
        }

        public ExistingContribution() { }
    }
}
