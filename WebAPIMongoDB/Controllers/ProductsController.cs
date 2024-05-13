using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMongoDB.Models;
using WebAPIMongoDB.Service;

namespace WebAPIMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            return Ok(await _productService.GetAsync());
        }

        [HttpPost]
        public async Task<Products> PostProduct(Products products) {
            await _productService.CreateAsync(products);
            return products;
        }
    }
}
