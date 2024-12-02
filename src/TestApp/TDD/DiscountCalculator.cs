using System;
using System.Collections.Generic;

namespace TestApp.TDD;

// Wzorzec fabryki     

public class DiscountPercentageCalculator
{
    private readonly IDiscountPercentageFactory _factory;
    private readonly IDictionary<string, decimal> _discountCodes;
    
    public DiscountPercentageCalculator(IDiscountPercentageFactory factory)
    {
        _factory = factory;
    }
    
    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");
        
        if (string.IsNullOrEmpty(discountCode))
            return price;
        
        var percentage = _factory.Create(discountCode); 
        
        return price - price * percentage;
    }
}

