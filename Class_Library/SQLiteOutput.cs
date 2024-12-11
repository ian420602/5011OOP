namespace Class_Library
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.Sqlite; //  Microsoft.Data.Sqlite 
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SQLiteOutput
    {
        private SqliteConnection sqlite_conn;

        // Open SQLite connection
        public void OpenConnection(string databaseName)
        {
            try
            {
                sqlite_conn = new SqliteConnection($"Data Source={databaseName};");
                sqlite_conn.Open();
                Console.WriteLine("SQLite connection opened.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening SQLite connection: {ex.Message}");
            }
        }

        // Close SQLite connection
        public void CloseConnection()
        {
            try
            {
                sqlite_conn?.Close();
                Console.WriteLine("SQLite connection closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing SQLite connection: {ex.Message}");
            }
        }

        // Create tables for Orders and OrderDetails
        public void CreateTables()
        {
            try
            {
                string createOrderTable = @"
                    CREATE TABLE IF NOT EXISTS Orders (
                        OrderNumber INTEGER PRIMARY KEY,
                        CustomerName TEXT NOT NULL,
                        CustomerPhone TEXT NOT NULL,
                        DateTime TEXT NOT NULL,
                        TotalAmount REAL,
                        TaxAmount REAL,
                        TariffAmount REAL
                    );";

                string createOrderDetailTable = @"
                    CREATE TABLE IF NOT EXISTS OrderDetails (
                        DetailNumber INTEGER PRIMARY KEY,
                        OrderNumber INTEGER NOT NULL,
                        StockID TEXT NOT NULL,
                        StockName TEXT,
                        StockPrice REAL,
                        Quantity INTEGER,
                        SubTotal REAL,
                        FOREIGN KEY(OrderNumber) REFERENCES Orders(OrderNumber)
                    );";

                ExecuteNonQuery(createOrderTable);
                ExecuteNonQuery(createOrderDetailTable);
                Console.WriteLine("Tables created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating tables: {ex.Message}");
            }
        }

        // Insert an Order into the Orders table
        public void InsertOrder(Order order)
        {
            try
            {
                string insertOrderSql = @"
                    INSERT INTO Orders (OrderNumber, CustomerName, CustomerPhone, DateTime, TotalAmount, TaxAmount, TariffAmount)
                    VALUES (@OrderNumber, @CustomerName, @CustomerPhone, @DateTime, @TotalAmount, @TaxAmount, @TariffAmount);";

                using (var cmd = sqlite_conn.CreateCommand())
                {
                    cmd.CommandText = insertOrderSql;
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerPhone", order.CustomerPhone);
                    cmd.Parameters.AddWithValue("@DateTime", order.DateTime.ToString("o")); // ISO format
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@TaxAmount", order.TaxAmount);
                    cmd.Parameters.AddWithValue("@TariffAmount", order.TariffAmount);
                    cmd.ExecuteNonQuery();
                }

                // Insert each OrderDetail
                foreach (var detail in order.OrderDetails)
                {
                    InsertOrderDetail(detail, order.OrderNumber);
                }

                Console.WriteLine("Order inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting order: {ex.Message}");
            }
        }

        // Insert an OrderDetail into the OrderDetails table
        private void InsertOrderDetail(OrderDetail detail, int orderNumber)
        {
            try
            {
                string insertDetailSql = @"
                    INSERT INTO OrderDetails (DetailNumber, OrderNumber, StockID, StockName, StockPrice, Quantity, SubTotal)
                    VALUES (@DetailNumber, @OrderNumber, @StockID, @StockName, @StockPrice, @Quantity, @SubTotal);";

                using (var cmd = sqlite_conn.CreateCommand())
                {
                    cmd.CommandText = insertDetailSql;
                    cmd.Parameters.AddWithValue("@DetailNumber", detail.DetailNumber);
                    cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                    cmd.Parameters.AddWithValue("@StockID", detail.StockID);
                    cmd.Parameters.AddWithValue("@StockName", detail.StockName);
                    cmd.Parameters.AddWithValue("@StockPrice", detail.StockPrice);
                    cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                    cmd.Parameters.AddWithValue("@SubTotal", detail.CalculateSubTotal());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting order detail: {ex.Message}");
            }
        }

        // Helper method to execute non-query SQL commands
        private void ExecuteNonQuery(string sql)
        {
            using (var cmd = sqlite_conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
