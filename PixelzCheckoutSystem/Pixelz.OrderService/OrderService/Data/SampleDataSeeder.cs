using OrderService.Models;
using Pixelz.Models.Constants;

namespace OrderService.Data
{
    public class SampleDataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

            if (!context.Customers.Any())
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "John Doe",
                    Email = "john@example.com",
                    Phone = "123456789"
                };
                var orders = new List<Order>();


                // CASE 1: OrderStatus = Pending
                orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    Customer = customer,
                    Name = "ORD1",
                    Status = OrderStatus.Pending,
                    TotalAmount = 150,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Product A", Quantity = 1 },
                        new OrderItem { ProductName = "Product B", Quantity = 2 }
                    }
                });


                // CASE 2: TotalAmount % 2 != 0
                orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    Customer = customer,
                    Name = "ORD2",
                    Status = OrderStatus.Pending,
                    TotalAmount = 199, 
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Odd Priced Product", Quantity = 3 }
                    }
                });

                // CASE 3: OrderStatus != Pending
                orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    Customer = customer,
                    Name = "ORD3",
                    Status = OrderStatus.Completed,
                    TotalAmount = 200,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Odd Priced Product", Quantity = 3 }
                    }
                });

                // CASE 4: Total quantity > 100
                orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    Customer = customer,
                    Name = "ORD4",
                    Status = OrderStatus.Pending,
                    TotalAmount = 1000,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Bulk Item", Quantity = 101 }
                    }
                });

                context.Customers.Add(customer);
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
