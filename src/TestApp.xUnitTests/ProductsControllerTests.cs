using TestApp.Mocking;

namespace TestApp.xUnitTests;


public class CacheProductRepositoryProxyTests
{
    private class FakeExistingProductRepository : IProductRepository
    {
        public Product Get(int id)
        {
            return new Product {Id = id};
        }
    }

    private class FakeNotExistingProductRepository : IProductRepository
    {
        public Product Get(int id)
        {
            return null;
        }
    }

    private class CounterProductRepository : IProductRepository
    {
        public bool IsCalled { get; set; }
        public Product Get(int id)
        {
            IsCalled = true; 
            
            return null;
        }
    }

    [Fact]
    public void Get_WhenNotAdded_ShouldRealRepositoryIsCalled()
    {
        // Arrange
        CounterProductRepository realRepository = new CounterProductRepository();
        CacheProductRepositoryProxy repositoryProxy = new CacheProductRepositoryProxy(realRepository);
        
        // Act
        var product = repositoryProxy.Get(1);
        
        // Assert
      Assert.True(realRepository.IsCalled);
    }
    
    [Fact]
    public void Get_WhenProductAdded_ShouldRealRepositoryIsNotCalled()
    {
        // Arrange
        CounterProductRepository repository = new CounterProductRepository();
        CacheProductRepositoryProxy repositoryProxy = new CacheProductRepositoryProxy(repository);
        repositoryProxy.Add(new Product {Id = 1});
        
        // Act
        repositoryProxy.Get(1);
        
        // Assert
        Assert.False(repository.IsCalled);
    }
    
   
    
    [Fact]
    public void GetCacheHit_FirstCallWithExistingId_ShouldReturnProductAndCacheHitEqualZero()
    {
        // Arrange
        CacheProductRepositoryProxy repositoryProxy =
            new CacheProductRepositoryProxy(new FakeExistingProductRepository());
        
        repositoryProxy.Get(1);
        
        // Act
        var cacheHit = repositoryProxy.GetCacheHit(1);

        // Assert
        Assert.Equal(0, cacheHit);
    }
    
    [Fact]
    public void Get_NextCallWithExistingId_ShouldReturnProductAndCacheHitEqualGreatherZero()
    {
        // Arrange
        CacheProductRepositoryProxy repositoryProxy =
            new CacheProductRepositoryProxy(new FakeExistingProductRepository());

        repositoryProxy.Get(1);
        repositoryProxy.Get(1);
        repositoryProxy.Get(1);
        
        // Act
        repositoryProxy.Get(1);

        // Assert
        var cacheHit = repositoryProxy.GetCacheHit(1);
        Assert.Equal(3, cacheHit);
    }
}

public class ProductsControllerTests
{
    private class FakeExistingProductRepository : IProductRepository
    {
        public Product Get(int id)
        {
            return new Product {Id = id};
        }
    }

    private class FakeNotExistingProductRepository : IProductRepository
    {
        public Product Get(int id)
        {
            return null;
        }
    }

    [Fact]
    public void Get_ExistingId_ReturnsProduct()
    {
        // Arrange
        IProductRepository repository = new FakeExistingProductRepository();
        ProductsController controller = new ProductsController(repository);

        // Act
        var result = controller.Get(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public void Get_NotExistingId_ReturnsEmptyProduct()
    {
        // Arrange
        IProductRepository repository = new FakeNotExistingProductRepository();
        ProductsController controller = new ProductsController(repository);

        // Act
        var result = controller.Get(1);

        // Assert
        Assert.Null(result);
    }

   
}