namespace Identity.Account.Features.RegisterUser;

public record RegisterUserCommand(RegisterDto Register) : ICommand<ApiUserResponse>;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Register.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Register.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Register.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Register.ConfirmPassword)
            .Equal(x => x.Register.Password).WithMessage("Passwords do not match.");
    }
}




public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ApiUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ApiUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var dto = command.Register;

        var user = ApplicationUser.Create(
            dto.Email,
            dto.Name,
            address: "",
            phone: "",
            country: "",
            city: "",
            profileImage: ""
        );

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"User creation failed: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

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

