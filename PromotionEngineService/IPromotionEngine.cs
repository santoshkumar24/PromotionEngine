using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineService
{
   public interface IPromotionEngine
    {
        double GetOrderPrice(SKUCart Cart);
    }
}
