namespace TestApp.Mocking;

public class CacheProduct : Product
{
    public Product Product { get; set; }
    public int CacheHit { get; set; }

    public CacheProduct(Product product)
    {
        Product = product;
        CacheHit = 0;   
    }
}