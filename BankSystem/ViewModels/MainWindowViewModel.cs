using BankSystem.View;
using BankSystem.ViewModels;
using BankSystem.ViewModels.Base;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankSystem.ViewModels
{
    /// <summary>
    /// Каркас страницы 
    /// </summary>
    class MainWindowViewModel : ViewModelsBase
    {


        private Page _page;
        
        public Page CurrenPage
        {
            get => _page;
            set => Set(ref _page, value);
        }
        
        public MainWindowViewModel(NavigationService navigation)
        {

            navigation.OnPageChanged += Page => CurrenPage = Page; //подписываемся на навигацию для пооказа текущей страницы
            navigation.Navigate(new StartWindow()); //первая страница, страница входа
           
        }

        
    }
}
