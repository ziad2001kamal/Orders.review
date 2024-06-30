using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;

namespace Orders.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;
        public CategoryService(OrdersDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<CategoryViewModel>> GetAll(string searchKey)
        {
            var categories = await _db.Categories
          .Where(x => string.IsNullOrWhiteSpace(searchKey) || x.Name.Contains(searchKey))
          .Select(x => new CategoryViewModel
          {
              Id = x.Id,
              Name = x.Name,
              // Use a more optimized way to get the count of meals
              MealsCount = _db.Meals.Count(meal => meal.CategoryId == x.Id)
          })
          .ToListAsync();
            return categories;
        }
        public async Task<int> Delete(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
            {

                //throw

            }
            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            return category.Id;
        }
        public async Task<int> Create(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }
        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == dto.Id);
            if (category == null)
            {
                //throw
            }
            var updatecategory = _mapper.Map(dto, category);
            _db.Categories.Update(updatecategory);
            _db.SaveChanges();
            return category.Id;

        }
        public async Task<CategoryViewModel> Get(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
            {

                //throw

            }
            var categroyVm = _mapper.Map<CategoryViewModel>(category);
            categroyVm.MealsCount = _db.Meals.Count(x => x.CategoryId == category.Id);
            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            return categroyVm;

        }
    }

}
