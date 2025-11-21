using System;
using System.Collections.Generic;

namespace SalesOrderApp.Application.DTOs
{
    public class CreateSalesOrderDto
    {
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CreateSalesOrderLineDto> Lines { get; set; } = new List<CreateSalesOrderLineDto>();
    }

    public class CreateSalesOrderLineDto
    {
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; } = "";
    }
}
