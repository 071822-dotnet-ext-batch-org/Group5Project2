using System.Data.SqlClient;
using System;
using Models;
using System.Data;
using Microsoft.AspNetCore.Http;



namespace RepoLayer
{
    public class StoreFrontRepoLayer
    {
       

        private static readonly SqlConnection conn = new SqlConnection("Server=tcp:emma1.database.windows.net,1433;Initial Catalog=Project1;Persist Security Info=False;User ID=nwaodec79;Password= Magnum_2279;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public async Task<int> InsertProductsAsync(Products product, byte[]? Imagebyte)
        {

            using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Products] (ProductID, ProductName, ProductImage, ProductDetails, ProductPrice, StockDate, Stock)  VALUES (@productID, @productName, @productImage, @productDetails, @productPrice, @stockDate, @stock)", conn))
            {
                //command.Parameters.AddWithValue("@productID", product.ProductID);
                command.Parameters.Add("@productID", SqlDbType.UniqueIdentifier).Value = product.ProductID;
                //command.Parameters.AddWithValue("@productName", product.ProductName);
                command.Parameters.Add("@productName", SqlDbType.VarChar).Value = product.ProductName;
                //command.Parameters.AddWithValue("@productImage", product.ProductImage);
                command.Parameters.Add("@productImage", SqlDbType.VarBinary).Value = Imagebyte;
                //command.Parameters.AddWithValue("@productDetails", product.ProductDetails);
                command.Parameters.Add("@productDetails", SqlDbType.VarChar).Value = product.ProductDetails;
                //command.Parameters.AddWithValue("@productPrice", product.ProductPrice);
                command.Parameters.Add("@productPrice", SqlDbType.SmallMoney).Value = product.ProductPrice;
                //command.Parameters.AddWithValue("@stockDate", product.StockDate);
                command.Parameters.Add("@stockDate", SqlDbType.Date).Value = product.StockDate;
                //command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.Add("@stock", SqlDbType.Int).Value = product.Stock;


                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                if (ret == 1)
                {
                    conn.Close();
                    return ret;

                }
                else
                {
                    conn.Close();
                    return ret;
                }
            }

        }//EoM



        //public async Task<int> InsertProductsAsync(Guid productID, string? productName, Products pdto, string? productDetails, double? productPrice, DateTime? stockDate, int? stock)
        //{
            
        //    using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Products] (ProductID, ProductName, ProductImage, ProductDetails, ProductPrice, StockDate, Stock)  VALUES (@productID, @productName, @pdto, @productDetails, @productPrice, @stockDate, @stock)", conn))
        //    {
        //        command.Parameters.AddWithValue("@productID", productID);
        //        command.Parameters.AddWithValue("@productName", productName);
        //        command.Parameters.AddWithValue("@pdto", pdto);
        //        command.Parameters.AddWithValue("@productDetails", productDetails);
        //        command.Parameters.AddWithValue("@productPrice", productPrice);
        //        command.Parameters.AddWithValue("@stockDate", stockDate);
        //        command.Parameters.AddWithValue("@stock", stock);

        //        conn.Open();
        //        int ret = await command.ExecuteNonQueryAsync();

        //        if (ret == 1)
        //        {
        //            conn.Close();
        //            return ret;

        //        }
        //        else
        //        {
        //            conn.Close();
        //            return ret;
        //        }
        //    }

        //}//EoM




    }
}





