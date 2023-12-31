﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Marketplace.Models;
using Task1_Marketplace.Services;

namespace Task1_Marketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchProductsAsync([FromBody] SearchProductRequest request)
        {
            var result = await _productService.SearchProductsAsync(request.SearchText);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
            {
                return BadRequest($"Unable to find product with id of {id}");
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProductAsync(AddProductRequest request)
        {
            await _productService.AddProductAsync(request);
            return Ok();
        }
    }
}
