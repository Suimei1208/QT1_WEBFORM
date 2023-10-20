using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QT1_WEB.Models;

namespace QT1_WEB.Pages.Agents
{
    public class addModel : PageModel
    {
        private readonly Qt1NetTechContext dbContext;
        public addModel(Qt1NetTechContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public Customer CustomerRequest { get; set; }
        public void OnGet()
        {
        }
        public void OnPostAdd()
        {

            if (string.IsNullOrEmpty(CustomerRequest.CustName) || string.IsNullOrEmpty(CustomerRequest.Username) 
                || string.IsNullOrEmpty(CustomerRequest.Password) || string.IsNullOrEmpty(CustomerRequest.Address))
            {
                ViewData["Message"] = "Enter again!";
            }
            else
            {
                var existingCustomer = dbContext.Customers.FirstOrDefault(c => c.Username == CustomerRequest.Username);

                if (existingCustomer != null)
                {
                    ViewData["Message"] = "Username already exists. Please choose a different username.";
                }
                else
                {
                    int newCustomerNumber = 1;

                    var sortedData = dbContext.Customers.ToList().OrderBy(c => int.Parse(c.CustId.Substring("CustID".Length))).ToList();
                    if (sortedData != null)
                    {
                        string lastCustID = sortedData.Last().CustId;
                        string[] parts = lastCustID.Split(new string[] { "CustID" }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            if (int.TryParse(parts[1], out int lastNumber))
                            {
                                newCustomerNumber = lastNumber + 1;
                            }
                        }
                    }

                    string newCustID = "CustID" + newCustomerNumber.ToString();
                    var customerDomainModel = new Customer
                    {
                        CustId = newCustID,
                        CustName = CustomerRequest.CustName,
                        Username = CustomerRequest.Username,
                        Password = CustomerRequest.Password,
                        Address = CustomerRequest.Address
                    };
                    dbContext.Customers.Add(customerDomainModel);
                    dbContext.SaveChanges();
                    ViewData["Message"] = "Account added successfully!";
                }
            }
        }
    }
}
