using System;
using System.Collections.Generic;

namespace InvoiceTask.Models
{
    public partial class Products
    {
        public Products()
        {
            InvoiceProducts = new HashSet<InvoiceProducts>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? UnitPrice { get; set; }
        public int? AvailableQuantity { get; set; }
        public string Description { get; set; }

        public virtual ICollection<InvoiceProducts> InvoiceProducts { get; set; }
    }
}
