namespace Class_Library;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



public class JsonOutput
{
    // Save an Order to a JSON file
    public void SaveOrderToFile(Order order, string filePath)
    {
        try
        {
            // Serialize the Order object to JSON
            string json = JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                WriteIndented = true // Pretty-print JSON
            });

            // Write JSON to the specified file
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Order {order.OrderNumber} saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving order to JSON: {ex.Message}");
        }
    }

    // Read an Order from a JSON file
    public Order ReadOrderFromFile(string filePath)
    {
        try
        {
            // Read the JSON content from the specified file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string back into an Order object
            Order order = JsonSerializer.Deserialize<Order>(json);
            
            if (order == null)
                throw new InvalidOperationException("Deserialization resulted in a null Order object.");
            
            Console.WriteLine($"Order {order.OrderNumber} read from {filePath}");
            return order;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading order from JSON: {ex.Message}");
            throw;
        }
    }

    // Save multiple Orders to a JSON file
    public void SaveOrdersToFile(IEnumerable<Order> orders, string filePath)
    {
        try
        {
            // Serialize the list of Orders to JSON
            string json = JsonSerializer.Serialize(orders, new JsonSerializerOptions
            {
                WriteIndented = true // Pretty-print JSON
            });

            // Write JSON to the specified file
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Orders saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving orders to JSON: {ex.Message}");
        }
    }

    // Read multiple Orders from a JSON file
    public List<Order> ReadOrdersFromFile(string filePath)
    {
        try
        {
            // Read the JSON content from the specified file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string back into a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(json);

            if (orders == null)
                throw new InvalidOperationException("Deserialization resulted in a null list of Orders.");

            Console.WriteLine($"Orders read from {filePath}");
            return orders;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading orders from JSON: {ex.Message}");
            throw;
        }
    }
}
