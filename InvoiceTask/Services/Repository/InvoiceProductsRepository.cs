using InvoiceTask.Models;
using InvoiceTask.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTask.Services
{
    public class InvoiceProductsRepository: IInvoiceProductsRepository
    {

        private readonly invoicTaskDBContext _context;
        public InvoiceProductsRepository(invoicTaskDBContext context)
        {
            _context = context;
        }

        public void AddInvoiceProducts(List<InvoiceProducts> invoiceProducts)
        {
            _context.InvoiceProducts.AddRange(invoiceProducts);
           
        }

        public void DeleteInvoiceProducts(int id)
        {
            var invoiceProducts = _context.InvoiceProducts.Where(x => x.InvoiceId == id).ToList();
            //if (invoiceProducts != null)
                _context.InvoiceProducts.RemoveRange(invoiceProducts);
        }

        public IEnumerable<InvoiceProducts> GetInvoicesProducts()
        {
            return _context.InvoiceProducts.ToList();
        }
        
    }
}
