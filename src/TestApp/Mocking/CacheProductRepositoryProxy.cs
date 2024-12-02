using System.Collections.Generic;

namespace TestApp.Mocking;

public class CacheProductRepositoryProxy : IProductRepository
{
    // Real Subject
    private readonly IProductRepository _productRepository;

    private readonly Dictionary<int, CacheProduct> _products = [];

    public CacheProductRepositoryProxy(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Add(Product product)
    {
        _products.Add(product.Id, new CacheProduct(product));
    }

    public int GetCacheHit(int id)
    {
        return _products[id].CacheHit;
    }

    public Product Get(int id)
    {
        if (_products.TryGetValue(id, out var product))
        {
            product.CacheHit++;
            return product.Product;
        }

        // Real Subject
        var realProduct = _productRepository.Get(id);

        if (realProduct != null)
        {
            Add(realProduct);
        }

        return realProduct;
    }
}