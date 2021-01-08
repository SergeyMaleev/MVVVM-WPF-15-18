using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models.Json
{
    /// <summary>
    /// Класс загрузки и сохранения файлов json
    /// </summary>
    public class FileJson
    {

        private string PATH;//Путь файла


        
        public FileJson(string path)
        {
            PATH = path;
        }


        /// <summary>
        /// Метод загружающий коллекцию клиентов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Client> LoadData()
        {
            var FileExists = File.Exists(PATH);//Если файла нет, создаем новый
            if (!FileExists)
            {
                File.CreateText(PATH).Dispose();
                return new ObservableCollection<Client>();
            }
            using (StreamReader reader = File.OpenText(PATH))//Если есть открываем
            {

                var fileText = reader.ReadToEnd();//Прочитываем до конца             
                return JsonConvert.DeserializeObject<ObservableCollection<Client>>(fileText);//Возвращаем в исходном виде
            }

        }

        /// <summary>
        /// Метод сохраняющий коллекцию клиентов
        /// </summary>
        /// <param name="tasksList">коллекция клиентов</param>
        public void SaveData(object tasksList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(tasksList);
                writer.Write(output);

            }

        }


    }
}
