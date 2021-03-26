using SupermarketPricing.Models;
using System.Collections.Generic;

namespace SupermarketPricing.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Return order object 
        /// </summary>
        /// <param name="id">order identifier</param>
        /// <returns></returns>
        Order GetOrder(int id);

        /// <summary>
        /// Get list of all existing orders 
        /// </summary>
        /// <returns></returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// Add new order to orders list
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);

        /// <summary>
        /// remove order from list of orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveOrder(int id);

        /// <summary>
        /// Add new product order item to list of items in order
        /// </summary>
        /// <param name="orderId">the target order identifier</param>
        /// <param name="item">the product order item to add</param>
        void AddItemToOrder(int orderId, ProductOrder item);

        /// <summary>
        /// Remove a product order item from list of items in order
        /// </summary>
        /// <param name="orderId">the target order identifier</param>
        /// <param name="itemId">the product order item to remove</param>
        /// <returns></returns>
        bool RemoveItemFromOrder(int orderId, int itemId);

        /// <summary>
        /// Calculate the total price for an order
        /// </summary>
        /// <param name="id">order identifier</param>
        /// <returns></returns>
        decimal CalculateTotalPrice(int id);


    }
}