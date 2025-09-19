using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models;

namespace OnlineStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly StoreContext _context;

    public InvoiceController(StoreContext context)
    {
        _context = context;
    }

    // GET: api/invoice/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
    {
        //call GetInvoiceByIdAsync service from InvoiceService
        var invoice = await _context.Invoices
            .Include(i => i.Order)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }
        return invoice;

    }

    //create Invoice
    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoice(string orderId, decimal totalAmount)
    {
        //call CreateInvoiceAsync service from InvoiceService
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id.ToString() == orderId);

        if (order == null)
        {
            return NotFound($"Order not found: {orderId}");
        }

        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            OrderID = order.Id.ToString(),
            InvoiceDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Order = order
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
    }
}