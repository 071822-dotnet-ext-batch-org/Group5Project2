
using Models;
using RepoLayer;

namespace Tests.StoreFrontAPI
{
    public class ModelsTests
    {
        private readonly StoreFrontRepoLayer _repoLayer;

        public ModelsTests()
        {
            this._repoLayer = new StoreFrontRepoLayer();
        }


        [Fact]
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

            Assert.Equal(newproduct.ProductID, guid);

        }



        [Fact]
        public void InsertproductCartsWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();


            //Act

            CartsProducts newcartproduct = new CartsProducts()
            {
                CartsProductsID = guid,
                FK_ProductsID = guid,
                FK_CartID = guid,

            };


            //Assert

            Assert.Equal(newcartproduct.CartsProductsID, guid);

        }





        [Fact]
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
                Fk_UserID = "auth0test1"


            };


            //Assert

            Assert.Equal(newuser.ProfileID, guid);

        }


    }


}