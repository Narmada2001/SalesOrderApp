using System.Collections.Generic;
using System.Threading.Tasks;
using SalesOrderApp.Application.DTOs;

namespace SalesOrderApp.Application.Interfaces
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrderDto>> GetAllOrdersAsync();
        Task<SalesOrderDto?> GetOrderByIdAsync(int id);
        Task<SalesOrderDto> CreateOrderAsync(CreateSalesOrderDto input);
        Task DeleteOrderAsync(int id);
    }
}
