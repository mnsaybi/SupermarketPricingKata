using NUnit.Framework;
using SupermarketPricing.Common;
using SupermarketPricing.Models;
using SupermarketPricing.Services;
using System;
using System.Collections.Generic;

namespace SupermarketPricing.Tests
{
    [TestFixture]
    public class SupermarketPricingTest
    {
        private OrderService orderService;
        private ProductService productService;

        [SetUp]
        public void Setup()
        {
            // we can use Mocking framework like Moq or NSubstitute
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
                Name = "A1",
                Sku = "A0001",
                UnitPrice = 0.5m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(1)
            });
            productService.AddProduct(new Product()
            {
                Id = 2,
                Name = "B1",
                Sku = "B0001",
                UnitPrice = 4.75m,
                MeasureUnit = MeasureUnit.POUND,
                PricingRule = productService.GetPricingRule(2)
            });
            productService.AddProduct(new Product()
            {
                Id = 3,
                Name = "C1",
                Sku = "C0001",
                UnitPrice = 1.75m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(3)
            });
            productService.AddProduct(new Product()
            {
                Id = 4,
                Name = "D1",
                Sku = "D0001",
                UnitPrice = 5.25m,
                MeasureUnit = MeasureUnit.PIECE,
                PricingRule = productService.GetPricingRule(4)
            });

            productService.AddProduct(new Product()
            {
                Id = 5,
                Name = "E1",
                Sku = "E0001",
                UnitPrice = 5.25m,
                MeasureUnit = MeasureUnit.GALLON
            });
            productService.AddProduct(new Product()
            {
                Id = 7,
                Name = "B2",
                Sku = "B0002",
                UnitPrice = 0.55m,
                MeasureUnit = MeasureUnit.OUNCE,
                PricingRule = productService.GetPricingRule(2)
            });
            productService.AddProduct(new Product()
            {
                Id = 8,
                Name = "D2",
                Sku = "D0002",
                UnitPrice = 5.25m,
                MeasureUnit = MeasureUnit.GALLON,
                PricingRule = productService.GetPricingRule(4)
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
                Id = 6,
                Name = "F1",
                Sku = "F0001",
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
            orderService.RemoveItemFromOrder(1, item.Id);

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

        [Test]
        public void Test_CanCalculateWhenDifferentUnits()
        {
            // Arrange
            var porder = new ProductOrder()
            {
                Id = 1,
                Product = productService.GetProduct(7),
                Quantity = 5
            };

            // Act
            // add product with different but compatiblemeasure unit between product and pricing rule            
            orderService.AddItemToOrder(1, porder);

            // Assert
            Assert.DoesNotThrow(() => orderService.CalculateTotalPrice(1));
        }

        [Test]
        public void Test_ExceptionWhenNotCompatibleUnits()
        {
            // Arrange
            var porder = new ProductOrder()
            {
                Id = 1,
                Product = productService.GetProduct(8),
                Quantity = 5
            };

            // Act
            // add product with incompatible measure unit between product and pricing rule            
            orderService.AddItemToOrder(1, porder);

            // Assert
            Assert.Catch(() => orderService.CalculateTotalPrice(1));
        }

        [Test]
        public void Test_PassWhenPrincingRuleIsNull()
        {
            var porder = new ProductOrder()
            {
                Id = 1,
                Product = productService.GetProduct(5),
                Quantity = 10
            };

            // add product with undefined pricing rule
            Assert.DoesNotThrow(() => orderService.AddItemToOrder(1, porder));

        }

        [Test]
        public void Test_CanCalculateWhenPrincingRuleIsNull()
        {
            // Arrange
            var porder = new ProductOrder()
            {
                Id = 1,
                Product = productService.GetProduct(5),
                Quantity = 10
            };

            // Act
            // add product with undefined pricing rule
            orderService.AddItemToOrder(1, porder);

            // Assert
            Assert.AreEqual(52.5, orderService.CalculateTotalPrice(1));

        }

        [Test]
        [TestCase(1, 3, 1.0)]
        [TestCase(1, 2, 1.0)]
        [TestCase(1, 5, 2.0)]
        [TestCase(2, 1, 1.99)]
        [TestCase(2, 1.5, 1.99 + 4.75 * 0.5)]
        [TestCase(7, 10, 0.55 * 10 * 0.0625)]
        [TestCase(3, 2, 1.5)]
        [TestCase(3, 6, 4.5)]
        [TestCase(3, 5, 3 + 1.75)]
        [TestCase(4, 15, 0.65 * 15)]
        public void Test_CanCalculateTotalWithPricingRules(int productId, decimal qty, decimal expectedTotal)
        {
            // Arrange
            var porder = new ProductOrder() { Id = 1, Product = productService.GetProduct(productId), Quantity = qty };

            // Act
            // add product with undefined pricing rule
            orderService.AddItemToOrder(1, porder);

            // Assert
            Assert.AreEqual(expectedTotal, orderService.CalculateTotalPrice(1));

        }

        [Test]
        [TestCase(new int[] { 1, 3, 4 }, new object[] { 3.0, 2.0, 15.0 }, 1 + 1.5 + 0.65 * 15)]
        [TestCase(new int[] { 1, 2, 3 }, new object[] { 2, 1.5, 6 }, 1 + 1.99 + 4.75 * 0.5 + 4.5)]
        [TestCase(new int[] { 1, 7, 3 }, new object[] { 5, 10, 5 }, 2 + 0.55 * 10 * 0.0625 + 3 + 1.75)]
        [TestCase(new int[] { 1, 2, 3 }, new object[] { 3, 1, 2 }, 1 + 1.99 + 1.5)]
        public void Test_CanCalculateTotalWithMultiplePricingRules(int[] productId, object[] qty, decimal expectedTotal)
        {
            orderService.AddItemToOrder(1, new ProductOrder() { Id = 1, Product = productService.GetProduct(productId[0]), Quantity = Convert.ToDecimal(qty[0]) });
            orderService.AddItemToOrder(1, new ProductOrder() { Id = 1, Product = productService.GetProduct(productId[1]), Quantity = Convert.ToDecimal(qty[1]) });
            orderService.AddItemToOrder(1, new ProductOrder() { Id = 1, Product = productService.GetProduct(productId[2]), Quantity = Convert.ToDecimal(qty[2]) });

            // Assert
            Assert.AreEqual(expectedTotal, orderService.CalculateTotalPrice(1));

        }

    }
}