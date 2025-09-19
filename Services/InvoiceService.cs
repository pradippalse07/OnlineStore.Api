// using Microsoft.EntityFrameworkCore;
// using OnlineStore.Api.Models;

// namespace OnlineStore.Api.Services;

// public class InvoiceService
// {
//     private readonly StoreContext _context;

//     public InvoiceService(StoreContext context)
//     {
//         _context = context;
//     }

//     public async Task<Invoice> CreateInvoiceAsync(int orderId, decimal totalAmount)
//     {
//         using var transaction = await _context.Database.BeginTransactionAsync();
//         try
//         {
//             // Load the order with its items
//             var order = await _context.Orders
//                 .Include(o => o.OrderItems)
//                 .FirstOrDefaultAsync(o => o.Id == orderId);

//             if (order == null)
//             {
//                 throw new Exception($"Order not found: {orderId}");
//             }

//             // Create the invoice
//             var invoice = new Invoice
//             {
//                 Id = Guid.NewGuid(),
//                 OrderID = order.Id,
//                 InvoiceDate = DateTime.UtcNow,
//                 TotalAmount = totalAmount,
//                 Order = order
//             };

//             _context.Invoices.Add(invoice);
//             await _context.SaveChangesAsync();
//             await transaction.CommitAsync();

//             return invoice;
//         }
//         catch
//         {
//             await transaction.RollbackAsync();
//             throw;
//         }
//     }

//     //get service
//     public async Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId)
//     {
//         var invoice = await _context.Invoices
//             .Include(i => i.Order)
//             .ThenInclude(o => o.OrderItems)
//             .FirstOrDefaultAsync(i => i.Id == invoiceId);

//         if (invoice == null)
//         {
//             throw new Exception($"Invoice not found: {invoiceId}");
//         }

//         return invoice;
//     }

// }