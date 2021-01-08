using BankSystem.Models;
using BankSystem.Models.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Interface
{

    /// <summary>
    /// Интерфейс предлагаемого кредитного продукта
    /// </summary>
    [JsonConverter(typeof(BaseConverterICredit))]
    public interface ICredit
    {
        
        int ICreditID { get; set; }

        /// <summary>
        /// Статус клиента
        /// </summary>
        string Status { get; }
        
             
        /// <summary>
        /// Максимальный лимит по кредиту
        /// </summary>
        double MaxLimit { get; set; }


        /// <summary>
        /// Ежемесячная плата по кредиту
        /// </summary>
        double MonthlyFee { get; set; }

        /// <summary>
        /// Максимальный срок на который выдан кредит (месяцев)
        /// </summary>
        int Period { get; set; }


        /// <summary>
        /// Базовая процентная ставка по кредиту
        /// </summary>
        double CreditRate { get; set; }

        /// <summary>
        /// тип для дессирилизации наследников json
        /// </summary>
        int ObjType { get; set; }


        void CreditOffer(Client client); //Лучшее кредитное предложение для клиента

    }
}
