﻿using System.Data.SqlClient;
using System;
using Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Drawing;





namespace RepoLayer
{
    public class StoreFrontRepoLayer
    {
       

        private static readonly SqlConnection conn = new SqlConnection("Server=tcp:emma1.database.windows.net,1433;Initial Catalog=Project1;Persist Security Info=False;User ID=nwaodec79;Password=ECNsoftware_2212;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        //Insert product into inventory
        public async Task<int> InsertProductsAsync(Products product, byte[]? Imagebyte)
        {

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




        //Get Product by ID

        public async Task<ProductDto?> GetProductByIdAsync(Guid productID)
        {
      
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
                    MemoryStream memoryStream = new MemoryStream(Imagebyte);
                    p.ProductImage = Image.FromStream(memoryStream);

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



        //Register New Users
        public async Task<int> Register(UserProfile userprofile, byte[]? UserImagebyte)
        {

            using (SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Products] (ProductID, ProductName, ProductImage, ProductDetails, ProductPrice, StockDate, Stock)  VALUES (@productID, @productName, @productImage, @productDetails, @productPrice, @stockDate, @stock)", conn))
            {

                command.Parameters.Add("@productID", SqlDbType.UniqueIdentifier).Value = userprofile.ProfileID;
                command.Parameters.Add("@productName", SqlDbType.VarChar).Value = userprofile.ProfileName;
                command.Parameters.Add("@productName", SqlDbType.VarChar).Value = userprofile.ProfileAddress;
                command.Parameters.Add("@productName", SqlDbType.VarChar).Value = userprofile.ProfilePhone;
                command.Parameters.Add("@productImage", SqlDbType.VarBinary).Value = UserImagebyte;
                command.Parameters.Add("@productID", SqlDbType.UniqueIdentifier).Value = userprofile.fk_UserID;
                

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



    }

}





