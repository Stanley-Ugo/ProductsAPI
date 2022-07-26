using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.ServiceLayer.IServices;
using ProductsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProducts()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var result = _productService.GetAllProductService();

            return Ok(result);
        }

        [HttpGet("GetProductById/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            try
            {
                var response = _productService.GetProductByIdService(id);

                if (response != null)
                {
                    return Ok(new { responseCode = 200, Data = response });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { responseCode = 400, responseDescription = ex.Message });
            }
            
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProductAsync([FromBody]ProductViewModel product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        var resonse = _productService.CreateProductService(product);

                        if (resonse == true)
                        {
                            return Ok(new { responseCode = 201, responseDescription = "Product Created Successfully!", Data = product });
                        }

                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }

            return BadRequest(new { Message = "Incorrect Product Creation Data!!" });
        }

        [HttpPut("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductViewModel product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        var resonse = _productService.UpdateProductService(product);

                        if (resonse == true)
                        {
                            return Ok(new { responseCode = 201, responseDescription = $"Product with Id { product.Id } Updated Successfully!", Data = product });
                        }

                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }

            return BadRequest(new { Message = "Incorrect Product Update Data!!" });
        }

        [Route("DisableProduct/{id:int}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DisableProductAsync(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var product = _productService.DisableProductByIdService(id);

            if (product == null)
                return NotFound();

            return Ok(new { responseCode = 200, responseDescription = $"Product Disabled Successful", Data = product });
        }

        [Route("DeleteProduct/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var product = _productService.DeleteProductByIdService(id);

            if (product == null) 
                return NotFound();

            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful", Data = product});
        }

        [Route("SumOfProductsInAWeek")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SumOfProductsInAWeekAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var sumOfProducts = _productService.sumOfProductsInAWeekService();

            if (sumOfProducts == 0)
                return Ok(new { responseCode = 200, responseDescription = "No Product Added in the last seven(7) days", Data = sumOfProducts });

            return Ok(new { responseCode = 200, responseDescription = "Computed Successful", Data = sumOfProducts });
        }

        [HttpGet("GetAllDisabledProducts")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDisabledProductsAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var result = _productService.GetAllDisabledProductService();

            if(result.Count() == 0)
                return NotFound(new { responseCode = 404, responseDescription = "Records Not Found!", Data = result });

            return Ok(new { responseCode = 200, responseDescription = "Operation Successful", Data = result });
        }

        [HttpGet("GetAllNonDisabledProducts")]
        [Authorize]
        public async Task<IActionResult> GetAllNonDisabledProductsAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var accessTokenValue = HttpContext.Session.GetString(accessToken);

            if (accessTokenValue != "isValid")
                return Unauthorized();

            var result = _productService.GetAllNonDisabledProductService();

            if (result.Count() == 0)
                return NotFound(new { responseCode = 404, responseDescription = "Records Not Found!", Data = result });

            return Ok(new { responseCode = 200, responseDescription = "Operation Successful", Data = result });
        }
    }
}
