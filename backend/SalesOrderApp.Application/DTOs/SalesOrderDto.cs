using System;
using System.Collections.Generic;

namespace SalesOrderApp.Application.DTOs
{
    public class SalesOrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = "";
        public string InvoiceNo { get; set; } = "";
        public DateTime InvoiceDate { get; set; }
        public DateTime OrderDate { get; set; } // Mapping InvoiceDate to OrderDate for frontend consistency if needed
        public string ReferenceNo { get; set; } = "";
        public string Note { get; set; } = "";
        public decimal TotalExcl { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalIncl { get; set; }
        public List<SalesOrderLineDto> Lines { get; set; } = new List<SalesOrderLineDto>();
    }

    public class SalesOrderLineDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = "";
        public string ItemDescription { get; set; } = "";
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public decimal AmountExcl { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal AmountIncl { get; set; }
        public string Note { get; set; } = "";
    }
}
