using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.DTOs;
using ProductsApi.Models;
using ProductsApi.Repositories;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // get single product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.Get(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        // create new product
        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            Product product = new()
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Created = DateTime.Now
            };
            await _productRepository.Add(product);
            return Ok();
        }

        // delete target product by id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productRepository.Delete(id);
            return Ok();
        }

        // update specific product by id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDTO updateProductDTO){
            var product = await _productRepository.Get(id);
            product.ProductId = id;
            product.Name = updateProductDTO.Name;
            product.Price = updateProductDTO.Price;
            await _productRepository.Update(product);
            return Ok(product);
        }
    }
}