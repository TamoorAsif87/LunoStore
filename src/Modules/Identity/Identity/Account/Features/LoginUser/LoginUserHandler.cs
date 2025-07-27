using Identity.Account.Dto;
using Identity.Account.Exceptions;

namespace Identity.Account.Features.LoginUser;

public record LoginUserCommand(LoginDto Login) : ICommand<ApiUserResponse>;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Login.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Login.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, ApiUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMapper mapper,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<ApiUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var dto = command.Login;

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not Found With Email {dto.Email}");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
            throw new BadRequestException("Invalid email or password");

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

        var accessToken = user.GenerateToken(_configuration, roleClaims);
        var refreshToken = await user.GenerateRefreshToken(_userManager);

        var userDto = _mapper.Map<UserDto>(user);

        string role = "User";

        if (roles.Contains("Admin"))
        {
            role = "Admin";
        }

        return new ApiUserResponse(userDto, accessToken,refreshToken,role);
    }
}
