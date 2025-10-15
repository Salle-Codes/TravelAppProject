using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbModels;
using System.Linq;
using Models;
using Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    readonly IUserService _service;
    readonly ILogger<UsersController> _logger;
    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // 4. Visa alla användare och de kommentarer som användaren har lagt in
    [HttpGet]
    [ActionName("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users with comments");
            return BadRequest(ex.Message);
        }
    }
}