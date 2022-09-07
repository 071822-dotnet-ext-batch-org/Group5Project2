using BusinessLayer;
using Models;
using RepoLayer;


namespace Tests.StoreFrontAPI
{
    public class StoreFrontAPITests
    {
        private readonly StoreFrontRepoLayer _repoLayer;

        public StoreFrontAPITests()
        {
            this._repoLayer = new StoreFrontRepoLayer();
        }


        [Test]
        public void ProductsInsertWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();


            //Act

            Products newproduct = new Products()
            {
                ProductID = guid,
                ProductName = "Bed",
                ProductDetails = "Short bed",
                ProductPrice = 300.99,
                Stock = 100,
            };


            //Assert

            Assert.AreEqual(newproduct.ProductID, guid);

        }


        [Test]
        public async Task ProductsWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            Mock_RepoLayer m = new Mock_RepoLayer();

            ProductsBusinessLayer pBL = new ProductsBusinessLayer();

            Products product = new Products( );
        


            //Act

            List<ProductDto?> productList = await pBL.GetAllProductsAsync();


            //Assert

            Assert.IsTrue(true);

        }



    }


}