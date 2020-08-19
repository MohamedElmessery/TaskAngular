using System;
using System.Collections.Generic;

namespace InvoiceTask.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceProducts = new HashSet<InvoiceProducts>();
        }

        public int InvoiceId { get; set; }
        public int? TotalAmount { get; set; }

        public virtual ICollection<InvoiceProducts> InvoiceProducts { get; set; }
    }
}
