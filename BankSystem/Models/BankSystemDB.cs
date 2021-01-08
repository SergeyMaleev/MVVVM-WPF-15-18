using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankSystem.Models
{
    /// <summary>
    /// Класс системы управления банком (Взаимодействия с базой данных)
    /// </summary>
   public class BankSystemDB : DbContext
   {          
       public BankSystemDB() : base("DBConnection") { }

       public DbSet<Client> ClientsBank { get; set; } //коллекция клиентов

       public DbSet<ExistingLoan> ExistingsLoan { get; set; } //коллекция клиентов

       public DbSet<ExistingContribution> ExistingContribution { get; set; } //коллекция клиентов


        private readonly SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подключения 
       {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "userstore",
            IntegratedSecurity = true,
       };


        /// <summary>
        /// Метод проверяющий уникальность логина (Написал для демонстрации работы SQL запроса)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool ClientUnique(string login)
        {
                       
            using (SqlConnection c = new SqlConnection(strCon.ConnectionString))
            {
                try
                {                    
                    c.Open();
                    var sql = @"SELECT * FROM Clients WHERE Login = @login";
                    SqlCommand command = new SqlCommand(sql, c);
                    command.Parameters.AddWithValue("@login", login);


                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) return true;           
                    else return false;                                 
                }
                catch 
                {
                    MessageBox.Show($"Нет соединения с базой данных");
                    return false;
                }

            } 
        }


        /// <summary>
        /// Метод проверяющий уникальность логина и подходящий к нему пароль
        /// </summary>
        /// <param name="login">Присутствие логина в базе</param>
        /// <param name="login">Пароль клиента</param>
        /// <returns></returns>
        public bool ClientUnique(string login, string password)
        {
            var UniqueLogin = ClientUnique(login);

            if (UniqueLogin)
            {
                using (SqlConnection c = new SqlConnection(strCon.ConnectionString))
                {
                    try
                    {
                        c.Open();
                        var sql = @"SELECT * FROM Clients WHERE Login = @login AND Password = @password";
                        SqlCommand command = new SqlCommand(sql, c);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {                            
                            return true;
                        } 
                        else
                        {
                            MessageBox.Show($"Неверный пароль");
                            return false;
                        } 
                    }
                    catch
                    {
                        MessageBox.Show($"Нет соединения с базой данных");
                        return false;
                    }

                }
            }
            else
            {
                MessageBox.Show($"Логин отсутствует в базе данных");
                return false;
            }
        }
    
   }
}
