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
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            // Company car
            CreateMap<CompanyCarModel, CompanyCar>();
            CreateMap<CompanyCar, CompanyCarModel>();
            // Reservation
            CreateMap<ReservationModel, Reservation>();
            CreateMap<Reservation, ReservationModel>();
            // DGTCar
            CreateMap<DGTCarModel, DGTCar>();
            CreateMap<DGTCar, DGTCarModel>();
        }
    }
}
