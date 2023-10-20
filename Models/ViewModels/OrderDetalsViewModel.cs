namespace QT1_WEB.Models.ViewModels
{
    public class OrderDetalsViewModel
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ItemId { get; set; }

        public string? ItemName { get; set; }

        public int? Quantity { get; set; }

        public string? CustID { get; set; }

        public double? UnitAmount { get; set; }

        public virtual Item? Item { get; set; }

        public virtual Order? Order { get; set; }
    }
}
