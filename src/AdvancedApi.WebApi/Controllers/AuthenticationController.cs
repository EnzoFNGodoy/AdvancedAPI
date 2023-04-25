using AdvancedApi.Application.Interfaces;
using AdvancedApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedApi.WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IUserServices _userServices;

    public AuthenticationController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    /// <summary>
    /// Used for sign in the user.
    /// </summary>
    /// <remarks>
    /// 
    /// Sample object to sign in:
    /// 
    ///     LOGIN 
    ///     {
    ///        "email": "Salvador55@yahoo.com",
    ///        "password": "d32ermc7bbr63tcx"
    ///     }
    /// 
    /// </remarks>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateRequestViewModel viewModel)
            => Ok(await _userServices.Login(viewModel));

}