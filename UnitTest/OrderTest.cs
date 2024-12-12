namespace UnitTest;
using System;
using System.Collections.Generic;
using System.IO;
using Class_Library;
using Xunit;

public class OrderTests {
        [Fact] 
        public void TestOrderCreation_ValidInputs()
        {
            var order = new Order(1000, "John Jenkins", "253-312-4578");
            Assert.Equal(1000, order.OrderNumber);
            Assert.Equal("John Jenkins", order.CustomerName);
            Assert.Equal("253-312-4578", order.CustomerPhone);
        }

        [Fact]
        public void TestOrderWithDetails()
        {
            var order = new Order(1000, "John", "200-300-4000");
            var details = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            order.OrderDetails = details;
            Assert.Equal(2, order.OrderDetails.Count);
            Assert.Equal("TV", order.OrderDetails[0].StockName);
        }

        [Fact]
        public void TestCalculateOrderTotalAmount()
        {
            var order = new Order(1000, "John", "200-300-4000");
            order.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            order.CalculateTotalAmount();
            Assert.Equal(400.00f, order.TotalAmount); // Total without tax or tariff
        }

        [Fact]
        public void TestOrderTaxAndTariffCalculation()
        {
            var order = new Order(1000, "John", "200-300-4000");
            order.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            order.CalculateTotalAmount();
            order.CheckTariff();
            Assert.Equal(315.00f, order.OrderDetails[0].SubTotal); // Tariff applied
            Assert.Equal(31.50f, order.TaxAmount); // Tax applied on total
        }

        [Fact]
        public void TestOrderJsonOutput()
        {
            var order = new Order(1000, "John", "200-300-4000");
            order.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            var jsonOutput = new JsonOutput();
            jsonOutput.SaveOrderToFile(order, "TestOrder.json");
            Assert.True(File.Exists("TestOrder.json"));
        }

        [Fact]
        public void TestOrderSQLiteOutput()
        {
            var order = new Order(1000, "John", "200-300-4000");
            order.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, "ELECT001", "42 Inch TV", 300.00f, 1),
                new OrderDetail(2, "NOELE001", "Non-Electronic Item", 50.00f, 2)
            };

            var sqliteOutput = new SQLiteOutput();
            sqliteOutput.OpenConnection("TestOrders.db");
            sqliteOutput.CreateTables();
            sqliteOutput.InsertOrder(order);
            sqliteOutput.CloseConnection();
            Assert.True(File.Exists("TestOrders.db"));
        }

        [Fact]
        public void TestInvalidPhoneNumber()
        {
            Assert.Throws<ArgumentException>(() => new Order(1000, "John", "INVALID_PHONE"));
        }
    }
