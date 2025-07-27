namespace Identity.Account.Features.ChangePassword;


public record ChangePasswordCommand(ChangePasswordDto ChangePassword) : ICommand<bool>;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.ChangePassword.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.ChangePassword.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

        RuleFor(x => x.ChangePassword.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters.");
    }
}

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var authenticatedUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        var dto = command.ChangePassword;

        if (authenticatedUserEmail != dto.Email)
            throw new UnauthorizedAccessException("You can only change your own password.");

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UserNotFoundException("User not found.");

        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"Password change failed: {errors}");
        }

        return true;
    }
}
