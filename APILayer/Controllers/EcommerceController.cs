using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;


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

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<UserInfoDto>> GetUserInfoAsync()
        {
            // Console.WriteLine(HttpContext.Request.Headers["Authorization"]);

            string? auth0id = User.Identity?.Name;
          
            UserInfoDto? u = await this._bus.GetUserInfoAsync(auth0id, User.Claims);

            if(u == null)
            {
                return NotFound("No user by that username");
            }

            return Ok(u);
        }

        [HttpPut("user")]
        [Authorize]
        public async Task<ActionResult<UserProfile?>> UpdateUserInfoAsync(UpdateUserInfoDto request)
        {
            string? auth0id = User.Identity?.Name;

            UserProfile? u = await this._bus.UpdateUserInfoAsync(auth0id, request);

            if(u==null)
            {
                return BadRequest();
            }

            return Ok(u);
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<Product?>>> GetAllProductsAsync()
        {
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
        [Authorize]
        public async Task<ActionResult<Order?>> CreateOrderAsync()
        {
            string? auth0id = User.Identity?.Name;

            Order? o = await this._bus.CreateOrderAsync(auth0id);

            if(o == null)
            {
                return BadRequest(new {message = "Order failed to process"});
            }

            return Ok(new {message = "Order completed! Thank for shopping with us."});
        }

        [HttpGet("my-orders")]
        [Authorize]
        public async Task<ActionResult<List<Order?>>> GetMyOrdersAsync()
        {
            string? auth0id = User.Identity?.Name;

            List<Order?> o = await this._bus.GetMyOrdersAsync(auth0id);

            return Ok(o);
        }

        [HttpGet("my-orders/{orderID}")]
        [Authorize]
        public async Task<ActionResult<SingleOrderDto?>> GetOrderAsync(Guid? orderID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("There was an error with the model");
            }

            SingleOrderDto? o = await this._bus.GetOrderAsync(orderID);

            if (o == null)
            {
                return BadRequest("Unable to find order");
            }

            return Ok(o);
        }

        [HttpGet("my-cart")]
        [Authorize]
        public async Task<ActionResult<MyCartDto?>> GetMyCartAsync()
        {
            string? auth0id = User.Identity?.Name;

            MyCartDto? c = await this._bus.GetMyCartAsync(auth0id);

            if (c == null)
            {
                return Unauthorized("Unable to find cart");
            }

            return Ok(c);
        }

        [HttpPost("my-cart/addItem")]
        [Authorize]
        public async Task<ActionResult<Cart?>> AddProductToCartAsync(Guid? productID, int count)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            string? auth0id = User.Identity?.Name;

            Cart? c = await this._bus.AddProductToCartAsync(auth0id, productID, count);

            if (c==null)
            {
                return BadRequest(c);
            }

            return Ok(c);
        }

        [HttpPost("my-cart/deleteItem")]
        [Authorize]
        public async Task<ActionResult<Cart?>> DeleteProductFromCartAsync(Guid? productID, int count)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            string? auth0id = User.Identity?.Name;

            Cart? c = await this._bus.DeleteProductFromCartAsync(auth0id, productID, count);

            if (c==null)
            {
                return BadRequest(c);
            }

            return Ok(c);
        }

        // TODO: Fix seed to add up all productPrice in cart per user to get cartTotal
        // TODO: Same for orders
        // TODO: Fix cartItems = Count how many products in CartsProducts by cartID
        // TODO: Fix orders, add orderItems = ^ in OrdersProducts by orderID
        // use triggers?
        // TODO: fix cart total in database (reading 0 when items are added)
        // TODO: fix my orders page to make the cards say count and total
    }
}