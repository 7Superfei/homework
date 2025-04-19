using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    // 商品类
    public class Product
    {
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
        public required string Name { get; set; }
        public required string Phone { get; set; }

        public override string ToString()
        {
            return $"客户: {Name}, 电话: {Phone}";
        }
    }

    // 订单明细类
    public class OrderDetails : IEquatable<OrderDetails>
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalAmount => Product.Price * Quantity;

        public override string ToString()
        {
            return $"{Product}, 数量: {Quantity}, 小计: {TotalAmount}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as OrderDetails);
        }

        public bool Equals(OrderDetails? other)
        {
            if (other is null) return false;
            return Product.Name == other.Product.Name && Quantity == other.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product.Name, Quantity);
        }
    }

    // 订单类
    public class Order : IEquatable<Order>
    {
        public int OrderId { get; set; }
        public required Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();

        public decimal TotalAmount => Details.Sum(d => d.TotalAmount);

        public override string ToString()
        {
            var detailsString = string.Join("\n", Details.Select(d => $"\t{d}"));
            return $"订单号: {OrderId}\n客户信息: {Customer}\n订单明细:\n{detailsString}\n总金额: {TotalAmount}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            if (other is null) return false;
            return OrderId == other.OrderId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId);
        }
    }

    // 订单服务类
    public class OrderService
    {
        private readonly List<Order> orders = new();

        // 添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
                throw new ArgumentException("订单已存在。");
            orders.Add(order);
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException("订单不存在。");
            orders.Remove(order);
        }

        // 修改订单
        public void UpdateOrder(int orderId, Order updatedOrder)
        {
            var index = orders.FindIndex(o => o.OrderId == orderId);
            if (index == -1)
                throw new ArgumentException("订单不存在。");
            orders[index] = updatedOrder;
        }

        // 查询订单
        public List<Order> QueryOrders(Func<Order, bool> predicate)
        {
            return orders.Where(predicate).OrderBy(o => o.TotalAmount).ToList();
        }

        // 排序订单
        public List<Order> SortOrders(Func<Order, object> keySelector)
        {
            return orders.OrderBy(keySelector).ToList();
        }

        // 显示所有订单
        public void DisplayAllOrders()
        {
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
                            int orderIdToRemove = int.Parse(Console.ReadLine() ?? "0");
                            service.RemoveOrder(orderIdToRemove);
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
                            int orderIdToUpdate = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("请输入新的客户姓名：");
                            string newCustomerName = Console.ReadLine() ?? "";

                            Console.Write("请输入新的客户电话：");
                            string newCustomerPhone = Console.ReadLine() ?? "";

                            var newCustomer = new Customer { Name = newCustomerName, Phone = newCustomerPhone };

                            Console.Write("请输入新的商品数量：");
                            int newProductCount = int.Parse(Console.ReadLine() ?? "0");

                            var newDetails = new List<OrderDetails>();
                            for (int i = 0; i < newProductCount; i++)
                            {
                                Console.Write($"请输入商品名称（{i + 1}）：");
                                string productName = Console.ReadLine() ?? "";

                                Console.Write($"请输入商品价格（{i + 1}）：");
                                decimal productPrice = decimal.Parse(Console.ReadLine() ?? "0");

                                Console.Write($"请输入商品数量（{i + 1}）：");
                                int quantity = int.Parse(Console.ReadLine() ?? "0");

                                var product = new Product { Name = productName, Price = productPrice };
                                newDetails.Add(new OrderDetails { Product = product, Quantity = quantity });
                            }

                            var updatedOrder = new Order
                            {
                                OrderId = orderIdToUpdate,
                                Customer = newCustomer,
                                Details = newDetails
                            };

                            service.UpdateOrder(orderIdToUpdate, updatedOrder);
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
                            Console.WriteLine("\n查询选项：");
                            Console.WriteLine("1. 按订单号查询");
                            Console.WriteLine("2. 按商品名称查询");
                            Console.WriteLine("3. 按客户名称查询");
                            Console.WriteLine("4. 按订单金额范围查询");
                            
                            var queryChoice = Console.ReadLine();
                            List<Order> queryResults = null;
                            
                            switch (queryChoice)
                            {
                                case "1":
                                    Console.Write("请输入订单号：");
                                    int orderId = int.Parse(Console.ReadLine() ?? "0");
                                    queryResults = service.QueryOrders(o => o.OrderId == orderId);
                                    break;
                                    
                                case "2":
                                    Console.Write("请输入商品名称：");
                                    string productName = Console.ReadLine() ?? "";
                                    queryResults = service.QueryOrders(o => 
                                        o.Details.Any(d => d.Product.Name.Contains(productName)));
                                    break;
                                    
                                case "3":
                                    Console.Write("请输入客户名称：");
                                    string customerName = Console.ReadLine() ?? "";
                                    queryResults = service.QueryOrders(o => 
                                        o.Customer.Name.Contains(customerName));
                                    break;
                                    
                                case "4":
                                    Console.Write("请输入最小金额：");
                                    decimal minAmount = decimal.Parse(Console.ReadLine() ?? "0");
                                    Console.Write("请输入最大金额：");
                                    decimal maxAmount = decimal.Parse(Console.ReadLine() ?? "0");
                                    queryResults = service.QueryOrders(o => 
                                        o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount);
                                    break;
                                    
                                default:
                                    Console.WriteLine("无效的查询选项！");
                                    break;
                            }
                            
                            if (queryResults != null)
                            {
                                if (queryResults.Count == 0)
                                {
                                    Console.WriteLine("未找到匹配的订单。");
                                }
                                else
                                {
                                    Console.WriteLine($"找到 {queryResults.Count} 个匹配的订单：");
                                    foreach (var order in queryResults)
                                    {
                                        Console.WriteLine(order);
                                        Console.WriteLine(new string('-', 40));
                                    }
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
                        Console.WriteLine("正在退出...");
                        return;

                    default:
                        Console.WriteLine("无效的选择，请重试。");
                        break;
                }
            }
        }
    }
}