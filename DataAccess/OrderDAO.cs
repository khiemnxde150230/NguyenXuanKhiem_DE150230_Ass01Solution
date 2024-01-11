using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        private FstoreContext dbContext;

        public OrderDAO(FstoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public OrderDAO()
        : this(new FstoreContext())
        {
        }

        public IEnumerable<Order> GetOrderList()
        {
            return dbContext.Orders.ToList();
        }

        public Order GetOrderByID(int orderId)
        {
            return dbContext.Orders.SingleOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrderByMemberID(int memberId)
        {
            return dbContext.Orders.Where(o => o.MemberId == memberId).ToList();
        }

        public void AddNew(Order order)
        {
            if (GetOrderByID(order.OrderId) == null)
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The order is already exist.");
            }
        }

        public void Update(Order order)
        {
            var existingOrder = GetOrderByID(order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.MemberId = order.MemberId;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.ShippedDate = order.ShippedDate;
                existingOrder.Freight = order.Freight;
                existingOrder.RequiredDate = order.RequiredDate;

                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The order does not already exist.");
            }
        }

        public void Remove(int orderId)
        {
            var order = GetOrderByID(orderId);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The order does not already exist.");
            }
        }
    }
}
