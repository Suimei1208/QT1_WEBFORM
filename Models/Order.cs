using System;
using System.Collections.Generic;

namespace QT1_WEB.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? CustId { get; set; }

    public virtual Customer? Cust { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
