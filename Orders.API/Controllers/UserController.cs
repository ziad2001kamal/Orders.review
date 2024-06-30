using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Users;

namespace Orders.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetAll(string searchkey)
        {
            var users = _userServices.GetAll(searchkey);
            return Ok(GetResponse(users));
        }
        [HttpGet]
        public IActionResult Get(string id)
        {
            var user = _userServices.Get(id);
            return Ok(GetResponse(user));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto dto)
        {
            var saveId = _userServices.Create(dto);
            return Ok(GetResponse(saveId));
        }
        [HttpPut]
        public IActionResult Update(UpdateUserDto dto)
        {
            var saveId = _userServices.Update(dto);
            return Ok(GetResponse(saveId));
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var deleteId = _userServices.Delete(id);
            return Ok(GetResponse(deleteId));
        }


    }
}
