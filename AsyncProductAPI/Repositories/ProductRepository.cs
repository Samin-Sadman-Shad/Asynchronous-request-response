using AsyncProductAPI.Models;
using AsyncProductAPI.Persistance;

namespace AsyncProductAPI.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProducts(ListProduct products)
        {
            await _context.AddAsync(products);
            await _context.SaveChangesAsync();
        }

        public async Task<ListProduct> GetProducts()
        {
            var products = await _context.FindAsync<ListProduct>();
            return products;
        }

        public async Task<ListProduct> GetProductsByRequestId(Guid RequestId)
        {
            var product = _context.Products.FirstOrDefault(lp => lp.RequestId == RequestId);
            return product;
        }
    }
}
