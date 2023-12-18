using System;
using System.Collections.Generic;

namespace EF___LINQ.Models.DbModels
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
