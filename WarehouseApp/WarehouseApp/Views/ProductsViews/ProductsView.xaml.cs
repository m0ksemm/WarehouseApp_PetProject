using ServiceContracts.DTOs.CategoriesDTOs;
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
using WarehouseApp.ViewModels.ProductsViewModels;

namespace WarehouseApp.Views.ProductsViews
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        //private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (sender is ComboBox combo && combo.DataContext is ProductsViewModel vm)
        //    {
        //        // Якщо користувач обрав елемент зі списку, оновлюємо текст у полі
        //        if (combo.SelectedItem is CategoryResponse category)
        //        {
        //            vm.CategorySearchText = category.CategoryName == "All Categories" ? "" : category.CategoryName;
        //        }
        //    }
        //}
    }
}
