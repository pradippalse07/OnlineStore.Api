namespace OnlineStore.Api.ViewModels;
using OnlineStore.Api.Models;
public class CreateInvoiceViewModel
    {
        
        public int OrderID { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        // Navigation
        public Order Order { get; set; }
    }


//     //CreateInvoiceViewModel.cs
// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using OnlineStore.Api.Models;
// namespace OnlineStore.Api.ViewModels;
// {
//     public class CreateInvoiceViewModel
//     {
//         [Required]
//         public Guid UserId { get; set; }

//         [Required]
//         public List<InvoiceItemViewModel> Items { get; set; }
//     }

//     public class InvoiceItemViewModel
//     {
//         [Required]
//         public Guid ProductId { get; set; }

//         [Required]
//         [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
//         public int Quantity { get; set; }
//     }
// }