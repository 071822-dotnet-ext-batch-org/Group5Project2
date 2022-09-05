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
        public async Task ProductsWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            ProductDto m = new ProductDto();

            m.ProductName = "Bed";

            Products newproduct = new Products()
            {
                ProductID = guid,
                ProductName = "Bed",
                ProductDetails = "Short bed",
                ProductPrice = 300.99,
                Stock = 100,
            };


            //Act

            bool p1 = await this._repoLayer.CheckExisitngProductAsync(newproduct);


            //Assert

            Assert.True(p1);

        }



        [Test]
        public async Task RegistrationWorksCorrectly()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            UserProfileDto u = new UserProfileDto();

            u.ProfileEmail = "ken@yahoo.com";

            UserProfile newUser = new UserProfile()
            {
                ProfileID = guid,
                ProfileName = "Jake Chan",
                ProfileAddress = "20 Chan Dr",
                ProfilePhone = "71399999999",
                ProfileEmail = "ken@yahoo.com",

            };


            //Act

            bool u1 = await this._repoLayer.GetUsersByEmailAsync(newUser);


            //Assert

            Assert.True(u1);


        }



    }


}