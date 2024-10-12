
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Db;
using Northwind.Models;
using Northwind.ViewModels;


namespace Northwind.Controllers
{
    public class OrderController : Controller
    {

        private readonly northwindDbContext _Db;

        private int orderId = 0;
        public OrderController(northwindDbContext context)
        {
            _Db = context;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //ordenar por columnas
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1; // Si hay una nueva búsqueda, reinicia la página a 1
            }
            else
            {
                searchString = currentFilter; // Mantén el filtro anterior
            }

            ViewData["CurrentFilter"] = searchString;

            //obtener los datos de ordenes usando entity y linq
            var orders = (from o in _Db.Orders
                          join c in _Db.Customers on o.CustomerId equals c.CustomerId
                          select new OrderVM
                          {
                              OrderId = o.OrderId,
                              OrderDate = o.OrderDate,
                              CustomerName = c.CompanyName ?? c.ContactName, // Prioriza la compañía, pero usa el nombre si no está.
                              CustomerPhone = c.Phone

                          });



            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderBy(o => o.CustomerName);
                    break;
                case "Date":
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.OrderId);
                    break;
            }

            int pageSize = 15;
            return View(await PaginatedList<OrderVM>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public IActionResult GetDetails(int id, string customer)
        {
            var ordersDetails = _Db.Orders
                                 .Include(o => o.OrderDetails)
                                 .ThenInclude(od => od.Product) // Para obtener detalles del producto
                                 .Where(o => o.OrderId == id)
                                 .Select(o => new OrderDetailsVM
                                 {
                                     OrderId = o.OrderId,
                                     OrderDate = o.OrderDate,
                                     CustomerName = customer,
                                     Total = o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),
                                     Products = o.OrderDetails.Select(od => new OrderProductVM
                                     {
                                         ProductId = od.ProductId,
                                         ProductName = od.Product.ProductName,
                                         Quantity = od.Quantity,
                                         UnitPrice = od.UnitPrice,
                                         Total = od.Quantity * od.UnitPrice
                                     }).ToList(),
                                     IsConfirmed = o.IsConfirmed,
                                     Comments = o.Comments,
                                     ConfirmationDate = o.ConfirmationDate
                                 }).FirstOrDefault();

            return Json(ordersDetails);
        }

        [HttpPost]
        public IActionResult UpdateConfirm(int orderId, bool isConfirmed, string comments, DateTime? confirmationDate)
        {
            var orderDb = _Db.Orders.Find(orderId);
            if (orderDb != null)
            {
                orderDb.IsConfirmed = isConfirmed; // Recibe true o false dependiendo del estado del checkbox
                orderDb.Comments = comments;
                orderDb.ConfirmationDate = confirmationDate;
                _Db.Orders.Update(orderDb);
                _Db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
