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
    public partial class WindowOrderDetailsDetail : Window
    {
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public OrderDetail OrderDetailInfo { get; set; }

        public WindowOrderDetailsDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (InsertOrUpdate == true)
            {
                txtOrderID.Text = OrderDetailInfo.OrderId.ToString();
                txtProductID.Text = OrderDetailInfo.ProductId.ToString();
                txtUnitPrice.Text = OrderDetailInfo.UnitPrice.ToString();
                txtQuantity.Text = OrderDetailInfo.Quantity.ToString();
                txtDiscount.Text = OrderDetailInfo.Discount.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (InsertOrUpdate == false)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = int.Parse(txtOrderID.Text),
                        ProductId = int.Parse(txtProductID.Text),
                        UnitPrice = decimal.Parse(txtUnitPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Discount = float.Parse(txtDiscount.Text)

                    };

                    OrderDetailRepository.InsertOrderDetail(orderDetail);
                    MessageBox.Show("Order Detail Added");
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = int.Parse(txtOrderID.Text),
                        ProductId = int.Parse(txtProductID.Text),
                        UnitPrice = decimal.Parse(txtUnitPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Discount = float.Parse(txtDiscount.Text)

                    };
                    OrderDetailRepository.UpdateOrderDetail(orderDetail);
                    MessageBox.Show("Order Detail Updated");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new order detail" : "Update a order detail");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
