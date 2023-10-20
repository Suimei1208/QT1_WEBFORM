using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using QT1_WEB.Models;

namespace QT1_WEB.Pages.OrderDetail
{
    public class addModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        public List<Models.Item> Items { get; set; }
        [BindProperty]
        public Models.OrderDetail OrderDetailadd { get; set; }
        public addModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Items = dbContext.Items.ToList();
            var ID = dbContext.OrderDetails.Max(o => o.Id);
            int orderID = ID + 1;
            ViewData["ID"] = orderID.ToString();
        }
        public void OnPostAdd(int id)
        {
            if (string.IsNullOrEmpty(OrderDetailadd.ItemId.ToString()))
            {
                ViewData["Message"] = "Enter again! Please watch the table";
            }
            else if(string.IsNullOrEmpty(OrderDetailadd.Quantity.ToString()) || string.IsNullOrEmpty(OrderDetailadd.UnitAmount.ToString()))
            {
                ViewData["Message"] = "Enter again";
            }
            else
            {
                var ID = dbContext.OrderDetails.Max(o => o.Id);
                int orderID = ID + 1;
                var OrderDetailsDomainModel = new Models.OrderDetail
                {
                    Id = orderID,
                    OrderId = id,
                    ItemId = OrderDetailadd.ItemId,
                    Quantity = OrderDetailadd.Quantity,
                    UnitAmount = OrderDetailadd.UnitAmount
                };
                dbContext.OrderDetails.Add(OrderDetailsDomainModel);
                dbContext.SaveChanges();
                ViewData["Message"] = "Add successfully!";
            }
        }
    }
}
