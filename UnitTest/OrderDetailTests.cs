using System;
using System.Collections.Generic;
using System.IO;
using Class_Library;
using Xunit;


    public class OrderDetailTests
    {
        [Fact]
        public void TestOrderDetailCreation_ValidInputs()
        {
            var detail = new OrderDetail(1, "ELECT001", "TV", 300.00f, 2);
            Assert.Equal(1, detail.DetailNumber);
            Assert.Equal("ELECT001", detail.StockID);
            Assert.Equal("TV", detail.StockName);
            Assert.Equal(300.00f, detail.StockPrice);
            Assert.Equal(2, detail.Quantity);
            Assert.Equal(600.00f, detail.SubTotal); // Price * Quantity
        }

        [Fact]
        public void TestOrderDetailCreation_InvalidStockID()
        {
            Assert.Throws<ArgumentException>(() => new OrderDetail(1, "", "TV", 300.00f, 2));
        }

        [Fact]
        public void TestOrderDetailCreation_InvalidQuantity()
        {
            Assert.Throws<ArgumentException>(() => new OrderDetail(1, "ELECT001", "TV", 300.00f, 0));
        }

        [Fact]
        public void TestCalculateSubTotal_ValidInputs()
        {
            var detail = new OrderDetail(1, "ELECT001", "TV", 300.00f, 2);
            var subTotal = detail.CalculateSubTotal();
            Assert.Equal(600.00f, subTotal);
        }

        [Fact]
        public void TestCalculateSubTotal_InvalidStockPrice()
        {
            var detail = new OrderDetail(1, "ELECT001", "TV", -100.00f, 2);
            Assert.Throws<InvalidOperationException>(() => detail.CalculateSubTotal());
        }

        [Fact]
        public void TestDeepCopy()
        {
            var detail = new OrderDetail(1, "ELECT001", "TV", 300.00f, 2);
            var copiedDetail = detail.DeepCopy();

            // Ensure the copied object has identical values
            Assert.Equal(detail.DetailNumber, copiedDetail.DetailNumber);
            Assert.Equal(detail.StockID, copiedDetail.StockID);
            Assert.Equal(detail.StockName, copiedDetail.StockName);
            Assert.Equal(detail.StockPrice, copiedDetail.StockPrice);
            Assert.Equal(detail.Quantity, copiedDetail.Quantity);
            Assert.Equal(detail.SubTotal, copiedDetail.SubTotal);

            // Ensure they are two distinct objects
            Assert.NotSame(detail, copiedDetail);
        }

        [Fact]
        public void TestDeepCopy_ModificationsDoNotAffectOriginal()
        {
            var detail = new OrderDetail(1, "ELECT001", "TV", 300.00f, 2);
            var copiedDetail = detail.DeepCopy();
            copiedDetail.Quantity = 3; // Modify copy

            Assert.Equal(2, detail.Quantity); // Original remains unchanged
            Assert.Equal(3, copiedDetail.Quantity);
        }
    }

