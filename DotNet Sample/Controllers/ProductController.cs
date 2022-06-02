﻿using DotNet_Sample.Controllers.Service;
using DotNet_Sample.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DotNet_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ODataController
    {
        private readonly IProductService ProductService;

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        [EnableQuery]
        [ProducesResponseType(typeof(IList<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await ProductService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [EnableQuery]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var product = await ProductService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product.Id == default)
                product.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await ProductService.AddProductAsync(product);

            return CreatedAtAction("Add", new { id = product.Id }, product);
        }
    }
}
