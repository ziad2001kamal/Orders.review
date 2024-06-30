using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Data.Models;
using Orders.Infrastructure.Services.Resturents;

namespace Orders.API.Controllers
{
    public class ResturentController : BaseController
    {
        private readonly IResturentService _resturentService;

        public ResturentController(IResturentService resturentService)
        {
            _resturentService = resturentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string searchKey)
        {
            var resturent = _resturentService.GetAll(searchKey);
            return Ok(GetResponse(resturent));
        }
        [HttpPost]
        public  IActionResult Create([FromBody]CreateResturentDto dto )
        {
            var resturent = _resturentService.Create(dto);
            return Ok(GetResponse(resturent));
        }

    }
}
