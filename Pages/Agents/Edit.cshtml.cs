using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;
using QT1_WEB.Models.ViewModels;

namespace QT1_WEB.Pages.Agents
{
    public class EditModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;

        [BindProperty]
        public Customer Customers { get; set; }
        public EditModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            var cust = dbContext.Customers.Find(id);
            if (cust != null)
            {
                Customers = new Customer
                {
                    CustId = cust.CustId,
                    CustName = cust.CustName,
                    Address = cust.Address,
                };
            }
            else
            {
                ViewData["Message"] = "Not Found!";
            }
        }
        public void OnPostUpdate()
        {
            if (Customers != null)
            {
                var existingCust = dbContext.Customers.Find(Customers.CustId);
                if (existingCust != null)
                {
                    existingCust.CustName = Customers.CustName;
                    existingCust.Address = Customers.Address;
                }
                dbContext.SaveChanges();
                ViewData["Message"] = "Edit successfully!";
            }
        }

        public IActionResult OnPostDelete()
        {
            var existingCust = dbContext.Customers.Find(Customers.CustId);
            if (existingCust != null)
            {
                dbContext.Customers.Remove(existingCust);
                dbContext.SaveChanges();
                return RedirectToPage("/Agents/list");
            }
            return Page();
        }
    }
}
