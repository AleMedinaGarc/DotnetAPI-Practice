using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace APICarData.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Login
            CreateMap<GoogleUserDataModel, GoogleUserData>();
            CreateMap<GoogleUserData, GoogleUserDataModel>();
            // Reservations
            // Company cars
        }
    }
}
