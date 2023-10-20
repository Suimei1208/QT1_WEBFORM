using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;
using QT1_WEB.Models.ViewModels;

namespace QT1_WEB.Pages.Order
{
    public class ListModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        public List<Models.ViewModels.OrderViewModel> Orders { get; set; }
        public ListModel(Qt1NetTechContext dbContext)
        {
             this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Orders = dbContext.Orders.Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate ?? DateTime.MinValue,
                CustId = o.CustId,
                CustomerName = o.Cust != null ? o.Cust.CustName : "Unknown"
            }).ToList();
        }

        public IActionResult OnPostDelete(int? id)
        {        
            List<Models.OrderDetail> orderDetailsToDelete = dbContext.OrderDetails.Where(od => od.OrderId == id).ToList();

            foreach (var orderDetail in orderDetailsToDelete)
            {
                dbContext.OrderDetails.Remove(orderDetail);
            }
            dbContext.SaveChanges();

            var orderToDelete = dbContext.Orders.SingleOrDefault(o => o.OrderId == id);
            if (orderToDelete != null)
            {
                dbContext.Orders.Remove(orderToDelete);
                dbContext.SaveChanges();
            }

            return RedirectToPage("");
        }

    }


}
