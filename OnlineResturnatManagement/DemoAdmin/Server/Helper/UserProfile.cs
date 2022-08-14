﻿using AutoMapper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Helper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CompanyProfile, CompanyProfileDto>().ReverseMap();
            CreateMap<NavigationMenu, NavigationMenuDto>().ReverseMap();
            CreateMap<SoftwareSettings, SoftwareSettingsDto>().ReverseMap();
            CreateMap<UnitOfMeasure, UnitOfMeasureDto>().ReverseMap();
            CreateMap<Kitchen, KitchenDto>().ReverseMap();
            CreateMap<CounterInfo, CounterInfoDto>().ReverseMap();
            CreateMap<CreditCard,CreditCardDtos>().ReverseMap();
            CreateMap<CustomerSetup, CustomerSetupDtos>().ReverseMap(); 

        }
    }
}
