using GoF.FacadePattern.External;
using GoF.FacadePattern.Facade;
using GoF.FacadePattern.Models;
using GoF.FacadePattern.Repositories;
using Moq;

namespace GoF.Tests
{
    public class ProductsFacadeTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IExternalProductService> _externalProductServiceMock;
        private readonly ProductsFacade _productsFacade;

        public ProductsFacadeTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _externalProductServiceMock = new Mock<IExternalProductService>();
            _productsFacade = new ProductsFacade(_productRepositoryMock.Object, _externalProductServiceMock.Object);
        }

        [Fact]
        public async Task ���i�����|�W�g���ɑ��݂���ꍇ_���|�W�g�����珤�i���擾����()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId, Name = "Test Product", Price = 10.0m };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productsFacade.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result?.Id);
            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _externalProductServiceMock.Verify(service => service.FetchProductAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ���i�����|�W�g���ɑ��݂��Ȃ��ꍇ_�O���T�[�r�X���珤�i���擾���ăL���b�V���ɕۑ�����()
        {
            // Arrange
            var productId = 2;
            var externalProduct = new Product { Id = productId, Name = "External Product", Price = 20.0m };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product?)null);
            _externalProductServiceMock.Setup(service => service.FetchProductAsync(productId)).ReturnsAsync(externalProduct);
            _productRepositoryMock.Setup(repo => repo.AddAsync(externalProduct)).Returns(Task.CompletedTask);

            // Act
            var result = await _productsFacade.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result?.Id);
            Assert.Equal("External Product", result?.Name);
            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _externalProductServiceMock.Verify(service => service.FetchProductAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Id == productId)), Times.Once);
        }

        [Fact]
        public async Task ���i���ǂ��ɂ����݂��Ȃ��ꍇ_null��Ԃ�()
        {
            // Arrange
            var productId = 3;
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product?)null);
            _externalProductServiceMock.Setup(service => service.FetchProductAsync(productId)).ReturnsAsync((Product?)null);

            // Act
            var result = await _productsFacade.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
            _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
            _externalProductServiceMock.Verify(service => service.FetchProductAsync(productId), Times.Once);
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task ���|�W�g���ɓo�^����Ă��邷�ׂĂ̏��i���擾����ꍇ_���ׂĂ̏��i��Ԃ�()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0m },
                new Product { Id = 2, Name = "Product 2", Price = 20.0m }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productsFacade.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
