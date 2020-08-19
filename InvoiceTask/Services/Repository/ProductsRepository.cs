using InvoiceTask.Models;
using InvoiceTask.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTask.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly invoicTaskDBContext _context;

        public ProductsRepository(invoicTaskDBContext context)
        {
            _context = context;
        }

        public void EditProduct(Products product)
        {
            var oldProduct = _context.Products.SingleOrDefault(x => x.ProductId == product.ProductId);
            oldProduct.UnitPrice = product.UnitPrice;
            oldProduct.AvailableQuantity = product.AvailableQuantity;
        }

        public IEnumerable<Products> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Products> GetProductById(int id)
        {
            return _context.Products.Where(x => x.ProductId == id).ToList();
        }
    }
}
