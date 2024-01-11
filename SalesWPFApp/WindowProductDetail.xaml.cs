using BusinessObject;
using DataAccess.Repository;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowMemberDetail.xaml
    /// </summary>
    public partial class WindowProductDetail : Window
    {
        public IProductRepository ProductRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Product ProductInfo { get; set; }

        public WindowProductDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (InsertOrUpdate == true)
            {
                txtProductID.Text = ProductInfo.ProductId.ToString();
                txtCategoryID.Text = ProductInfo.CategoryId.ToString();
                txtProductName.Text = ProductInfo.ProductName;
                txtWeight.Text = ProductInfo.Weight;
                txtUnitPrice.Text = ProductInfo.UnitPrice.ToString();
                txtUnitsInStock.Text = ProductInfo.UnitsInStock.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var memberu = new Member
                //{
                //    MemberId = int.Parse(txtMemberID.Text),
                //    Email = txtEmail.Text,
                //    CompanyName = txtCompanyName.Text,
                //    City = txtCity.Text,
                //    Country = txtCountry.Text,
                //    Password = txtPassword.Password

                //};


                if (InsertOrUpdate == false)
                {
                    var product = new Product
                    {
                        //MemberId = int.Parse(txtMemberID.Text),
                        CategoryId = int.Parse(txtCategoryID.Text),
                        ProductName = txtProductName.Text,
                        Weight = txtWeight.Text,
                        UnitPrice = decimal.Parse(txtUnitPrice.Text),
                        UnitsInStock = int.Parse(txtUnitsInStock.Text)

                    };

                    ProductRepository.InsertProduct(product);
                    MessageBox.Show("Product Added");
                }
                else
                {
                    var product = new Product
                    {
                        ProductId = int.Parse(txtProductID.Text),
                        CategoryId = int.Parse(txtCategoryID.Text),
                        ProductName = txtProductName.Text,
                        Weight = txtWeight.Text,
                        UnitPrice = decimal.Parse(txtUnitPrice.Text),
                        UnitsInStock = int.Parse(txtUnitsInStock.Text)

                    };
                    ProductRepository.UpdateProduct(product);
                    MessageBox.Show("Product Updated");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new product" : "Update a product");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
