using Northwind.Models;

namespace Northwind.ViewModels
{
    public class OrderVM
    {
        public List<OrderM> Orders { get; set; }
        public OrderM Order { get; set; }
        public int OrderId { get; set; } 
        public DateTime? OrderDate { get; set; } 
        public string? CustomerName { get; set; } 
        public string? CustomerPhone { get; set; }
        public bool IsConfirmed { get; set; }
        public string? Comments { get; set; }
        public DateTime? ConfirmationDate { get; set; }
    }

}