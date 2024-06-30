using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Orders.Core.Constant;
using Orders.Core.Dtos;
using Orders.Core.Resources;
using Orders.Core.ViewModels;
using Orders.Infrastructure.Services.Categories;
namespace Orders.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryServices;
        private readonly IStringLocalizer<Messages> _localizerMessages;

        public CategoryController(ICategoryService categoryServices , IStringLocalizer<Messages> localizerMessages)
        {
            _categoryServices = categoryServices;
            _localizerMessages = localizerMessages;
        }
       
        [HttpGet]
        public IActionResult GetAll(string? searchkey)
        {

            var categories = _categoryServices.GetAll(searchkey);
            return Ok(GetResponse(categories, _localizerMessages[Messages.WelcomeMessage]));
        }
        [HttpGet]
        public IActionResult Get(int id)
        {
            var category = _categoryServices.Get(id);
            return Ok(GetResponse(category));
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryDto dto)
        {
            var saveId=_categoryServices.Create(dto);
            return Ok(GetResponse(saveId));
        }
        [HttpPut]
        public IActionResult Update(UpdateCategoryDto dto)
        {
            var saveId = _categoryServices.Update(dto);
            return Ok(GetResponse(saveId));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteId= _categoryServices.Delete(id);
            return Ok(GetResponse(deleteId));
        }


    }
}
