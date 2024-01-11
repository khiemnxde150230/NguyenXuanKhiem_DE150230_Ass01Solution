using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int orderId)
        {
            OrderDetailDAO.Instance.Remove(orderId);
        }

        public OrderDetail GetOrderDetailByID(int orderId)
        {
            return OrderDetailDAO.Instance.GetOrderDetailByID(orderId);
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return OrderDetailDAO.Instance.GetOrderDetailList();
        }

        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.Instance.AddNew(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.Instance.Update(orderDetail);
        }
    }
}
