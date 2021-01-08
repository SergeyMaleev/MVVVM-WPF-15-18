using BankSystem.Models;
using BankSystem.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ViewModels.Admin
{
    class ListCreditWindowViewModel : ViewModelsBase
    {

        public List<Models.ExistingLoan> ExistingLoans { get; set; }

        BankSystemDB bankSystemDB = new BankSystemDB();

        public ListCreditWindowViewModel()
        {
            ExistingLoans = bankSystemDB.ExistingsLoan.ToList();
        }

    }
}
