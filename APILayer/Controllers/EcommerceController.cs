using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using RestSharp;

namespace APILayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EcommerceController : ControllerBase
    {
        private readonly IBus _bus;

        public EcommerceController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string?>> LoginAsync(LoginDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            User? u = await this._bus.LoginAsync(request);

            if(u == null)
            {
                return Unauthorized("Wrong username or password");
            }

            var client = new RestClient("https://dev-u4nrg-wp.us.auth0.com/oauth/token");
            var request1 = new RestRequest("Post");
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", "{\"client_id\":\"smRz6cCOnyMgRDv4d5g3HAYM1pwBC06W\",\"client_secret\":\"gsxgugySdkMUEpaAsHkqiSMLuqIigeHF1HtUjvLMY21Er8gdP7Oj_Wd__RImRwyP\",\"audience\":\"https://localhost:7231/Ecommerce\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            RestResponse response = client.Execute(request1);

            return Ok(response);
        }

        [HttpGet("user/{username}")]
        public async Task<ActionResult<string?>> GetUserInfoAsync(string username)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            UserInfoDto? u = await this._bus.GetUserInfoAsync(username);

            if(u == null)
            {
                return NotFound("No user by that username");
            }

            return Ok(u);
        }

        [HttpGet("user/{username}/photo")]
        public async Task<ActionResult<Stream?>> GetUserPhotoAsync(string username)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Stream? p = await this._bus.GetUserPhotoAsync(username);

            if(p == null)
            {
                return NotFound("Photo not found");
            }

            return File(p, "image/png");
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserInfoDto?>> RegisterAsync([FromForm]RegisterDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            UserInfoDto? u = await this._bus.RegisterNewUserAsync(request);

            if (u?.ErrorMessage != String.Empty)
            {
                return Unauthorized(u?.ErrorMessage);
            }

            return Created($"/user/{request.Username}", u);
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<Product?>>> GetAllProductsAsync()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            List<Product?> p = await this._bus.GetAllProductsAsync();

            return Ok(p);
        }

        [HttpGet("product/{productID}/image")]
        public async Task<ActionResult<Stream?>> GetProductImageAsync(Guid? productID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Stream? p = await this._bus.GetProductImageAsync(productID);

            if(p == null)
            {
                return NotFound("Image not found");
            }

            return File(p, "image/png");
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<Order?>> CreateOrderAsync(Guid? userID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Order? o = await this._bus.CreateOrderAsync(userID);

            if(o == null)
            {
                return Unauthorized("Unable to create order");
            }

            return Created($"my-orders/{o.OrderID}", o);
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<List<Order?>>> GetMyOrdersAsync(Guid? userID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            List<Order?> o = await this._bus.GetMyOrdersAsync(userID);

            return Ok(o);
        }

        [HttpGet("my-orders/{orderID}")]
        public async Task<ActionResult<SingleOrderDto?>> GetOrderAsync(Guid? orderID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            SingleOrderDto? o = await this._bus.GetOrderAsync(orderID);

            if (o == null)
            {
                return Unauthorized("Unable to find order");
            }

            return Ok(o);
        }

        [HttpGet("my-cart")]
        public async Task<ActionResult<MyCartDto?>> GetMyCartAsync(Guid? userID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            MyCartDto? c = await this._bus.GetMyCartAsync(userID);

            if (c == null)
            {
                return Unauthorized("Unable to find cart");
            }

            return Ok(c);
        }

        [HttpPost("my-cart/addItem/{productID}")]
        public async Task<ActionResult<MyCartDto?>> AddProductToCartAsync(Guid? userID, Guid? productID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            MyCartDto? c = await this._bus.AddProductToCartAsync(userID, productID);

            if (c==null)
            {
                return Unauthorized("Unable to add item to cart");
            }

            return Ok(c);
        }

        // TODO: Fix seed to add up all productPrice in cart per user to get cartTotal
        // TODO: Same for orders
        // TODO: Fix cartItems = Count how many products in CartsProducts by cartID
        // TODO: Fix orders, add orderItems = ^ in OrdersProducts by orderID
        // use triggers?
    }
}