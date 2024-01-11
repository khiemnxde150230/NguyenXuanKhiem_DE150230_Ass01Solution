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
    public partial class WindowOrders : Window
    {
        IOrderRepository orderRepository = new OrderRepository();
        public ObservableCollection<Order> Orders { get; set; }
        public WindowOrders()
        {
            InitializeComponent();
            Orders = new ObservableCollection<Order>();
            dgvOrderList.ItemsSource = Orders;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
        }

        public void LoadOrderList()
        {
            try
            {
                var orders = orderRepository.GetOrders();

                Orders.Clear();

                foreach (var order in orders)
                {
                    Orders.Add(order);
                }

                if (orders.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load order list");
            }
        }

        private void dgvOrderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a order");
                return;
            }

            Order selectedOrder = (Order)dataGrid.SelectedItem;
            int orderId = selectedOrder.OrderId;

            Order orderInfo = orderRepository.GetOrderById(orderId);

            if (orderInfo != null)
            {
                WindowOrdersDetail windowOrdersDetail = new WindowOrdersDetail();

                windowOrdersDetail.OrderRepository = orderRepository;
                windowOrdersDetail.OrderInfo = orderInfo;
                windowOrdersDetail.InsertOrUpdate = true;
                windowOrdersDetail.Show();
            }
            else
            {
                MessageBox.Show("Invalid order");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WindowOrdersDetail windowOrdersDetail = new WindowOrdersDetail();

            windowOrdersDetail.OrderRepository = orderRepository;
            windowOrdersDetail.InsertOrUpdate = false;
            windowOrdersDetail.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = (Order)dgvOrderList.SelectedItem;
                orderRepository.DeleteOrder(order.OrderId);
                LoadOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete order");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
