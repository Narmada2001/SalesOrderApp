namespace SalesOrderApi.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
    }
}
