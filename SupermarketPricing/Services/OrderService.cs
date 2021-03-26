using SupermarketPricing.Models;
using SupermarketPricing.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupermarketPricing.Services
{
    public class OrderService : IOrderService
    {
        private OrderBusiness _orderBusiness;
        private readonly List<Order> _ordersRepo;

        // we can use dependency injection to inject order repository to this service later
        public OrderService()
        {
            _orderBusiness = new OrderBusiness();
            _ordersRepo = new List<Order>();
        }

        public Order GetOrder(int id)
        {
            return _ordersRepo.SingleOrDefault(x => x.Id == id);
        }

        public List<Order> GetAllOrders()
        {
            return _ordersRepo.ToList();
        }

        public void AddOrder(Order order)
        {
            _ordersRepo.Add(order);
        }

        public bool RemoveOrder(int id)
        {
            var order = GetOrder(id);
            if (order != null)
            {
                return _ordersRepo.Remove(order);
            }
            return false;
        }

        public void AddItemToOrder(int orderId, ProductOrder item)
        {
            var order = GetOrder(orderId);

            if (order == null)
            {
                throw new ArgumentNullException("Cannot insert to null order ");
            }

            if (item == null)
            {
                throw new ArgumentNullException("Cannot insert null order item ");
            }

            if (item.Product == null)
            {
                throw new ArgumentNullException("Cannot add an order item invalid product");
            }

            //if (item.Product.PricingRule == null)
            //{
            //    throw new Exception("Cannot add an order item invalid pricing rule");
            //}

            if (string.IsNullOrEmpty(item.Product.Sku))
            {
                throw new Exception("Cannot add an order item invalid product reference");
            }

            if (item.Product.UnitPrice <= 0)
            {
                throw new ArgumentException("Cannot add an order item invalid unit price");
            }

            if (item.Quantity <= 0)
            {
                throw new ArgumentException("Cannot add an order item with negative or zero quantity");
            }

            if ((item.Product.PricingRule != null) && (item.Product.MeasureUnit != item.Product.PricingRule.MeasureUnit))
            {
                throw new ArgumentException("Cannot add an order item with different measure unit and pricing rule unit");
            }

            order.ProductOrderList.Add(item);

        }

        public bool RemoveItemFromOrder(int orderId, int itemId)
        {
            var order = GetOrder(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order object not found");
            }
            var item = order.ProductOrderList.ToList().FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {
                throw new ArgumentException("Product order item not found");
            }
            
            return order.ProductOrderList.Remove(item);
        }

        public decimal CalculateTotalPrice(int id)
        {
            var order = GetOrder(id);
            return _orderBusiness.CalculateTotalPrice(order);
        }


    }
}
