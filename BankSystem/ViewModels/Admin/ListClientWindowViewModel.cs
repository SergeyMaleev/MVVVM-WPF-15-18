using BankSystem.Models;
using BankSystem.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ViewModels.Admin
{
    class ListClientWindowViewModel : ViewModelsBase
    {
        
        public List<Models.Client> Clients { get; set; }

        BankSystemDB bankSystemDB = new BankSystemDB();

        public ListClientWindowViewModel()
        {
            Clients = bankSystemDB.ClientsBank.ToList();
        }




    }
}
