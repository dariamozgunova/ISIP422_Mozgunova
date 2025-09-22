using ISIP422_Mozgunova.Classes;
using ISIP422_Mozgunova.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ISIP422_Mozgunova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InventoryManager inventory;

        public MainWindow()
        {
            InitializeComponent();
            inventory = new InventoryManager();
            ShowAllProducts();
            StatusText.Text = "Загружено " + inventory.Products.Count + " товаров";
        }

        private void ShowAllProducts()
        {
            ProductsGrid.ItemsSource = null;
            ProductsGrid.ItemsSource = inventory.Products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new AddProductWindow();
                if (dialog.ShowDialog() == true)
                {
                    inventory.AddProduct(dialog.ProductName, dialog.ProductPrice, dialog.ProductQuantity, dialog.ProductCategory);
                    ShowAllProducts();
                    StatusText.Text = "Товар добавлен: " + dialog.ProductName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }


        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                inventory.DeleteProduct(selectedProduct.Code);
                ShowAllProducts();
                StatusText.Text = "Товар удален: " + selectedProduct.Name;
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления!");
            }
        }

        private void SellProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                if (selectedProduct.Quantity == 0)
                {
                    MessageBox.Show("Товара нет в наличии.");
                    return;
                }

                var dialog = new QuantityWindow("Продажа товара: " + selectedProduct.Name, selectedProduct.Quantity);
                if (dialog.ShowDialog() == true)
                {
                    if (inventory.SellProduct(selectedProduct.Code, dialog.Quantity))
                    {
                        ShowAllProducts();
                        StatusText.Text = "Продано " + dialog.Quantity + " шт. товара: " + selectedProduct.Name;
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно товара на складе!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для продажи!");
            }
        }

        private void OrderSupply_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selectedProduct)
            {
                var dialog = new QuantityWindow("Поставка товара: " + selectedProduct.Name, 1000);
                if (dialog.ShowDialog() == true)
                {
                    inventory.OrderSupply(selectedProduct.Code, dialog.Quantity);
                    ShowAllProducts();
                    StatusText.Text = "Заказано " + dialog.Quantity + " шт. товара: " + selectedProduct.Name;
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для заказа поставки!");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var results = inventory.SearchProducts(SearchTextBox.Text);
            ProductsGrid.ItemsSource = null;
            ProductsGrid.ItemsSource = results;
            StatusText.Text = "Найдено товаров: " + results.Count;
        }
    }
}
