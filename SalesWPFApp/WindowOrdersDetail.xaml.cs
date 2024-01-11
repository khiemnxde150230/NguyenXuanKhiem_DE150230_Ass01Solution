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
    public partial class WindowOrdersDetail : Window
    {
        public IOrderRepository OrderRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Order OrderInfo { get; set; }

        public WindowOrdersDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (InsertOrUpdate == true)
            {
                txtOrderID.Text = OrderInfo.OrderId.ToString();
                txtMemberID.Text = OrderInfo.MemberId.ToString();
                txtFreight.Text = OrderInfo.Freight.ToString();
                dtpOrderDate.SelectedDate = OrderInfo.OrderDate;
                dtpRequiredDate.SelectedDate = OrderInfo.RequiredDate;
                dtpShippedDate.SelectedDate = OrderInfo.ShippedDate;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (InsertOrUpdate == false)
                {
                    var order = new Order
                    {
                        //OrderId = int.Parse(txtOrderID.Text),
                        MemberId = int.Parse(txtMemberID.Text),
                        OrderDate = DateTime.Parse(dtpOrderDate.Text),
                        ShippedDate = DateTime.Parse(dtpShippedDate.Text),
                        Freight = decimal.Parse(txtFreight.Text),
                        RequiredDate = DateTime.Parse(dtpRequiredDate.Text)

                    };

                    OrderRepository.InsertOrder(order);
                    MessageBox.Show("Order Added");
                }
                else
                {
                    var order = new Order
                    {
                        OrderId = int.Parse(txtOrderID.Text),
                        MemberId = int.Parse(txtMemberID.Text),
                        OrderDate = DateTime.Parse(dtpOrderDate.Text),
                        ShippedDate = DateTime.Parse(dtpShippedDate.Text),
                        Freight = decimal.Parse(txtFreight.Text),
                        RequiredDate = DateTime.Parse(dtpRequiredDate.Text)

                    };
                    OrderRepository.UpdateOrder(order);
                    MessageBox.Show("Order Updated");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new order" : "Update a order");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
