using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementAPI.Models
{
    public class OrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            if (_context.Orders.Any(o => o.OrderId == order.OrderId))
                throw new ArgumentException("订单已存在。");

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException("订单不存在。");

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        // 修改订单
        public void UpdateOrder(int orderId, Order updatedOrder)
        {
            var existingOrder = _context.Orders
                .Include(o => o.Details)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (existingOrder == null)
                throw new ArgumentException("订单不存在。");

            // 更新订单基本信息
            existingOrder.CustomerId = updatedOrder.CustomerId;
            existingOrder.Customer = updatedOrder.Customer;

            // 删除旧的订单明细
            _context.OrderDetails.RemoveRange(existingOrder.Details);

            // 添加新的订单明细
            existingOrder.Details = updatedOrder.Details;

            _context.SaveChanges();
        }

        // 查询订单
        public List<Order> QueryOrders(Func<Order, bool> predicate)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .Where(predicate)
                .OrderBy(o => o.TotalAmount)
                .ToList();
        }

        // 排序订单
        public List<Order> SortOrders(Func<Order, object> keySelector)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .OrderBy(keySelector)
                .ToList();
        }
    }
} 