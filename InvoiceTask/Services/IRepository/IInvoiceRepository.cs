using InvoiceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTask.Services.IServices
{
   public interface IInvoiceRepository
    {
         void AddInvoice(Invoice invoice);
         IEnumerable<Invoice> GetInvoices();
        int? GetInvoiceById(int id);
        void DeleteInvoice(int id);

    }
}
