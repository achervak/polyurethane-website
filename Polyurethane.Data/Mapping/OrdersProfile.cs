using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Polyurethane.Data.Entities;
using Polyurethane.Data.Models;
using Polyurethane.Data.Models.Orders;

namespace Polyurethane.Data.Mapping
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<CreateOrder, OrderEntity>();
            CreateMap<OrderEntity, Order>();
            CreateMap<DetailEntity, OrderedDetail>();
            CreateMap<UserEntity, Customer>();


            //CreateMap<BoxDocs, BoxTopDocument>()
            //    .ForMember(dest => dest.CustomerAccountNumber, opts => opts.MapFrom(src => src.CustAccNum))
            //    .ForMember(dest => dest.PrincipalAccountNumber, opts => opts.MapFrom(src => src.PrincipalAccNum))
            //    .ReverseMap();
        }
    }
}
