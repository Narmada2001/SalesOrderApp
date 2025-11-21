using AutoMapper;
using SalesOrderApi.Domain.Entities;
using SalesOrderApp.Application.DTOs;

namespace SalesOrderApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SalesOrder, SalesOrderDto>()
                .ForMember(d => d.ClientName, o => o.MapFrom(s => s.Client != null ? s.Client.CustomerName : ""))
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.InvoiceDate));

            CreateMap<SalesOrderLine, SalesOrderLineDto>()
                .ForMember(d => d.ItemCode, o => o.MapFrom(s => s.Item != null ? s.Item.Code : ""))
                .ForMember(d => d.ItemDescription, o => o.MapFrom(s => s.Item != null ? s.Item.Description : ""))
                .ForMember(d => d.AmountExcl, o => o.MapFrom(s => s.ExclAmount))
                .ForMember(d => d.AmountIncl, o => o.MapFrom(s => s.InclAmount));

            CreateMap<CreateSalesOrderDto, SalesOrder>()
                .ForMember(d => d.InvoiceDate, o => o.MapFrom(s => s.OrderDate));

            CreateMap<CreateSalesOrderLineDto, SalesOrderLine>();
        }
    }
}
