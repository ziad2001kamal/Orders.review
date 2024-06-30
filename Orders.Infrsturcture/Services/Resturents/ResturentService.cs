using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Data.Models;

namespace Orders.Infrastructure.Services.Resturents
{
    public class ResturentService : IResturentService
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public ResturentService(OrdersDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<ResturentViewModel>> GetAll(string searchKey)
        {
            var resturent = await _db.Resturents.Include(x => x.Meals).Where(x => x.Name.Contains(searchKey) || string.IsNullOrEmpty(searchKey)).OrderByDescending(x => x.Meals.Count()).ToListAsync();
       return _mapper.Map<List<ResturentViewModel>>(resturent);
        }
        public async Task<int> Create(CreateResturentDto dto)
        {
            var resturent = _mapper.Map<Resturent>(dto);
            await _db.Resturents.AddAsync(resturent);
            await _db.SaveChangesAsync();
            return resturent.Id;
        }
    }
}
