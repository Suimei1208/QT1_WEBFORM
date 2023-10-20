﻿namespace QT1_WEB.Models.ViewModels
{
    public class Edit_Item
    {
        public int ItemId { get; set; }

        public string? ItemName { get; set; }

        public string? Size { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
