using SupermarketPricing.Models;
using System.Collections.Generic;

namespace SupermarketPricing.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Get all prodcuts
        /// </summary>
        /// <returns></returns>
        IList<Product> GetAllProducts();

        /// <summary>
        /// Get product object with id identifier
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// Get product object having sku value
        /// </summary>
        /// <param name="sku">product code value or sku</param>
        /// <returns></returns>
        Product GetProduct(string sku);

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(Product product);

        /// <summary>
        /// Remove product from list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RemoveProduct(int id);

        /// <summary>
        /// Get all pricing rules
        /// </summary>
        /// <returns></returns>
        IList<PricingRule> GetAllPricingRules();

        /// <summary>
        /// Get a pricing rule with id identifier
        /// </summary>
        /// <param name="id">Pricing rule identifier</param>
        /// <returns></returns>
        PricingRule GetPricingRule(int id);

        /// <summary>
        /// Add new pricing rule to list
        /// </summary>
        /// <param name="prule"></param>
        void AddPricingRule(PricingRule prule);

        /// <summary>
        /// Remove pricing rule from list
        /// </summary>
        /// <param name="id">Pricing rule identifier</param>
        /// <returns></returns>
        bool RemovePricingRule(int id);

        /// <summary>
        /// Get all created product orders
        /// </summary>
        /// <returns></returns>
        IList<ProductOrder> GetAllProductOrders();

        /// <summary>
        /// Get product order item with id identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductOrder GetProductOrder(int id);

        /// <summary>
        /// Add product order item to list
        /// </summary>
        /// <param name="porder"></param>
        void AddProductOrder(ProductOrder porder);


    }
}