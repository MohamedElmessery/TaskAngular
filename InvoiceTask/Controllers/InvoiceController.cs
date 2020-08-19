using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceTask.Services;
using InvoiceTask.Services.IServices;
using InvoiceTask.Models;
using Newtonsoft.Json;

namespace InvoiceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetInvoiceById/{id}")]
        public object GetInvoiceById(int id)
        {
            
            var products = _unitOfWork.productsRepository.GetProducts();
            var invoices = _unitOfWork.invoiceRepository.GetInvoices();
            var invoicesProucts = _unitOfWork.invoiceProductsRepository.GetInvoicesProducts();
            //Inner join With three tables products,invoicesProucts,invoice
            var result = (from product in products
                        join invoicesProuct in invoicesProucts on product.ProductId equals invoicesProuct.ProductId
                        join invoice in invoices on invoicesProuct.InvoiceId equals invoice.InvoiceId
                        where invoice.InvoiceId == id
                        select new {
                            ItemId =product.ProductId,
                            ItemName=product.ProductName,
                            Descrption=product.Description,
                            product.UnitPrice,
                            Quentity= invoicesProuct.Quantity,
                           invoicesProuct.Total,
                            invoice.TotalAmount,
                            invoice.InvoiceId
                        }).ToList();

            return result;
        }


        [HttpGet]
        [Route("GetProducts")]
        public ActionResult<IEnumerable<Products>> GetProducts()
        {
            var result = _unitOfWork.productsRepository.GetProducts();
            return result.ToList();
        }
        [HttpPost]
        [Route("AddInvoice")]
        public object AddInvoice(InvoiceItemsVm model)
        {
            Invoice invoice = new Invoice();
            invoice.TotalAmount = model.TotalAmount;
            //first add TotalAmount in invoice table
            _unitOfWork.invoiceRepository.AddInvoice(invoice);
            _unitOfWork.Save();
            //get last id of invoice 
            int inVoiceId = _unitOfWork.invoiceRepository.GetInvoices().Last().InvoiceId;
            // map invoice id with invoicproducts array
            model.InvoiceProducts.ForEach(item => item.InvoiceId = inVoiceId);
            // add array of invoice products in invoicproducts table
            _unitOfWork.invoiceProductsRepository.AddInvoiceProducts(model.InvoiceProducts);
            _unitOfWork.Save();
            // return invoice datails to user 
            var result = GetInvoiceById(inVoiceId);
            return result;
        }
        [HttpDelete]
        [Route("DeleteInvoice/{id}")]
        public int DeleteInvoice(int id)
        {
            try
            {
                _unitOfWork.invoiceProductsRepository.DeleteInvoiceProducts(id);
                _unitOfWork.Save();
                _unitOfWork.invoiceRepository.DeleteInvoice(id);
                _unitOfWork.Save();
               
                return 1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        [HttpPut]
        [Route("EditProduct")]
        public int EditProduct(Products product)
        {
            try
            {
                _unitOfWork.productsRepository.EditProduct(product);
                _unitOfWork.Save();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        [HttpGet]
        [Route("GeProductById/{id}")]
        public List<Products> GeProductById(int id)
        {
            return _unitOfWork.productsRepository.GetProductById(id);
        }
        public class InvoiceItemsVm
        {
            public List<InvoiceProducts> InvoiceProducts { get; set; }
            public int TotalAmount { get; set; }
        }
    }
}