namespace TestApp.TDD;

// Abstract Factory
public interface IDiscountPercentageFactory
{
    void Add(string discountCode, decimal percentage);
    decimal Create(string discountCode);
}