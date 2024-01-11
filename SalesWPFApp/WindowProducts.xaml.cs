using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        IProductRepository productRepository = new ProductRepository();
        public ObservableCollection<Product> Products { get; set; }
        public WindowProducts()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
            dgvProductList.ItemsSource = Products;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
        }

        public void LoadProductList()
        {
            try
            {
                var products = productRepository.GetProducts();

                Products.Clear();

                foreach (var product in products)
                {
                    Products.Add(product);
                }

                if (products.Count() == 0)
                {
                    btnDelete.IsEnabled = false;
                }
                else
                {
                    btnDelete.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product list");
            }
        }

        private void dgvProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a product");
                return;
            }

            Product selectedProduct = (Product)dataGrid.SelectedItem;
            int productId = selectedProduct.ProductId;

            Product productInfo = productRepository.GetProductById(productId);

            if (productInfo != null)
            {
                WindowProductDetail windowProductDetail = new WindowProductDetail();

                windowProductDetail.ProductRepository = productRepository;
                windowProductDetail.ProductInfo = productInfo;
                windowProductDetail.InsertOrUpdate = true;
                windowProductDetail.Show();
            }
            else
            {
                MessageBox.Show("Invalid product");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WindowProductDetail windowProductDetail = new WindowProductDetail();

            windowProductDetail.ProductRepository = productRepository;
            windowProductDetail.InsertOrUpdate = false;
            windowProductDetail.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = (Product)dgvProductList.SelectedItem;
                productRepository.DeleteProduct(product.ProductId);
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete product");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
