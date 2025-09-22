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

namespace ISIP422_Mozgunova.Windows
{
    public partial class QuantityWindow : Window
    {
        public int Quantity { get; set; }

        public QuantityWindow(string message, int maxQuantity)
        {
            InitializeComponent();
            MessageText.Text = message + "\nМаксимум: " + maxQuantity;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                Quantity = quantity;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Число не должно быть отрицательным и не должно превышать максимум!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}