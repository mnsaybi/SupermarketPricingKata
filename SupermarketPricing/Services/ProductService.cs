using SupermarketPricing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketPricing.Services
{
    public class ProductService : IProductService
    {
        private readonly List<PricingRule> _pricingRulesRepo;
        private readonly List<Product> _productsRepo;
        private readonly List<ProductOrder> _productOrdersRepo;

        // we can use dependency injection to inject all repositories to this service later
        public ProductService()
        {
            _pricingRulesRepo = new List<PricingRule>();
            _productsRepo = new List<Product>();
            _productOrdersRepo = new List<ProductOrder>();
        }

        public IList<Product> GetAllProducts()
        {
            return _productsRepo.ToList();
        }

        public Product GetProduct(int id)
        {
            return _productsRepo.SingleOrDefault(x=>x.Id==id);
        }

        public Product GetProduct(string sku)
        {
            return _productsRepo.FirstOrDefault(x => x.Sku == sku);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            if (product.UnitPrice < 0)
            {
                throw new ArgumentException("Cannot insert new product with negative or zero unit price");
            }

            if (GetProduct(product.Sku) != null)
            {
                throw new ArgumentException($"Cannot insert new product with an existing sku : {product.Sku}");
            }

            _productsRepo.Add(product);
        }

        public bool RemoveProduct(int id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                return _productsRepo.Remove(product);
            }

            return false;
        }

        public IList<PricingRule> GetAllPricingRules()
        {
            return _pricingRulesRepo.ToList();
        }

        public PricingRule GetPricingRule(int id)
        {
            return _pricingRulesRepo.SingleOrDefault(x => x.Id == id);
        }

        public void AddPricingRule(PricingRule prule)
        {
            _pricingRulesRepo.Add(prule);
        }

        public bool RemovePricingRule(int id)
        {
            var prule = GetPricingRule(id);
            if (prule != null)
            {
               return _pricingRulesRepo.Remove(prule);
            }

            return false;                        
        }

        public IList<ProductOrder> GetAllProductOrders()
        {
            return _productOrdersRepo.ToList();
        }

        public ProductOrder GetProductOrder(int id)
        {
            return _productOrdersRepo.SingleOrDefault(x => x.Id==id);
        }

        public void AddProductOrder(ProductOrder porder)
        {
            _productOrdersRepo.Add(porder);
        }


    }
}
