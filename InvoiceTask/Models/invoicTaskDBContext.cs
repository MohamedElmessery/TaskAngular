using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InvoiceTask.Models
{
    public partial class invoicTaskDBContext : DbContext
    {
        public invoicTaskDBContext()
        {
        }

        public invoicTaskDBContext(DbContextOptions<invoicTaskDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceProducts> InvoiceProducts { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=invoicTaskDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<InvoiceProducts>(entity =>
            {
                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceProducts)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_invoiceId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_productId");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.ProductName).HasMaxLength(30);
            });
        }
    }
}
