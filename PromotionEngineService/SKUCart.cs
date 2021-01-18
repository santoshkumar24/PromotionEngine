using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineService
{

    /// <summary>
    /// Defining SKUCart which has master product list and purchased product list 
    /// </summary>
    public class SKUCart
    {
        private IList<char> _products;
        private readonly IDictionary<char, double> _productByPrice;

        public SKUCart(IDictionary<char, double> ProductByPrice)
        {
            this._products = new List<char>();

            //Master(Available SKUid in master cart) List with client sent data
            _productByPrice = ProductByPrice;
        }
        
        /// <summary>
        /// Add Purchased cart to consumer cart
        /// </summary>
        /// <param name="Product"></param>
        public void AddProductToCart(char Product)
        {
            //Check added product is available in master cart or not
            if (this._productByPrice.ContainsKey(Product))
                _products.Add(Product);
            else
                throw new Exception("Product is not available in SKUCArt, Kindly Add Product into SKUcart along with the price");

        }

        /// <summary>
        /// Returns purchased consumer cart 
        /// </summary>
        /// <returns></returns>
        public IList<char> GetPurchasedProductList()
        {
           
            return _products;
        }

        /// <summary>
        /// Return Master cart with price
        /// </summary>
        /// <returns></returns>
        public IDictionary<char, double> GetAvailableProductsWithPrice()
        {
            //reruen 
            return _productByPrice;
        }


    }
}
