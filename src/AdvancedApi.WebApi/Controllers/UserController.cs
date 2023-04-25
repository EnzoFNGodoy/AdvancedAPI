using AdvancedApi.Application.Interfaces;
using AdvancedApi.Application.ViewModels;
using AdvancedApi.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedApi.WebApi.Controllers;

[ApiController]
[Route("users")]
public sealed class UserController : ControllerBase
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [Authorize(Roles = Roles.CUSTOMER)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UserQueryParameters queryParameters)
    {
        var users = (await _userServices.GetAll())
            .Skip(queryParameters.Size * (queryParameters.Page - 1))
            .Take(queryParameters.Size);

        if(queryParameters.Like is not null)
        {
            var like = queryParameters.Like;
            users = users.Where(x => x.Email.ToLower().Contains(like.ToLower())
            || x.Role.ToLower().Contains(like.ToLower())
            || x.Name.ToLower().Contains(like.ToLower())
            || x.Status.ToLower().Contains(like.ToLower()));
        }

        if (queryParameters.MinCreatedAtDate is not null)
            users = users.Where(x => x.CreatedAt >= queryParameters.MinCreatedAtDate);

        if (queryParameters.MaxCreatedAtDate is not null)
            users = users.Where(x => x.CreatedAt <= queryParameters.MaxCreatedAtDate);

        if (queryParameters.MinUpdatedAtDate is not null)
            users = users.Where(x => x.CreatedAt >= queryParameters.MinUpdatedAtDate);

        if (queryParameters.MaxUpdatedAtDate is not null)
            users = users.Where(x => x.CreatedAt <= queryParameters.MaxUpdatedAtDate);

        if (queryParameters.Status is not null)
            users = users.Where(x => x.Status == queryParameters.Status.ToString());

        return Ok(users.ToArray());
    }

    [Authorize(Roles = Roles.CUSTOMER)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _userServices.GetById(id));

    [Authorize(Roles = Roles.ADMIN)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequestViewModel viewModel)
    {
        await _userServices.Create(viewModel);

        return Ok("User created successfully!");
    }

    [Authorize(Roles = Roles.ADMIN)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserRequestViewModel viewModel)
    {
        await _userServices.Update(id, viewModel);

        return Ok("User updated successfully!");
    }

    [Authorize(Roles = Roles.ADMIN)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userServices.Delete(id);

        return Ok("User deleted successfully!");
    }

    [Authorize(Roles = Roles.ADMIN)]
    [HttpPatch("activate/{id}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        await _userServices.Activate(id);

        return Ok("User activated successfully!");
    }

    [Authorize(Roles = Roles.ADMIN)]
    [HttpPatch("deactivate/{id}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        await _userServices.Deactivate(id);

        return Ok("User deactivated successfully!");
    }
}