using System;
using System.Collections.Generic;

namespace EF___LINQ.Models.DbModels
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
