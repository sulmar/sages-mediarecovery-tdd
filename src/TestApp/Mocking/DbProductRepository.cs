namespace TestApp.Mocking;

public class DbProductRepository : IProductRepository
{
    public Product Get(int id)
    {
        throw new System.NotImplementedException();
    }
}