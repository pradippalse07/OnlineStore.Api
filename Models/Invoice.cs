namespace OnlineStore.Api.Models;
public class Invoice
    {
        public Guid Id { get; set; }

        public string OrderID { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        // Navigation
        public Order Order { get; set; }
    }