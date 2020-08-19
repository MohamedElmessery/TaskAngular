using System;
using System.Collections.Generic;

namespace InvoiceTask.Models
{
    public partial class InvoiceProducts
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? InvoiceId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Products Product { get; set; }
    }
}
