using NUnit.Framework;
using SupermarketPricing.Common;
using SupermarketPricing.Models;
using SupermarketPricing.Services;
using System;
using System.Collections.Generic;

namespace SupermarketPricing.Tests
{
    public class SupermarketPricingTest
    {
        private OrderService orderService;
        private ProductService productService;

        [SetUp]
        public void Setup()
        {
            orderService = new OrderService();
            productService = new ProductService();

            // init new list of pricing rules
            productService.AddPricingRule(new PricingRule()
            {
                Id = 1,
                Quantity = 3,
                Price = 1m,
                MeasureUnit = MeasureUnit.PIECE,
                Description = "Buy Three for a Dollar",
                BonusQuantity = 0
            });
            productService.AddPricingRule(new PricingRule()
            {
                Id = 2,
                Quantity = 1,
                Price = 1.99m,
                MeasureUnit = MeasureUnit.POUND,
                Description = "Buy one pound for 2 Dollars",
                BonusQuantity = 0
            });
            productService.AddPricingRule(new PricingRule()
            {
                Id = 3,
                Quantity = 2,
                Price = 1.5m,
                MeasureUnit = MeasureUnit.PIECE,
                Description = "Buy two, get one free",
                BonusQuantity = 1
            });
            productService.AddPricingRule(new PricingRule()
            {
                Id = 4,
                Quantity = 1,
                Price = 0.65m,
                MeasureUnit = MeasureUnit.PIECE,
                Description = "standard pricing",
                BonusQuantity = 0
            });

            // init new list of products
            productService.AddProduct(new Product()
            {
                Id = 1,
                Name = "A",
                Sku = "A0001",
                UnitPrice = 3.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });
            productService.AddProduct(new Product()
            {
                Id = 2,
                Name = "B",
                Sku = "B0001",
                UnitPrice = 3.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });
            productService.AddProduct(new Product()
            {
                Id = 3,
                Name = "C",
                Sku = "C0001",
                UnitPrice = 3.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });
            productService.AddProduct(new Product()
            {
                Id = 4,
                Name = "D",
                Sku = "D0001",
                UnitPrice = 3.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });

            // init new order object with zero items
            orderService.AddOrder(new Order() { Id = 1, ProductOrderList = new List<ProductOrder>() });
            orderService.AddOrder(new Order() { Id = 2, ProductOrderList = new List<ProductOrder>() });
            orderService.AddOrder(new Order() { Id = 3, ProductOrderList = new List<ProductOrder>() });

        }

        [Test]
        public void Test_CanAddOrRemovePricingRule()
        {
            productService.AddPricingRule(new PricingRule()
            {
                Id = 5,
                Quantity = 1,
                Price = 1.35m,
                MeasureUnit = MeasureUnit.KILOGRAM,
                Description = "new pricing rule",
                BonusQuantity = 0
            });

            productService.RemovePricingRule(5);

        }

        [Test]
        public void Test_CanAddOrRemoveProduct()
        {
            productService.AddProduct(new Product()
            {
                Id = 5,
                Name = "E",
                Sku = "E0001",
                UnitPrice = 6.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });

            productService.RemoveProduct(5);

        }

        [Test]
        public void Test_ExceptionWhenProductSkuExists()
        {
            Assert.Throws<ArgumentException>(() => productService.AddProduct(new Product()
            {
                Id = 5,
                Name = "E",
                Sku = "D0001",
                UnitPrice = 6.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            }));

        }

        [Test]
        public void Test_CanAddOrRemoveOrder()
        {
            orderService.AddOrder(new Order()
            {
                Id = 4,
                ProductOrderList = new List<ProductOrder>()
            });

            orderService.RemoveOrder(4);

        }

        [Test]
        public void Test_CanAddOrRemoveItemToOrder()
        {
            var item = new ProductOrder() { Id = 1, Product = productService.GetProduct(2), Quantity = 3 };
            orderService.AddItemToOrder(1, item);           
            orderService.RemoveItemFromOrder(1,item.Id);

        }

        [Test]
        public void Test_ExceptionWhenAddNullProductToOrder()
        {
            Assert.Catch(() => orderService.AddItemToOrder(1, new ProductOrder()
            {
                Id = 1,
                Product = productService.GetProduct(20),
                Quantity = 3
            }));

        }

        [Test]
        public void Test_UnitConvesrionKgToGramm()
        {
            Assert.AreEqual(1000, UnitsConverter.GetEquivalentUnit(MeasureUnit.KILOGRAM, MeasureUnit.GRAM));
        }

        [Test]
        public void Test_UnitConvesrionPoundToOunce()
        {
            Assert.AreEqual(16, UnitsConverter.GetEquivalentUnit(MeasureUnit.POUND, MeasureUnit.OUNCE));
        }

        [Test]
        public void Test_UnitConvesrionExceptionWhenPiece()
        {
            Assert.Catch(() => UnitsConverter.GetEquivalentUnit(MeasureUnit.PIECE, MeasureUnit.KILOGRAM));
        }

        [Test]
        public void Test_UnitConvesrionExceptionWhenDifferentType()
        {
            Assert.Catch(() => UnitsConverter.GetEquivalentUnit(MeasureUnit.KILOGRAM, MeasureUnit.LITER));
        }
    }
}