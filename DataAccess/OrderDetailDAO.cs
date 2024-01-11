using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        private FstoreContext dbContext;

        public OrderDetailDAO(FstoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public OrderDetailDAO()
        : this(new FstoreContext())
        {
        }

        public IEnumerable<OrderDetail> GetOrderDetailList()
        {
            return dbContext.OrderDetails.ToList();
        }

        public OrderDetail GetOrderDetailByID(int orderId)
        {
            return dbContext.OrderDetails.SingleOrDefault(od => od.OrderId == orderId);
        }

        public void AddNew(OrderDetail orderDetail)
        {
            if (GetOrderDetailByID(orderDetail.OrderId) == null)
            {
                dbContext.OrderDetails.Add(orderDetail);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The orderDetail is already exist.");
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            var existingOrderDetail = GetOrderDetailByID(orderDetail.OrderId);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.ProductId = orderDetail.ProductId;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Discount = orderDetail.Discount;

                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The orderDetail does not already exist.");
            }
        }

        public void Remove(int orderId)
        {
            var orderDetail = GetOrderDetailByID(orderId);
            if (orderDetail != null)
            {
                dbContext.OrderDetails.Remove(orderDetail);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The orderDetail does not already exist.");
            }
        }
    }

}
