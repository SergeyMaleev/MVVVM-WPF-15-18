using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ViewModels.Base
{
    public abstract class ViewModelsBase : INotifyPropertyChanged
    {

        //Подписываемся на изменения свойства
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Передаем имя свойства и генерируем событие
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод обнавления значения свойства
        /// </summary>
        /// <param name="faild">Ссылка на поле свойства</param>
        /// <param name="value">Новое значение </param>
        /// <param name="propertyName">Имя свойства(Может быть определено кампилятором самомтоятельно)</param>
        /// <returns></returns>
        protected bool Set<T>(ref T faild, T value, [CallerMemberName] string propertyName = null)
        {
            //Если ссылка на поле свойства равна новой переменной
            if (Equals(faild, value)) return false;
            //Присваеваем новое значение
            faild = value;
            //Выполняем событие
            OnPropertyChanged(propertyName);

            //Возвражение ложь или истина нужна для выполнения других связаных событий

            return true;
        }
    }
}
