using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class OrderM
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public bool IsConfirmed { get; set; }
        public string? Comments { get; set; }
        public DateTime? ConfirmationDate { get; set; }

        //public List<OrderDetailModel> OrderDetails { get; set; } // Detalles de la orden
    }
}

