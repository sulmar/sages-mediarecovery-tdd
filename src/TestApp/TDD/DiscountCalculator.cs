using System;
using System.Collections.Generic;

namespace TestApp.TDD;

public class DiscountCalculator
{
    private readonly Dictionary<string, bool> _discountCodes = [];

    public DiscountCalculator()
    {
        _discountCodes.Add("XYZ", true); // discont_code, isActive
    }
    
    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(discountCode))
            return price;
        
        if (discountCode == "SAVE10NOW")
            return price * 0.9M;
        
        if (discountCode == "DISCOUNT20OFF")
            return price * 0.8M;

        if (_discountCodes.ContainsKey(discountCode))
        {
            if (_discountCodes[discountCode])
            {
                _discountCodes[discountCode] = false;
                return price * 0.5M;
            }
            else
            {
                throw new ArgumentException("Discount code has been used");
            }
        }
        
        throw new ArgumentException("Invalid discount code");
    }
}