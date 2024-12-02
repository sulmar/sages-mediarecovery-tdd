using System;
using System.Collections.Generic;

namespace TestApp.TDD;

public class DiscountCodeParams
{
    public string DiscountCode { get; set; }
    public decimal Percentage { get; set; }
    public bool IsReusable { get; set; }
    public bool IsActive { get; set; } = true;

}

public class DiscountCalculator
{
    private readonly Dictionary<string, DiscountCodeParams> _discountCodes = [];

    public DiscountCalculator()
    {
        var discountCodeParams1 = new DiscountCodeParams { DiscountCode = "XYZ", Percentage = 0.5M};
        var discountCodeParams2 = new DiscountCodeParams { DiscountCode = "SAVE10NOW", Percentage = 0.9M, IsReusable = true};
        var discountCodeParams3 = new DiscountCodeParams { DiscountCode = "DISCOUNT20OFF", Percentage = 0.8M, IsReusable = true};
        
        _discountCodes.Add(discountCodeParams1.DiscountCode, discountCodeParams1); // discont_code, isActive
        _discountCodes.Add(discountCodeParams2.DiscountCode, discountCodeParams2); // discont_code, isActive
        _discountCodes.Add(discountCodeParams3.DiscountCode, discountCodeParams3); // discont_code, isActive
    }
    
    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(discountCode))
            return price;
        
        if (_discountCodes.ContainsKey(discountCode))
        {
            var discountCodeParams = _discountCodes[discountCode];
            
            if (discountCodeParams.IsActive)
            {
                discountCodeParams.IsActive = discountCodeParams.IsReusable;

                return price * discountCodeParams.Percentage;
            }
            
            throw new ArgumentException("Discount code has been used");
            
        }
        
        throw new ArgumentException("Invalid discount code");
    }
}