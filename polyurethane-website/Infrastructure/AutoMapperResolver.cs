using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using polyurethane_website.Models;
using Polyurethane.Data.Mapping;
using Polyurethane.Data.Models.Orders;

namespace polyurethane_website.Infrastructure
{
    public class AutoMapperResolver
    {
        public static IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.
                //cfg.CreateMap<Pallet, PalletWithItems>();
                //cfg.CreateMap<HouseJob, HouseJobWithWarehouse>();
                //cfg.CreateMap<Job, HouseJob>()
                //    .ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.Company_Name))
                //    .ForMember(dest => dest.CustomerAccountNumber, opts => opts.MapFrom(src => src.CustAccNum))
                //    .ForMember(dest => dest.PrincipalAccountNumber, opts => opts.MapFrom(src => src.PrincipalAccNum));

                cfg.CreateMap<CreateOrderModel, CreateOrder>();
                //-- configure profiles
                cfg.AddProfile<OrdersProfile>();
            });

            return config.CreateMapper();
        }
    }
}