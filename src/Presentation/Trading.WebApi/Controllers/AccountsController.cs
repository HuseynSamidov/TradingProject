using Microsoft.AspNetCore.Mvc;
using System.Net;
using TradingProject.Application.Abstracts.Services;
using TradingProject.Application.DTOs.CategoryDto;
using TradingProject.Application.DTOs.UserDTOs;
using TradingProject.Application.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trading.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private IUserService _userService { get; }

    public AccountsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _userService.Register(dto);
        return StatusCode((int)result.StatusCode, result);
    }


    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }



    // PUT api/<AccountsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AccountsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
