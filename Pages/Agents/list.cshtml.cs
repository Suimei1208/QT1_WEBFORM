using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;

namespace QT1_WEB.Pages.Agents
{
    public class listModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        public listModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Models.Customer> Customer { get; set; }
        public void OnGet()
        {
            Customer = dbContext.Customers.ToList();
        }
    }
}
