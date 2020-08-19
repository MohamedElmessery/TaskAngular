using InvoiceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTask.Services.IServices
{
   public interface IProductsRepository
    {
        IEnumerable<Products> GetProducts();
        void EditProduct(Products product);
        List<Products> GetProductById(int id);
    }
}
