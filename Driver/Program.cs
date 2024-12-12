using System;
using System.Collections.Generic;
using Class_Library;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a sample order
            var order = new Order(1001, "Alice", "123-456-7890");

            // Add order details
            order.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            // Calculate 
            order.CalculateTotalAmount();
            order.CheckTariff();

            Console.WriteLine("Order Summary:");
            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"Customer Phone: {order.CustomerPhone}");
            Console.WriteLine($"Total Amount: {order.TotalAmount}");
            Console.WriteLine($"Tax Amount: {order.TaxAmount}");
            Console.WriteLine($"Tariff Amount: {order.TariffAmount}");

            Console.WriteLine("Order Details:");
            foreach (var detail in order.OrderDetails)
            {
                Console.WriteLine($"Detail Number: {detail.DetailNumber}, Stock Name: {detail.StockName}, Subtotal: {detail.SubTotal}");
            }

            // Use Factory 
            var outputDataFactory = new OutputDataFactory();
            IOrderRepository orderRepository;
    
            //show both case of Database and Json
            // 1 for SQLite output
            int outputType = 1; 
            orderRepository = outputDataFactory.CreateOutputData(outputType);
            
            if (orderRepository is SQLiteOutput sqliteOutput)
            {
                sqliteOutput.OpenConnection("database.db");
                sqliteOutput.CreateTables(); // Ensure tables are created
                sqliteOutput.SaveOrder(order);
                sqliteOutput.CloseConnection(); // Close connection after saving
            }
            
            //if database closed:
            // 2 for JSON output
            outputType = 2; 
            orderRepository = outputDataFactory.CreateOutputData(outputType);
            orderRepository.SaveOrder(order);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}