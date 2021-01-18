using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineService
{

    /// <summary>
    /// Promotion Engine logic Instace
    /// </summary>
    public class PromotionEngine_V1 : IPromotionEngine
    {
        IDictionary<char, int> _purchasedProductWithCount;



        public PromotionEngine_V1()
        {
                this._purchasedProductWithCount = new Dictionary<char, int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SKUCart">Cart with has master data and purchased data</param>
        /// <returns></returns>
        public double GetOrderPrice(SKUCart SKUCart)
        {
            populateProductWithCount(SKUCart);

            return GetPrice(SKUCart);
        }


        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="SKUCart">Cart with has master data and purchased data</param>
        private void populateProductWithCount(SKUCart SKUCart)
        {
            // Here we are populating products and its repeated times

            foreach (var product in SKUCart.GetPurchasedProductList())
            {
                if (_purchasedProductWithCount.ContainsKey(product))
                    _purchasedProductWithCount[product]++;
                else
                    _purchasedProductWithCount.Add(product, 1);
            }
        }



        private double GetPrice(SKUCart SKUCart)
        {

            // Getting popilated prices for each product (master data)
            IDictionary<char,double> priceOfEachItem = SKUCart.GetAvailableProductsWithPrice();

            double totalPrice =0;
            bool IsComboRule = checkForComboRule();

            foreach (var Product in _purchasedProductWithCount)
            {

                //Getting Rule defined for each product (sku id's)
                Tuple<int,double> Rule = PromotionRule.GetRule(Product.Key);
                if (Rule != null)
                    //applying rule and calculating total price
                    totalPrice += (Product.Value / Rule.Item1) * Rule.Item2 + (Product.Value % Rule.Item1) * priceOfEachItem[Product.Key];
                else
                //if there is no rule and it is not combo product(sku id) then calculating it's price
                    if (!IsComboRule)
                    totalPrice += priceOfEachItem[Product.Key] * Product.Value;


            }
            if (!IsComboRule)
                return totalPrice;
            else
                return caluculateComboRule(totalPrice, priceOfEachItem);
        }


        private bool checkForComboRule()
        {
           // Here we are checking whether combo product is present in the purchased cart ex scenrio C

            foreach (var item in PromotionRule._comboRules)
            {
                if (_purchasedProductWithCount.ContainsKey(item.Item1) && _purchasedProductWithCount.ContainsKey(item.Item2))
                    return true;
            }

           return  false;
        }


        private double caluculateComboRule(double totalPrice, IDictionary<char, double> priceOfEachItem)
        {

            //combo rule calculation

            //iterating new combo all the combo rules
            foreach (var item in PromotionRule._comboRules)
            {
                //calculating comborule and adding into its price
                while (_purchasedProductWithCount[item.Item1] != 0 && _purchasedProductWithCount[item.Item2] != 0)
                {
                    totalPrice += item.Item3;
                    _purchasedProductWithCount[item.Item1]--;
                    _purchasedProductWithCount[item.Item2]--;
                }

                //calculating individual price of it
                if (_purchasedProductWithCount[item.Item1] != 0)
                    totalPrice += _purchasedProductWithCount[item.Item1] * priceOfEachItem[item.Item1];

                if (_purchasedProductWithCount[item.Item2] != 0)
                    totalPrice += _purchasedProductWithCount[item.Item2] * priceOfEachItem[item.Item2];
            }

            return totalPrice;
        }

    }
}
