using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;
using QT1_WEB.Models.ViewModels;

namespace QT1_WEB.Pages.OrderDetail
{
    public class listModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        [BindProperty]
        public List<Models.ViewModels.OrderDetalsViewModel> OrderDetailViewModel { get; set; }
        int clone;
        public listModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet(int id)
        {
            clone = id;
            OrderDetailViewModel = dbContext.OrderDetails
                .Where(o => o.OrderId == id)
                .Select(o => new OrderDetalsViewModel
                {
                    Id = o.Id,
                    OrderId = id,
                    Quantity = o.Quantity,
                    UnitAmount = o.UnitAmount,
                    ItemId = o.ItemId,
                    ItemName = o.Item != null ? o.Item.ItemName : "Unknown"
                })
                .ToList();
            ViewData["Message"] = clone.ToString();
        }
        public IActionResult OnPostDelete(int id)
        {
            var existingItem = dbContext.OrderDetails.Find(id);
            if (existingItem != null)
            {
                dbContext.OrderDetails.Remove(existingItem);
                dbContext.SaveChanges();
            }
            OnGet(clone);
            return Page();
        }
    }
}
