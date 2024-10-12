namespace Northwind.ViewModels
{
    public class OrderDetailsVM
    {

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }

       
        public List<OrderProductVM> Products { get; set; }

        public bool IsConfirmed { get; set; }
        public string? Comments { get; set; }
        public DateTime? ConfirmationDate { get; set; }
    }
}
