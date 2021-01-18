using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineService
{

    /// <summary>
    /// Order Calculator
    /// </summary>
    public class OrderCalculator
    {

        private readonly IPromotionEngine _promotionEngine;

        /// <summary>
        /// Inject Your _promotionEngine Types
        /// </summary>
        /// <param name="PromotionEngine"> Promotion engine logic instance</param>
        public OrderCalculator(IPromotionEngine PromotionEngine)
        {
           _promotionEngine = PromotionEngine;
        }

        /// <summary>
        /// Pass Purchased cart
        /// </summary>
        /// <param name="cart">Cart with has master data and purchased data</param>
        /// <returns></returns>
        public double Calculate(SKUCart cart)
        {
            return _promotionEngine.GetOrderPrice(cart);
        }
    }
}
