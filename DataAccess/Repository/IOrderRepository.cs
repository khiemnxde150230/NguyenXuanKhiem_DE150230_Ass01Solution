﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int orderId);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        List<Order> GetOrderByMemberID(int memberId);

    }
}
