using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int orderId)
        {
            OrderDAO.Instance.Remove(orderId);
        }

        public Order GetOrderById(int orderId)
        {
            return OrderDAO.Instance.GetOrderByID(orderId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return OrderDAO.Instance.GetOrderList();
        }

        public void InsertOrder(Order order)
        {
            OrderDAO.Instance.AddNew(order);
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.Instance.Update(order);
        }

        public List<Order> GetOrderByMemberID(int memberId)
        {
            return OrderDAO.Instance.GetOrderByMemberID(memberId);
        }
    }
}
