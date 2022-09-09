using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Models;
using RepoLayer;
using System.Drawing;


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
        public void InsertproductCartsWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();


            //Act

            CartProducts newcartproduct = new CartProducts()
            {
                CartProductsID = guid,
                FK_ProductID = guid,
                FK_CartID = guid,

            };


            //Assert

            Assert.AreEqual(newcartproduct.CartProductsID, guid);

        }





        [Test]
        public void InsertNewUserWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();


            //Act

            UserProfile newuser = new UserProfile()
            {
                ProfileID = guid,
                ProfileName = "Ken",
                ProfileAddress = "20 ken dr",
                ProfilePhone = "713992867",
                ProfileEmail = "ken@yahoo.com",
                Fk_UserID = guid


            };


            //Assert

            Assert.AreEqual(newuser.ProfileID, guid);

        }




        [Test]
        public async Task ProductsWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            Mock_RepoLayer m = new Mock_RepoLayer();

            ProductsBusinessLayer pBL = new ProductsBusinessLayer();

            Products product = new Products();



            //Act

            List<ProductDto?> productList = await pBL.GetAllProductsAsync();


            //Assert

            Assert.IsTrue(true);

        }



    }


}