using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
namespace Orders.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _user;

        public UserService(OrdersDbContext db, IMapper mapper, UserManager<User> user)
        {
            _db = db;
            _mapper = mapper;
            _user = user;
        }

        public async Task<List<UserViewModel>> GetAll(string serachkey)
        {
            var users = _db.Users.Where(x => x.Fullname.Contains(serachkey) || x.PhoneNumber.Contains(serachkey) || string.IsNullOrWhiteSpace(serachkey)).Select(x => new UserViewModel()).ToList();
            var user = _mapper.Map<List<UserViewModel>>(users);
            return users;
        }
        public async Task<string> Delete(string id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {

                //throw

            }
            user.IsDelete = true;
            _db.Users.Update(user);
            _db.SaveChanges();
            return user.Id;
        }
        public async Task<string> Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.UserName = dto.PhoneNumber;
            await _user.CreateAsync(user, dto.Password);
            return user.Id;
        }
        public async Task<string> Update(UpdateUserDto dto)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == dto.Id);
            if (user == null)
            {
                //throw
            }
            var updateuser = _mapper.Map(dto, user);
            _db.Users.Update(updateuser);
            _db.SaveChanges();
            return user.Id;

        }
        public async Task<UserViewModel> Get(string id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {

                //throw

            }
            var userVm = _mapper.Map<UserViewModel>(user);
            // categroyVm.MealCount = _db.Meals.Count(x => x.CategoryId == category.Id);
            user.IsDelete = true;
            _db.Users.Update(user);
            _db.SaveChanges();
            return userVm;

        }
    }

}
