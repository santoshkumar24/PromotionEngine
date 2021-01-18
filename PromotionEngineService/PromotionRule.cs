using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineService
{
    public class PromotionRule
    {
        /// <summary>
        /// Rule 
        /// char -->skuid
        /// 
        /// Tuple
        /// 
        /// int --> number of times that sku id repeated
        /// double --> group value if t
        /// </summary>
        public static IDictionary<char, Tuple<int, double>> _rules;


        /// <summary>
        /// _comboRules
        /// char -->first skuid
        /// char --> second skuid
        /// double -->combinde value of it
        /// </summary>
        public static IList<Tuple<char, char, double>> _comboRules; 



        public PromotionRule(IDictionary<char, Tuple<int, double>> Rules, IList<Tuple<char, char, double>> ComboRules)
        { 


            _rules = Rules;
            _comboRules = ComboRules;
        }


        public static Tuple<int, double> GetRule(char SKUId)
        {
            //check key exist or not
            if (_rules.ContainsKey(SKUId))
                return _rules[SKUId];
            else
                return null;
        }


    }
}
