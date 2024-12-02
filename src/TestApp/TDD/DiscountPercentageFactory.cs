using System;
using System.Collections.Generic;

namespace TestApp.TDD;


// Concrete Fabric Wzorzec fabryki
public class DiscountPercentageFactory : IDiscountPercentageFactory
{
    private readonly Dictionary<string, decimal> _discountCodes = [];
    public void Add(string discountCode, decimal percentage) => _discountCodes.Add(discountCode, percentage);
    public decimal Create(string discountCode) => _discountCodes.ContainsKey(discountCode) switch
    {
        true => _discountCodes[discountCode],
        _ => throw new ArgumentException("Invalid discount code")
    };
}