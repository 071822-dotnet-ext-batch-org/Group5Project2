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

        [HttpPost("login")]
        [Authorize]
        public async Task<ActionResult<string?>> LoginAsync()
        {
            string? auth0id = User.Identity?.Name;

            User? u = await this._bus.LoginAsync(auth0id);

            if(u == null)
            {
                return NotFound("User does not exist.");
            }

            return Ok(u);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<string?>> GetUserInfoAsync()
        {
            string? auth0id = User.Identity?.Name;

            UserInfoDto? u = await this._bus.GetUserInfoAsync(auth0id);

            if(u == null)
            {
                return NotFound("No user by that username");
            }

            return Ok(u);
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<ActionResult<UserInfoDto?>> RegisterAsync()
        {
            string? auth0id = User.Identity?.Name;
            string? name = User.Claims.FirstOrDefault(c => c.Type.Equals("myinfo/name"))?.Value;
            string? email = User.Claims.FirstOrDefault(c => c.Type.Equals("myinfo/email"))?.Value;
            string? picture = User.Claims.FirstOrDefault(c => c.Type.Equals("myinfo/picture"))?.Value;

            UserInfoDto? u = await this._bus.RegisterNewUserAsync(auth0id, name, email, picture);

            if (u?.ErrorMessage != String.Empty)
            {
                return Unauthorized(u?.ErrorMessage);
            }

            return Created($"/my-profile", u);
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
                return Unauthorized("Unable to create order");
            }

            return Created($"my-orders/{o.OrderID}", o);
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

        [HttpPost("my-cart/addItem/{productID}")]
        [Authorize]
        public async Task<ActionResult<MyCartDto?>> AddProductToCartAsync(Guid? productID)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            string? auth0id = User.Identity?.Name;

            MyCartDto? c = await this._bus.AddProductToCartAsync(auth0id, productID);

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