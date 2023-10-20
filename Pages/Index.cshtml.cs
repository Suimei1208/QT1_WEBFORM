using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QT1_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QT1_WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Qt1NetTechContext _context;

        public IndexModel(ILogger<IndexModel> logger, Qt1NetTechContext context)
        {
            _logger = logger;
            _context = context;
        }
        public List<CustomerSummary> BestCustomers { get; set; }
        public List<ItemSummary> TopItems { get; set; }

        public void OnGet()
        {
            FilterBestItems();
            FindBestCustomers();
        }

        private void FilterBestItems()
        {
            var query = _context.OrderDetails
                .GroupBy(od => od.ItemId)
                .Select(group => new
                {
                    ItemID = group.Key ?? 0,
                    TotalQuantity = group.Sum(od => od.Quantity)
                })
                .OrderByDescending(item => item.TotalQuantity)
                .Take(10)
                .ToList();

            List<ItemSummary> bestSellingItemsList = new List<ItemSummary>();

            foreach (var item in query)
            {
                var itemName = _context.OrderDetails
                    .Where(od => od.ItemId == item.ItemID)
                    .Select(od => od.Item.ItemName)
                    .FirstOrDefault();

                ItemSummary bestSellingItem = new ItemSummary
                {
                    ItemID = item.ItemID,
                    ItemName = itemName,
                    TotalQuantity = item.TotalQuantity ?? 0
                };

                bestSellingItemsList.Add(bestSellingItem);
            }

            TopItems = bestSellingItemsList;
        }
        private void FindBestCustomers()
        {
            BestCustomers = _context.Customers
                .Select(c => new CustomerSummary
                {
                    CustomerId = c.CustId,
                    CustomerName = c.CustName,
                    TotalPurchaseAmount = c.Orders
                        .SelectMany(o => o.OrderDetails)
                        .Sum(od => od.Quantity * od.UnitAmount)
                })
                .OrderByDescending(result => result.TotalPurchaseAmount)
                .Take(10)
                .ToList();
        }

    }

    public class ItemSummary
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int TotalQuantity { get; set; }
    }
    public class CustomerSummary
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public double? TotalPurchaseAmount { get; set; }
    }

}
