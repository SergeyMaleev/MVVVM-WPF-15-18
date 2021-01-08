using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    /// <summary>
    /// Класс имеющигося кредита у клиента
    /// </summary>
    public class ExistingLoan
    {

        public int ExistingLoanID { get; set; }
        
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public double LoanAmount { get; set; } 

        /// <summary>
        /// Период кредита
        /// </summary>
        public int LoanPeriod { get; set; }

        /// <summary>
        /// Платеж по кредиту (ежемесячный)
        /// </summary>
        public double CreditPayment { get; set; }

        /// <summary>
        /// Дата выдачи кредита
        /// </summary>
        public DateTime DateTime { get; set; }

        public int ClientID { get; set; }

        /// <summary>
        /// Конструктор для заполнения информации по кредиту
        /// </summary>
        /// <param name="LoanAmount">Сумма кредита</param>
        /// <param name="LoanPeriod">Период выплаты кредита</param>
        /// <param name="CreditPayment">Ежемесячный платеж</param>
        public ExistingLoan(double LoanAmount, int LoanPeriod, double CreditPayment)
        {

            this.DateTime = DateTime.Now;

            this.LoanAmount = LoanAmount;

            this.LoanPeriod = LoanPeriod;

            this.CreditPayment = CreditPayment;
        
        
        
        }

        public ExistingLoan() { }











    }
}
