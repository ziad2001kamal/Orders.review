﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Auth;

namespace Orders.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [AllowAnonymous]
       public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            return Ok(GetResponse(await _authService.Login(dto))) ;
        }
      
    }
}
