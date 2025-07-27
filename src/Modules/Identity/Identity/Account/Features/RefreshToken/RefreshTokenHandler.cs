namespace Identity.Account.Features.RefreshToken;

public record RefreshTokenCommand(RefreshTokenDto RefreshToken):ICommand<ApiUserResponse>;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(p => p.RefreshToken.accessToken).NotEmpty();
        RuleFor(p => p.RefreshToken.refreshToken).NotEmpty();
    }
}

internal class RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper mapper) : ICommandHandler<RefreshTokenCommand, ApiUserResponse>
{
    public async Task<ApiUserResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityHandler.ReadJwtToken(command.RefreshToken.accessToken);

        


        var email = tokenContent.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;


        var user = await userManager.FindByEmailAsync(email!);

        if (user == null)
        {
            throw new UserNotFoundException($"User not found email {email}");
        }

        bool isRefreshTokenValid = await user.VerifyRefreshToken(userManager,command.RefreshToken.refreshToken);

        

        if (isRefreshTokenValid)
        {
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var role = roleClaims.FirstOrDefault()?.Value == "Admin" ? "Admin" : "User";

            var accessToken = user.GenerateToken(configuration, roleClaims);
            var refreshToken = await user.GenerateRefreshToken(userManager);

            return new ApiUserResponse(mapper.Map<UserDto>(user), accessToken, refreshToken, role);
        }

        await userManager.UpdateSecurityStampAsync(user);
        throw new BadRequestException("RefreshToken is not valid");

    }
}
