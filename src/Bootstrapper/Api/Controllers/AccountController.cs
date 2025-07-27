using Identity.Account.Dto;
using Identity.Account.Features.ChangePassword;
using Identity.Account.Features.ForgotPassword;
using Identity.Account.Features.LoginUser;
using Identity.Account.Features.RefreshToken;
using Identity.Account.Features.RegisterUser;
using Identity.Account.Features.ResetPassword;
using Identity.Profiles.Features.GetAllUsers;
using Identity.Profiles.Features.GetProfile;
using Identity.Profiles.Features.UpdateProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _sender;

    public AccountController(IMediator mediator)
    {
        _sender = mediator;
    }

    /// <summary>
    /// User Registration
    /// </summary>
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// User Login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Forgot Password - Generates a reset token
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        var token = await _sender.Send(command);
        return Ok(new { token, message = "Reset token generated. Please check your email." });
    }

    /// <summary>
    /// Reset Password
    /// </summary>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var success = await _sender.Send(command);
        return Ok(new { success, message = "Password has been reset successfully." });
    }

    /// <summary>
    /// Change Password
    /// </summary>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var result = await _sender.Send(command);
        return result ? Ok(new { success = true, message = "Password changed successfully." }) : BadRequest(new { success=false,message = "Failed to change password."});
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _sender.Send(new GetProfileQuery());
        return Ok(profile);
    }

    [Authorize]
    [HttpPost("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto data)
    {
        var result = await _sender.Send(new UpdateProfileCommand(data));
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [Authorize(Roles ="Admin")]
    [HttpGet("allUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _sender.Send(new GetAllUsersQuery());
        return Ok(result);
    }
}
