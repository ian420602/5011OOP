using System;
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

                // Calculate total amount, including tax and tariffs
                order.CalculateTotalAmount();
                order.CheckTariff();

                Console.WriteLine("Order Summary:");
                Console.WriteLine($"Order Number: {order.OrderNumber}");
                Console.WriteLine($"Customer Name: {order.CustomerName}");
                Console.WriteLine($"Customer Phone: {order.CustomerPhone}");
                Console.WriteLine($"Total Amount: {order.TotalAmount}");
                Console.WriteLine($"Tax Amount: {order.TaxAmount}");

                Console.WriteLine("Order Details:");
                foreach (var detail in order.OrderDetails)
                {
                    Console.WriteLine($"Detail Number: {detail.DetailNumber}, Stock Name: {detail.StockName}, Subtotal: {detail.SubTotal}");
                }

                // Save to JSON
                var jsonOutput = new JsonOutput();
                string jsonFilePath = "OrderOutput.json";
                jsonOutput.SaveOrderToFile(order, jsonFilePath);
                Console.WriteLine($"Order saved to JSON: {jsonFilePath}");

                // Save to SQLite
                var sqliteOutput = new SQLiteOutput();
                string databasePath = "OrderDatabase.db";
                sqliteOutput.OpenConnection(databasePath);
                sqliteOutput.CreateTables();
                sqliteOutput.InsertOrder(order);
                sqliteOutput.CloseConnection();
                Console.WriteLine($"Order saved to SQLite database: {databasePath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

