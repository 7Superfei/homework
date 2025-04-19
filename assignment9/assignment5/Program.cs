using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement
{
    // 商品类
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"商品: {Name}, 价格: {Price}";
        }
    }

    // 客户类
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }

        public override string ToString()
        {
            return $"客户: {Name}, 电话: {Phone}";
        }
    }

    // 订单明细类
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public required Product Product { get; set; }

        public decimal TotalAmount => Product.Price * Quantity;

        public override string ToString()
        {
            return $"{Product}, 数量: {Quantity}, 小计: {TotalAmount}";
        }
    }

    // 订单类
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public required Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();

        public decimal TotalAmount => Details.Sum(d => d.TotalAmount);

        public override string ToString()
        {
            var detailsString = string.Join("\n", Details.Select(d => $"\t{d}"));
            return $"订单号: {OrderId}\n客户信息: {Customer}\n订单明细:\n{detailsString}\n总金额: {TotalAmount}";
        }
    }

    // 订单服务类
    public class OrderService
    {
        private readonly OrderDbContext _context;

        public OrderService()
        {
            _context = new OrderDbContext();
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

        // 显示所有订单
        public void DisplayAllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .ToList();

            if (orders.Count == 0)
            {
                Console.WriteLine("暂无订单。");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order);
                Console.WriteLine(new string('-', 40));
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("订单管理系统启动...");

            var service = new OrderService();
            while (true)
            {
                Console.WriteLine("\n请选择操作：");
                Console.WriteLine("1. 添加订单");
                Console.WriteLine("2. 删除订单");
                Console.WriteLine("3. 修改订单");
                Console.WriteLine("4. 查询订单");
                Console.WriteLine("5. 显示所有订单");
                Console.WriteLine("6. 退出");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("请输入订单号：");
                            int orderId = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("请输入客户姓名：");
                            string customerName = Console.ReadLine() ?? "";

                            Console.Write("请输入客户电话：");
                            string customerPhone = Console.ReadLine() ?? "";

                            var customer = new Customer { Name = customerName, Phone = customerPhone };

                            Console.Write("请输入订单中的商品数量：");
                            int productCount = int.Parse(Console.ReadLine() ?? "0");

                            var details = new List<OrderDetails>();
                            for (int i = 0; i < productCount; i++)
                            {
                                Console.Write($"请输入商品名称（{i + 1}）：");
                                string productName = Console.ReadLine() ?? "";

                                Console.Write($"请输入商品价格（{i + 1}）：");
                                decimal productPrice = decimal.Parse(Console.ReadLine() ?? "0");

                                Console.Write($"请输入商品数量（{i + 1}）：");
                                int quantity = int.Parse(Console.ReadLine() ?? "0");

                                var product = new Product { Name = productName, Price = productPrice };
                                details.Add(new OrderDetails { Product = product, Quantity = quantity });
                            }

                            var order = new Order
                            {
                                OrderId = orderId,
                                Customer = customer,
                                Details = details
                            };

                            service.AddOrder(order);
                            Console.WriteLine("订单添加成功。");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"错误：{ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.Write("请输入要删除的订单号：");
                            int orderId = int.Parse(Console.ReadLine() ?? "0");
                            service.RemoveOrder(orderId);
                            Console.WriteLine("订单删除成功。");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"错误：{ex.Message}");
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.Write("请输入要修改的订单号：");
                            int orderId = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("请输入新的客户姓名：");
                            string customerName = Console.ReadLine() ?? "";

                            Console.Write("请输入新的客户电话：");
                            string customerPhone = Console.ReadLine() ?? "";

                            var customer = new Customer { Name = customerName, Phone = customerPhone };

                            Console.Write("请输入新的商品数量：");
                            int productCount = int.Parse(Console.ReadLine() ?? "0");

                            var details = new List<OrderDetails>();
                            for (int i = 0; i < productCount; i++)
                            {
                                Console.Write($"请输入商品名称（{i + 1}）：");
                                string productName = Console.ReadLine() ?? "";

                                Console.Write($"请输入商品价格（{i + 1}）：");
                                decimal productPrice = decimal.Parse(Console.ReadLine() ?? "0");

                                Console.Write($"请输入商品数量（{i + 1}）：");
                                int quantity = int.Parse(Console.ReadLine() ?? "0");

                                var product = new Product { Name = productName, Price = productPrice };
                                details.Add(new OrderDetails { Product = product, Quantity = quantity });
                            }

                            var updatedOrder = new Order
                            {
                                OrderId = orderId,
                                Customer = customer,
                                Details = details
                            };

                            service.UpdateOrder(orderId, updatedOrder);
                            Console.WriteLine("订单修改成功。");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"错误：{ex.Message}");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.WriteLine("请选择查询方式：");
                            Console.WriteLine("1. 按订单号查询");
                            Console.WriteLine("2. 按客户姓名查询");
                            Console.WriteLine("3. 按商品名称查询");

                            var searchChoice = Console.ReadLine();
                            List<Order> results = new();

                            switch (searchChoice)
                            {
                                case "1":
                                    Console.Write("请输入订单号：");
                                    int orderId = int.Parse(Console.ReadLine() ?? "0");
                                    results = service.QueryOrders(o => o.OrderId == orderId);
                                    break;

                                case "2":
                                    Console.Write("请输入客户姓名：");
                                    string customerName = Console.ReadLine() ?? "";
                                    results = service.QueryOrders(o => o.Customer.Name.Contains(customerName));
                                    break;

                                case "3":
                                    Console.Write("请输入商品名称：");
                                    string productName = Console.ReadLine() ?? "";
                                    results = service.QueryOrders(o => o.Details.Any(d => d.Product.Name.Contains(productName)));
                                    break;

                                default:
                                    Console.WriteLine("无效的选择。");
                                    continue;
                            }

                            if (results.Count == 0)
                            {
                                Console.WriteLine("未找到匹配的订单。");
                            }
                            else
                            {
                                foreach (var order in results)
                                {
                                    Console.WriteLine(order);
                                    Console.WriteLine(new string('-', 40));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"错误：{ex.Message}");
                        }
                        break;

                    case "5":
                        service.DisplayAllOrders();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("无效的选择，请重试。");
                        break;
                }
            }
        }
    }
}