namespace Identity.Account.Features.ResetPassword;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : ICommand<bool>;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.ResetPasswordDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.ResetPasswordDto.Token)
            .NotEmpty().WithMessage("Reset token is required.");

      

        RuleFor(x => x.ResetPasswordDto.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters long.");
    }
}

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var dto = command.ResetPasswordDto;
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            throw new UserNotFoundException($"User not found with email {dto.Email}");

        var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(dto.Token), dto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"Password reset failed: {errors}");
        }

        return true;
    }
}

