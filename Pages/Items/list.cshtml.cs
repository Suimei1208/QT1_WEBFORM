using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;

namespace QT1_WEB.Pages.Items
{
    public class listModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        public List<Models.Item> Items { get; set; }
        public listModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Items = dbContext.Items.ToList();
        }
    }
}
