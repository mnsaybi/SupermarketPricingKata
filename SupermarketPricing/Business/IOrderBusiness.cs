using SupermarketPricing.Models;

namespace SupermarketPricing.Business
{
    internal interface IOrderBusiness
    {       

        /// <summary>
        /// Calculate the total price for order 
        /// </summary>
        /// <param name="order">The order with all items to calculate</param>
        /// <returns>A decimal value representing the total price</returns>
        decimal CalculateTotalPrice(Order order);


    }
}