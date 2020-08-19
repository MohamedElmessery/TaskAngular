using InvoiceTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceTask.Services.IServices;
namespace InvoiceTask.Services
{
   public interface IUnitOfWork : IDisposable
    {
        IProductsRepository productsRepository { get; }
        IInvoiceProductsRepository invoiceProductsRepository { get; }
        IInvoiceRepository invoiceRepository { get; }
        //DbContext Context { get; }
        void Save();
    }
}
