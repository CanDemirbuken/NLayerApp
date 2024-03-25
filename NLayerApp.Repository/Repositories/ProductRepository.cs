using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Product>> GetProductsWithCategory()
        {
            // Eager Loading --> Product'a ait verileri çektiğimizde category verilerini de alırsak eager loading olur.
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
