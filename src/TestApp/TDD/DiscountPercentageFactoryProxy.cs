using System;
using System.Collections.Generic;

namespace TestApp.TDD;

// Wzorzec Proxy (Pośrednik)
public class DiscountPercentageFactoryProxy : IDiscountPercentageFactory
{
    private readonly HashSet<string> _discountCodes = [];
    private readonly HashSet<string> _usedDiscountCodes = [];
    
    // Real Subject
    private readonly IDiscountPercentageFactory _realFactory;
    
    private decimal _percentage;
    public DiscountPercentageFactoryProxy(IDiscountPercentageFactory realFactory)
    {
        _realFactory = realFactory;
    }

    public void Add(string discountCode, decimal percentage)
    {
        _percentage = percentage;
        _discountCodes.Add(discountCode);
    }
    
    public decimal Create(string discountCode)
    {
        if (_usedDiscountCodes.Contains(discountCode))
            throw new ArgumentException("Discount code has been used"); 
        
        if (_discountCodes.Contains(discountCode))
        {
            _discountCodes.Remove(discountCode);
            _usedDiscountCodes.Add(discountCode);

            return _percentage;
        }
        
        return _realFactory.Create(discountCode);
    }
}