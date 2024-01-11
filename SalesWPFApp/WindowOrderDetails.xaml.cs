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
    public partial class WindowOrderDetails : Window
    {
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        public ObservableCollection<OrderDetail> OrderDetails { get; set; }
        public WindowOrderDetails()
        {
            InitializeComponent();
            OrderDetails = new ObservableCollection<OrderDetail>();
            dgvOrderDetailList.ItemsSource = OrderDetails;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
        }

        public void LoadOrderDetailList()
        {
            try
            {
                var orderdetails = orderDetailRepository.GetOrderDetails();

                OrderDetails.Clear();

                foreach (var orderdetail in orderdetails)
                {
                    OrderDetails.Add(orderdetail);
                }

                if (orderdetails.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load order detail list");
            }
        }

        private void dgvOrderDetailList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a order detail");
                return;
            }

            OrderDetail selectedOrderDetail = (OrderDetail)dataGrid.SelectedItem;
            int orderId = selectedOrderDetail.OrderId;

            OrderDetail orderDetailInfo = orderDetailRepository.GetOrderDetailByID(orderId);

            if (orderDetailInfo != null)
            {
                WindowOrderDetailsDetail windowOrderDetailsDetail = new WindowOrderDetailsDetail();

                windowOrderDetailsDetail.OrderDetailRepository = orderDetailRepository;
                windowOrderDetailsDetail.OrderDetailInfo = orderDetailInfo;
                windowOrderDetailsDetail.InsertOrUpdate = true;
                windowOrderDetailsDetail.Show();
            }
            else
            {
                MessageBox.Show("Invalid order detail");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderDetailList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WindowOrderDetailsDetail windowOrderDetailsDetail = new WindowOrderDetailsDetail();

            windowOrderDetailsDetail.OrderDetailRepository = orderDetailRepository;
            windowOrderDetailsDetail.InsertOrUpdate = false;
            windowOrderDetailsDetail.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderDetail orderdetail = (OrderDetail)dgvOrderDetailList.SelectedItem;
                orderDetailRepository.DeleteOrderDetail(orderdetail.OrderId);
                LoadOrderDetailList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete order detail");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
