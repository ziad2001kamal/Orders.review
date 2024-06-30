using System;
using AutoMapper;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;

namespace Orders.Infrastructure.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<User, UserViewModel>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<CreateResturentDto, Resturent>();
            CreateMap<Resturent, ResturentViewModel>();
            //CreateMap<UpdateResturentDto, Resturent>();
        }
    }
}
