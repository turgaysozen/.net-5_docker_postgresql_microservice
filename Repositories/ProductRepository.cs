using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;

namespace ProductsApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataContext _context;
        public ProductRepository(IDataContext context)
        {
            _context = context;

        }

        // create single product
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // delete target product by id
        public async Task Delete(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new NullReferenceException();
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        // find specific product by id
        public async Task<Product> Get(int id)
        {
             return await _context.Products.FindAsync(id);
        }

        // get all product list
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        // update target product
        public async Task Update(Product product)
        {
            var item = await _context.Products.FindAsync(product.ProductId);
            if (item == null) throw new NullReferenceException();
            item.Name = product.Name;
            item.Price = product.Price;
            await _context.SaveChangesAsync();
        }
    }
}