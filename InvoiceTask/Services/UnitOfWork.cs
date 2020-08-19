using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceTask.Models;
using InvoiceTask.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace InvoiceTask.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly invoicTaskDBContext Context;
        private IInvoiceRepository _invoiceRepository;
        private IProductsRepository _productsRepository;
        private IInvoiceProductsRepository _invoiceProductsRepository;
        public IProductsRepository productsRepository
        {
            get
            {
                return _productsRepository = _productsRepository ?? new ProductsRepository(Context);
            }
        }

        public IInvoiceProductsRepository invoiceProductsRepository
        {
            get
            {
                return _invoiceProductsRepository = _invoiceProductsRepository ?? new InvoiceProductsRepository(Context);
            }
        }

        public IInvoiceRepository invoiceRepository
        {
            get
            {
                return _invoiceRepository = _invoiceRepository ?? new InvoiceRepository(Context);
            }
        }



        public UnitOfWork(invoicTaskDBContext context)
        {

            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
