using BankSystem.Models;
using BankSystem.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ViewModels.Admin
{
    class ListDepositWindowViewModel : ViewModelsBase
    {

        public List<Models.ExistingContribution> ExistingContributions { get; set; }

        BankSystemDB bankSystemDB = new BankSystemDB();

        public ListDepositWindowViewModel()
        {
            ExistingContributions = bankSystemDB.ExistingContribution.ToList();
        }


    }
}
