using InvoiceTask.Models;
using InvoiceTask.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTask.Services
{
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly invoicTaskDBContext _context;
        public InvoiceRepository(invoicTaskDBContext context )
        {
            _context = context;
        }

        public void AddInvoice(Invoice invoice)
        {
            _context.Invoice.Add(invoice);
        }
        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoice.ToList();
        }
        public int? GetInvoiceById(int id)
        {
            return _context.Invoice.SingleOrDefault(a=>a.InvoiceId==id).TotalAmount;
        }

        public void DeleteInvoice(int id)
        {
            var invoice = _context.Invoice.SingleOrDefault(x => x.InvoiceId == id);
            //if (invoice != null)
                _context.Invoice.Remove(invoice);
        }
    }
}
