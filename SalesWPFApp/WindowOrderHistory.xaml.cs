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
    /// Interaction logic for WindowOrderHistory.xaml
    /// </summary>
    public partial class WindowOrderHistory : Window
    {
        private int memberId;
        private IOrderRepository orderRepository = new OrderRepository();
        public ObservableCollection<Order> Orders { get; set; }
        public WindowOrderHistory(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;
            Orders = new ObservableCollection<Order>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = orderRepository.GetOrderByMemberID(memberId);
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }

                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order list");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
