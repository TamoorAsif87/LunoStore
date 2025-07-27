namespace Identity.Account.Features.ForgotPassword;

public record ForgotPasswordCommand(string Email) : ICommand<string>;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}

public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null)
            throw new UserNotFoundException($"No user found with this email {command.Email}");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var encodedToken = HttpUtility.UrlEncode(token);
        return encodedToken;
    }
}