using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankSystem.View.Client
{
    /// <summary>
    /// Логика взаимодействия для AddMoneyWindow.xaml
    /// </summary>
    public partial class AddMoneyWindow : Window
    {
        //public static DependencyProperty MoneyProperty = DependencyProperty.Register(
        //    nameof(MoneyAddClient),
        //    typeof(Double),
        //    typeof(CashAccountWindow),
        //    new PropertyMetadata(default(Double))
        //    );


        public double MoneyAddClient { get; set; }

        public AddMoneyWindow()
        {
            InitializeComponent();

            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {

                try
                {
                    MoneyAddClient = Convert.ToDouble(textBox.Text);
                    this.DialogResult = !false;
                }
                catch
                {
                    MessageBox.Show("Введено недопустимое значение");
                    textBox.Text = null;
                }
            };

        }


    }
}
