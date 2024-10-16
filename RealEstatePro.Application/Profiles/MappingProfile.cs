﻿using AutoMapper;
using RealEstatePro.Application.Estates.Create;
using RealEstatePro.Application.Estates.GetAll;
using RealEstatePro.Application.Users.Login;
using RealEstatePro.Domain.Estates;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, LoginUserDto>().ReverseMap();


        CreateMap<Estate, CreateEstateDto>().ReverseMap();
        CreateMap<Estate, EstateDto>().ReverseMap();
    }
}
