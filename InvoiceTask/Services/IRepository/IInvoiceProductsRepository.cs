using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceTask.Models;

namespace InvoiceTask.Services.IServices
{
   public interface IInvoiceProductsRepository
    {
         void AddInvoiceProducts(List<InvoiceProducts> invoiceProducts);
        IEnumerable<InvoiceProducts> GetInvoicesProducts();
        void DeleteInvoiceProducts(int id);
    }
}
