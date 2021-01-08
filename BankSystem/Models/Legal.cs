using BankSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    /// <summary>
    /// Юридическое лицо
    /// </summary>
    class Legal : Client
    {
        
        /// <summary>
        /// Наименование организации
        /// </summary>
        public string NameOrganization { get; set;}



        /// <summary>
        /// Конструктор для ручного заполнения 
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="NameOrganization">Название организации</param>
        /// <param name="FirstName">Имя представителя</param>
        /// <param name="LastName">Фамилия представителя</param>
        /// <param name="Age">Возраст представителя</param>        
        /// <param name="Telephone">Контактный телефон</param>
        public Legal(string Login, string Password, string NameOrganization, string FirstName, string LastName, int Age, string Telephone)
            : base(Login, Password, FirstName, LastName, Age, Telephone)
        {
            this.NameOrganization = NameOrganization;
            ObjType = 1;
           
        }

        /// <summary>
        /// Конструктор для автозаполнения 
        /// </summary>       
        /// <param name="NameOrganization">Название организации</param>
        /// <param name="FirstName">Имя представителя</param>
        /// <param name="LastName">Фамилия представителя</param>
        /// <param name="Age">Возраст представителя</param>   
        /// <param name="Profit">Доход организации</param> 
        /// <param name="Telephone">Контактный телефон</param>
        public Legal(string NameOrganization, string FirstName, string LastName, int Age, double Profit, string Telephone)
            : base(FirstName, LastName, Age, Profit, Telephone)
        {
            this.NameOrganization = NameOrganization;
            ObjType = 1;

        }

        public Legal() { }
    }
}
