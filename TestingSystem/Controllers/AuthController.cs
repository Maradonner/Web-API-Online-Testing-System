﻿using System.ComponentModel.DataAnnotations;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingSystem.Models;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _httpClient = new HttpClient();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(UserDto request)
        {
            var response = await _authService.RegisterUser(request);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var response = await _authService.Login(request);
            if (response.Success)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(RefreshDTO request)
        {
            var response = await _authService.RefreshToken(request.RefreshToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet, Authorize]
        public ActionResult<string> Aloha()
        {
            return Ok("You are authorized!");
        }





    }
}
