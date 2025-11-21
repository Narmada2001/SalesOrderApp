using AutoMapper;
using SalesOrderApi.Domain.Entities;
using SalesOrderApp.Application.DTOs;
using SalesOrderApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrderApp.Application.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IGenericRepository<SalesOrder> _orderRepo;
        private readonly IGenericRepository<Client> _clientRepo;
        private readonly IGenericRepository<Item> _itemRepo;
        private readonly IMapper _mapper;

        public SalesOrderService(
            IGenericRepository<SalesOrder> orderRepo,
            IGenericRepository<Client> clientRepo,
            IGenericRepository<Item> itemRepo,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _clientRepo = clientRepo;
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.ListAllAsync(o => o.Client, o => o.Lines);
            return _mapper.Map<IEnumerable<SalesOrderDto>>(orders);
        }

        public async Task<SalesOrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id, o => o.Client, o => o.Lines);
            return _mapper.Map<SalesOrderDto>(order);
        }

        public async Task<SalesOrderDto> CreateOrderAsync(CreateSalesOrderDto input)
        {
            var order = _mapper.Map<SalesOrder>(input);
            
            // Generate InvoiceNo (simple logic for now)
            order.InvoiceNo = $"INV-{DateTime.Now.Ticks}";
            
            // Calculate totals
            decimal totalExcl = 0;
            decimal totalTax = 0;

            foreach (var line in order.Lines)
            {
                // We might need to fetch Item price if not trusted from frontend, 
                // but here we trust the DTO or we should fetch Item.
                // The DTO has Price.
                
                line.ExclAmount = (decimal)line.Quantity * line.Price;
                line.TaxAmount = line.ExclAmount * line.TaxRate / 100;
                line.InclAmount = line.ExclAmount + line.TaxAmount;

                totalExcl += line.ExclAmount;
                totalTax += line.TaxAmount;
            }

            order.TotalExcl = totalExcl;
            order.TotalTax = totalTax;
            order.TotalIncl = totalExcl + totalTax;

            await _orderRepo.AddAsync(order);
            
            return _mapper.Map<SalesOrderDto>(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepo.DeleteAsync(order);
            }
        }
    }
}
