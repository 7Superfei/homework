using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    // 商品类
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Product: {Name}, Price: {Price}";
        }
    }

    // 客户类
    public class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Customer: {Name}, Phone: {Phone}";
        }
    }

    // 订单明细类
    public class OrderDetails : IEquatable<OrderDetails>
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalAmount => Product.Price * Quantity;

        public override string ToString()
        {
            return $"{Product}, Quantity: {Quantity}, Total: {TotalAmount}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderDetails);
        }

        public bool Equals(OrderDetails other)
        {
            if (other == null) return false;
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
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();

        public decimal TotalAmount => Details.Sum(d => d.TotalAmount);

        public override string ToString()
        {
            var detailsString = string.Join("\n", Details.Select(d => $"\t{d}"));
            return $"Order ID: {OrderId}\nCustomer: {Customer}\nDetails:\n{detailsString}\nTotal Amount: {TotalAmount}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order other)
        {
            if (other == null) return false;
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
        private List<Order> orders = new List<Order>();

        // 添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
                throw new ArgumentException("Order already exists.");
            orders.Add(order);
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException("Order not found.");
            orders.Remove(order);
        }

        // 修改订单
        public void UpdateOrder(int orderId, Order updatedOrder)
        {
            var index = orders.FindIndex(o => o.OrderId == orderId);
            if (index == -1)
                throw new ArgumentException("Order not found.");
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
                Console.WriteLine("No orders available.");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine(order);
                Console.WriteLine(new string('-', 40));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Order Management System...");

            var service = new OrderService();
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. Remove Order");
                Console.WriteLine("3. Update Order");
                Console.WriteLine("4. Query Orders");
                Console.WriteLine("5. Display All Orders");
                Console.WriteLine("6. Exit");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Order ID: ");
                            int orderId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Customer Name: ");
                            string customerName = Console.ReadLine();

                            Console.Write("Enter Customer Phone: ");
                            string customerPhone = Console.ReadLine();

                            var customer = new Customer { Name = customerName, Phone = customerPhone };

                            Console.Write("Enter number of products in the order: ");
                            int productCount = int.Parse(Console.ReadLine());

                            var details = new List<OrderDetails>();
                            for (int i = 0; i < productCount; i++)
                            {
                                Console.Write($"Enter Product Name ({i + 1}): ");
                                string productName = Console.ReadLine();

                                Console.Write($"Enter Product Price ({i + 1}): ");
                                decimal productPrice = decimal.Parse(Console.ReadLine());

                                Console.Write($"Enter Product Quantity ({i + 1}): ");
                                int quantity = int.Parse(Console.ReadLine());

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
                            Console.WriteLine("Order added successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.Write("Enter Order ID to remove: ");
                            int orderIdToRemove = int.Parse(Console.ReadLine());
                            service.RemoveOrder(orderIdToRemove);
                            Console.WriteLine("Order removed successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.Write("Enter Order ID to update: ");
                            int orderIdToUpdate = int.Parse(Console.ReadLine());

                            Console.Write("Enter new Customer Name: ");
                            string newCustomerName = Console.ReadLine();

                            Console.Write("Enter new Customer Phone: ");
                            string newCustomerPhone = Console.ReadLine();

                            var newCustomer = new Customer { Name = newCustomerName, Phone = newCustomerPhone };

                            Console.Write("Enter number of products in the updated order: ");
                            int newProductCount = int.Parse(Console.ReadLine());

                            var newDetails = new List<OrderDetails>();
                            for (int i = 0; i < newProductCount; i++)
                            {
                                Console.Write($"Enter Product Name ({i + 1}): ");
                                string productName = Console.ReadLine();

                                Console.Write($"Enter Product Price ({i + 1}): ");
                                decimal productPrice = decimal.Parse(Console.ReadLine());

                                Console.Write($"Enter Product Quantity ({i + 1}): ");
                                int quantity = int.Parse(Console.ReadLine());

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
                            Console.WriteLine("Order updated successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.WriteLine("Query options:");
                            Console.WriteLine("1. By Order ID");
                            Console.WriteLine("2. By Customer Name");
                            Console.WriteLine("3. By Product Name");
                            Console.WriteLine("4. By Total Amount");

                            var queryChoice = Console.ReadLine();
                            List<Order> result = null;

                            switch (queryChoice)
                            {
                                case "1":
                                    Console.Write("Enter Order ID: ");
                                    int queryOrderId = int.Parse(Console.ReadLine());
                                    result = service.QueryOrders(o => o.OrderId == queryOrderId);
                                    break;

                                case "2":
                                    Console.Write("Enter Customer Name: ");
                                    string queryCustomerName = Console.ReadLine();
                                    result = service.QueryOrders(o => o.Customer.Name.Contains(queryCustomerName));
                                    break;

                                case "3":
                                    Console.Write("Enter Product Name: ");
                                    string queryProductName = Console.ReadLine();
                                    result = service.QueryOrders(o => o.Details.Any(d => d.Product.Name.Contains(queryProductName)));
                                    break;

                                case "4":
                                    Console.Write("Enter Minimum Total Amount: ");
                                    decimal minAmount = decimal.Parse(Console.ReadLine());
                                    result = service.QueryOrders(o => o.TotalAmount >= minAmount);
                                    break;

                                default:
                                    Console.WriteLine("Invalid query option.");
                                    break;
                            }

                            if (result != null && result.Count > 0)
                            {
                                Console.WriteLine("Query Results:");
                                foreach (var order in result)
                                {
                                    Console.WriteLine(order);
                                    Console.WriteLine(new string('-', 40));
                                }
                            }
                            else
                            {
                                Console.WriteLine("No matching orders found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "5":
                        service.DisplayAllOrders();
                        break;

                    case "6":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}