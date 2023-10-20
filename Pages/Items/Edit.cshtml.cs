using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;
using QT1_WEB.Models.ViewModels;

namespace QT1_WEB.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;

        [BindProperty]
        public Edit_Item Item { get; set; }
        public EditModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int id)
        {
            var item = dbContext.Items.Find(id);
            if (item != null)
            {
                Item = new Edit_Item
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Size = item.Size
                };
            }
            else
            {
                ViewData["Message"] = "Not Found!";
            }        
        }
        public void OnPostUpdate()
        {
            if(Item != null)
            {
                var existingItem = dbContext.Items.Find(Item.ItemId);
                if (existingItem != null)
                {
                    existingItem.ItemName = Item.ItemName;
                    existingItem.Size = Item.Size;
                }
                dbContext.SaveChanges();
                ViewData["Message"] = "Edit successfully!";
            }
        }

        public IActionResult OnPostDelete()
        {
            var existingItem = dbContext.Items.Find(Item.ItemId);
            if (existingItem != null)
            {
                dbContext.Items.Remove(existingItem);
                dbContext.SaveChanges();
                return RedirectToPage("/Items/list");
            }
            return Page();
        }
    }
}
