namespace TestApp.Mocking;

public class ProductsController
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product Get(int id)
    {
        Product product = _repository.Get(id);

        return product;
    }
}