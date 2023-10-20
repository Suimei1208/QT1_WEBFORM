using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models.ViewModels;
using QT1_WEB.Models;

namespace QT1_WEB.Pages.Order
{
    public class addModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;

        public addModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Models.Order OrderRequest { get; set; }


        public void OnGet()
        {
            
        }
        public void OnPostAdd()
        {
            if (string.IsNullOrEmpty(OrderRequest.CustId))
            {
                ViewData["Message"] = "Enter again! Customer Id follow format (Ex:CustId1)";
            }
            else
            {
                var maxOrderID = dbContext.Orders.Max(o => o.OrderId);
                int orderID = maxOrderID + 1;
                var OrderDomainModel = new Models.Order
                {
                    OrderId = orderID,
                    OrderDate = DateTime.Now,
                    CustId = OrderRequest.CustId
                };               

                dbContext.Orders.Add(OrderDomainModel);
                dbContext.SaveChanges();

                ViewData["Message"] = "Add successfully!";
            }
        }
    }
}
