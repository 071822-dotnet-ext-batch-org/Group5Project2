using Models;
using Xunit;
using RepoLayer;

namespace Test.StoreFrontAPI;



public class UnitTest
{
    private readonly StoreFrontRepoLayer _repoLayer;
    public UnitTest()
    {
        this._repoLayer = new StoreFrontRepoLayer();
    }



    [Fact]
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

        Assert.AreEqual(null, p1);


    }



    [Fact]
    public async Task RegistrationWorksCorrectly()
    {
        //Arrange
        Guid guid = Guid.NewGuid();


        UserProfile u = new UserProfile()
        {
            ProfileID = guid,
            ProfileName = "Jake Chan",
            ProfileAddress = "20 Chan Dr",  
            ProfilePhone = "71399999999",
            ProfileEmail = "ken@yahoo.com",
    
        };


        //Act

        bool u1 = await this._repoLayer.GetUsersByEmailAsync(u);


        //Assert

        Assert.True(u1);


    }





}
