using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;
using QT1_WEB.Models.ViewModels;
using System.Drawing;

namespace QT1_WEB.Pages.Items
{
    public class addModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;

        public addModel(Qt1NetTechContext dbContext) {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Add_Item ItemsRequest { get; set; }
        public void OnGet()
        {
        }

        public void OnPostAdd() {
            if (string.IsNullOrEmpty(ItemsRequest.ItemName) || string.IsNullOrEmpty(ItemsRequest.Size))
            {
                ViewData["Message"] = "Enter again!";
            }
            else
            {
                var ItemsDomainModel = new Item
                {
                    ItemName = ItemsRequest.ItemName,
                    Size = ItemsRequest.Size
                };
                dbContext.Items.Add(ItemsDomainModel);
                dbContext.SaveChanges();

                ViewData["Message"] = "Add successfully!";
            }           
        }
    }
}
