using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementAPI.Models
{
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
} 