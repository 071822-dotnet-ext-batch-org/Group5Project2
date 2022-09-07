using Models;
using RepoLayer;
using System;
using System.Collections.Generic;


namespace Tests.StoreFrontAPI
{
    public class Mock_RepoLayer : StoreFrontRepoLayer
    {

        public async Task<List<ProductDto?>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }






    }




}
