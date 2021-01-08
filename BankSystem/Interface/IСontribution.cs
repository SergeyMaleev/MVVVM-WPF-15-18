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
    /// Интерфейс предлагаемого вклада
    /// </summary>
    [JsonConverter(typeof(BaseConverterICredit))]
    public interface IСontribution
    {

        int IСontributionID { get; set; }
        
        /// <summary>
        /// Минимальный срок вклада (Месяцев)
        /// </summary>
        int TimeOfDeposit { get; set; }


        /// <summary>
        /// Статус
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Ставка вклада
        /// </summary>
        double RateOfContribution { get; set; }

        /// <summary>
        /// метод вычисления условий вклада для физ и юр лица
        /// </summary>       
        void СontributionOffer(Client client);
    }
}
