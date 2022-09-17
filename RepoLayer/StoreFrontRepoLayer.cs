using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using System.Data;

namespace RepoLayer
{

    public class StoreFrontRepoLayer
    {
        public StoreFrontRepoLayer() { }

        private readonly IConfiguration? _config;

        public StoreFrontRepoLayer(IConfiguration config)
        {
            _config = config;
        }
       
        //Insert product into inventory
        public async Task<Products> InsertProductsAsync(Products product, byte[]? Imagebyte)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Products] (ProductID, ProductName, ProductImage, ProductDetails, ProductPrice, StockDate, Stock)  VALUES (@productID, @productName, @productImage, @productDetails, @productPrice, @stockDate, @stock)", conn))
            {
                
                command.Parameters.Add("@productID", SqlDbType.UniqueIdentifier).Value = product.ProductID;
                command.Parameters.Add("@productName", SqlDbType.VarChar).Value = product.ProductName;
                command.Parameters.Add("@productImage", SqlDbType.VarBinary).Value = Imagebyte;
                command.Parameters.Add("@productDetails", SqlDbType.VarChar).Value = product.ProductDetails; 
                command.Parameters.Add("@productPrice", SqlDbType.SmallMoney).Value = product.ProductPrice;     
                command.Parameters.Add("@stockDate", SqlDbType.Date).Value = product.StockDate;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = product.Stock;              
                
                
                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                conn.Close();
                return product;
            }
        }//EoM



        //Check for exisiting products before inserting
        public async Task<bool> CheckExisitngProductAsync(Products product)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE ProductName = @productName", conn))
            {
                command.Parameters.AddWithValue("@productName", product.ProductName);
                conn.Open();
                SqlDataReader? ret = await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    ProductDto m = new ProductDto();

                    m.ProductID = ret.GetGuid(0);
                    m.ProductName = ret.GetString(1);

                    byte[] Imagebyte2 = (byte[])ret["ProductImage"];
                    m.ProductImage = Imagebyte2;

                    m.ProductDetails = ret.GetString(3);
                    m.ProductPrice = (double)ret.GetDecimal(4);
                    m.StockDate = ret.GetDateTime(5);
                    m.Stock = ret.GetInt32(6);

                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }

        }//EoM



        //Get Product by ID

        public async Task<ProductDto?> GetProductByIdAsync(Guid productID)
        {
            //Console.WriteLine("This is a connection test: " + _config.GetSection("ConnectionStrings"));

            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE ProductID = @productID", conn))
            {
                command.Parameters.AddWithValue("@productID", productID);           
                conn.Open();
                SqlDataReader? ret = await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    ProductDto p = new ProductDto();

                    p.ProductID = ret.GetGuid(0);
                    p.ProductName = ret.GetString(1);

                    byte[] Imagebyte = (byte[])ret["ProductImage"];
                    p.ProductImage = Imagebyte;

                    p.ProductDetails = ret.GetString(3);
                    p.ProductPrice = (double)ret.GetDecimal(4);
                    p.StockDate = ret.GetDateTime(5);
                    p.Stock = ret.GetInt32(6);

                    conn.Close();
                    return p;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }

        }//EoM



        //Get all Products

        public async Task<List<ProductDto?>> GetAllProductsAsync()
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Products", conn))
            {          
                conn.Open();
                SqlDataReader? ret = await command.ExecuteReaderAsync();
                
                List<ProductDto?> productList = new List<ProductDto?>();

                while (ret.Read())
                {

                    ProductDto p = new ProductDto();

                    p.ProductID = ret.GetGuid(0);
                    p.ProductName = ret.GetString(1);

                    byte[] Imagebyte = (byte[])ret["ProductImage"];
                    p.ProductImage = Imagebyte;

                    p.ProductDetails = ret.GetString(3);
                    p.ProductPrice = (double)ret.GetDecimal(4);
                    p.StockDate = ret.GetDateTime(5);
                    p.Stock = ret.GetInt32(6);

                    productList.Add(p);

                    
                }
                conn.Close();
                return productList;
            }

        }//EoM


       

        //Register New Users
        public async Task<UserProfile> RegisterAsync(UserProfile userprofile, byte[]? UserImagebyte)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Profiles] (ProfileID, ProfileName, ProfileAddress, ProfilePhone, ProfileEmail, ProfilePicture, Fk_UserID)  VALUES (@profileID, @profileName, @profileAddress, @profilePhone, @profileEmail, @profileImage, @fk_UserID)", conn))
            {

                command.Parameters.Add("@profileID", SqlDbType.UniqueIdentifier).Value = userprofile.ProfileID;
                command.Parameters.Add("@profileName", SqlDbType.VarChar).Value = userprofile.ProfileName;
                command.Parameters.Add("@profileAddress", SqlDbType.VarChar).Value = userprofile.ProfileAddress;
                command.Parameters.Add("@profilePhone", SqlDbType.VarChar).Value = userprofile.ProfilePhone;
                command.Parameters.Add("@profileEmail", SqlDbType.VarChar).Value = userprofile.ProfileEmail;
                command.Parameters.Add("@profileImage", SqlDbType.VarBinary).Value = UserImagebyte;
                command.Parameters.Add("@fk_UserID", SqlDbType.UniqueIdentifier).Value = userprofile.Fk_UserID;
                

                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                conn.Close();
                return userprofile;     
            
            }


        }//EoM




        //Check for exisitng users before registering
        public async Task<bool> GetUsersByEmailAsync(UserProfile userprofile)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM [dbo].[Profiles] WHERE ProfileEmail = @profileEmail", conn))
            {
                UserProfileDto u = new UserProfileDto();
                command.Parameters.AddWithValue("@profileEmail", userprofile.ProfileEmail);
                conn.Open();
                SqlDataReader? ret = await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    u.ProfileID = ret.GetGuid(0);
                    u.ProfileName = ret.GetString(1);
                    u.ProfileAddress = ret.GetString(2);
                    u.ProfilePhone = ret.GetString(3);
                    u.ProfileEmail = ret.GetString(4);

                    byte[] userImagebyte = (byte[])ret["ProfilePicture"];
                    u.ProfilePicture = string.Empty;
                    u.ProfileID = ret.GetGuid(6);
                    
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }

        }//EoM


        //Add product to cart
        public async Task<CartsProducts> AddProductToCartAsync(CartsProducts addtocart)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[CartsProducts] (CartsProductsID, FK_ProductsID, FK_CartID)  VALUES (@cartsproductsID, @fk_productID, @fk_cartID)", conn))
            {

                command.Parameters.Add("@cartsproductsID", SqlDbType.UniqueIdentifier).Value = addtocart.CartsProductsID;
                command.Parameters.Add("@fk_productID", SqlDbType.UniqueIdentifier).Value = addtocart.FK_ProductsID;
                command.Parameters.Add("@fk_cartID", SqlDbType.UniqueIdentifier).Value = addtocart.FK_CartID;


                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                conn.Close();
                return addtocart;
            }
        }//EoM



        //Check for exisiting products in cart
        public async Task<bool> CheckExisitngCartProductAsync(CartsProducts addtocart)
        {
            SqlConnection conn = new SqlConnection();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM [dbo].[CartsProducts] WHERE CartsProductsID = @cartsproductID", conn))
            {
                command.Parameters.AddWithValue("@cartsproductID", addtocart.CartsProductsID);
                conn.Open();
                SqlDataReader? ret = await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    CartsProducts cp = new CartsProducts();

                    cp.CartsProductsID = ret.GetGuid(0);
                    cp.FK_ProductsID = ret.GetGuid(1);
                    cp.FK_CartID = ret.GetGuid(2);

                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }

        }//EoM

           private static readonly
      SqlConnection conn = new SqlConnection("Server=tcp:mohammadrevature.database.windows.net,1433;Initial Catalog=revatureClasses;Persist Security Info=False;User ID=mohammad960@revature.net@mohammadrevature;Password=Revature@2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;;");

        public async Task<Order?> CreateNewOrderAsync(UpdateNewOrderDto unod, Guid orderId)
        {
            using (SqlCommand command = new SqlCommand($"INSERT INTO Order (OrderId,OrderTotal,DateOrder,DateDelivered,Cancelled,Refunded) VALUES (@orderId,@orderTotal,@dateOrder,@dateDelivered,@cancelled,@refunded)", conn))
            {
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@orderTotal", unod.OrderTotal);
                command.Parameters.AddWithValue("@dateOrder", unod.DateOrder);
                command.Parameters.AddWithValue("@dateDelivered", unod.dateDelivered);
                command.Parameters.AddWithValue("@cancelled", unod.Cancelled);
                command.Parameters.AddWithValue("@refunded", unod.Refunded);

                conn.Open();
                if ((await command.ExecuteNonQueryAsync()) == 1) {
                    conn.Close();
                    Order oo = new Order(orderId, unod.OrderTotal, unod.DateOrder, unod.dateDelivered, unod.Cancelled, unod.Refunded, null);
                    return oo;

                }
                return null;
            }

        }

            public async Task<bool> UpdateProductImage(byte[] photo, Guid ProductID)
        {
            using SqlCommand command = new SqlCommand("UPDATE [dbo].[Products] SET ProductImage = @ProductImage WHERE ProductID = @productId", conn);
            command.Parameters.AddWithValue("@ProductImage", photo);
            command.Parameters.AddWithValue("@productId", ProductID);

            conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) > 0;
            conn.Close();

            return ret;
        }

}
}





