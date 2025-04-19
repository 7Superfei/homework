using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderManagement.Tests
{
    [TestClass]
    public class OrderTests
    {
        private OrderService? service;
        private Order? testOrder;

        [TestInitialize]
        public void Initialize()
        {
            service = new OrderService();
            
            // 创建测试订单
            var customer = new Customer { Name = "测试客户", Phone = "1234567890" };
            var product = new Product { Name = "测试商品", Price = 100m };
            var details = new List<OrderDetails>
            {
                new OrderDetails { Product = product, Quantity = 2 }
            };
            
            testOrder = new Order
            {
                OrderId = 1,
                Customer = customer,
                Details = details
            };
        }

        [TestMethod]
        public void TestAddOrder()
        {
            service!.AddOrder(testOrder!);
            var orders = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(testOrder, orders[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicateOrder()
        {
            service!.AddOrder(testOrder!);
            service.AddOrder(testOrder); // 应该抛出异常
        }

        [TestMethod]
        public void TestRemoveOrder()
        {
            service!.AddOrder(testOrder!);
            service.RemoveOrder(1);
            var orders = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveNonExistentOrder()
        {
            service!.RemoveOrder(999); // 应该抛出异常
        }

        [TestMethod]
        public void TestUpdateOrder()
        {
            service!.AddOrder(testOrder!);
            
            var updatedCustomer = new Customer { Name = "新客户", Phone = "0987654321" };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Customer = updatedCustomer,
                Details = testOrder.Details
            };
            
            service.UpdateOrder(1, updatedOrder);
            var orders = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual("新客户", orders[0].Customer.Name);
        }

        [TestMethod]
        public void TestQueryByOrderId()
        {
            service!.AddOrder(testOrder!);
            var orders = service.QueryOrders(o => o.OrderId == 1);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(testOrder, orders[0]);
        }

        [TestMethod]
        public void TestQueryByCustomerName()
        {
            service!.AddOrder(testOrder!);
            var orders = service.QueryOrders(o => o.Customer.Name.Contains("测试"));
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(testOrder, orders[0]);
        }

        [TestMethod]
        public void TestQueryByProductName()
        {
            service!.AddOrder(testOrder!);
            var orders = service.QueryOrders(o => o.Details.Any(d => d.Product.Name.Contains("测试")));
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(testOrder, orders[0]);
        }

        [TestMethod]
        public void TestSortByOrderId()
        {
            var order2 = new Order
            {
                OrderId = 2,
                Customer = testOrder!.Customer,
                Details = testOrder.Details
            };
            
            service!.AddOrder(order2);
            service.AddOrder(testOrder);
            
            var sortedOrders = service.SortOrders(o => o.OrderId);
            Assert.AreEqual(1, sortedOrders[0].OrderId);
            Assert.AreEqual(2, sortedOrders[1].OrderId);
        }

        [TestMethod]
        public void TestSortByTotalAmount()
        {
            var order2 = new Order
            {
                OrderId = 2,
                Customer = testOrder!.Customer,
                Details = new List<OrderDetails>
                {
                    new OrderDetails
                    {
                        Product = new Product { Name = "贵商品", Price = 200m },
                        Quantity = 1
                    }
                }
            };
            
            service!.AddOrder(order2);
            service.AddOrder(testOrder);
            
            var sortedOrders = service.SortOrders(o => o.TotalAmount);
            Assert.AreEqual(200m, sortedOrders[0].TotalAmount);
            Assert.AreEqual(300m, sortedOrders[1].TotalAmount);
        }
    }
} 