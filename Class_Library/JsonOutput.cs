namespace Class_Library;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

    public class JsonOutput : IOrderRepository
    {
        private readonly string jsonFilePath = "orders.json";

        public void SaveOrder(Order order)
        {
            try
            {
                var orderJson = JsonConvert.SerializeObject(order, Formatting.Indented);
                File.WriteAllText(jsonFilePath, orderJson);
                Console.WriteLine("Order saved to JSON file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving order to JSON: {ex.Message}");
            }
        }

        public Order LoadOrder(int orderNumber)
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    var ordersJson = File.ReadAllText(jsonFilePath);
                    var order = JsonConvert.DeserializeObject<Order>(ordersJson);
                    Console.WriteLine("Order loaded from JSON file.");
                    return order?.OrderNumber == orderNumber ? order : null;
                }

                Console.WriteLine("JSON file not found.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading order from JSON: {ex.Message}");
                return null;
            }
        }
    }

