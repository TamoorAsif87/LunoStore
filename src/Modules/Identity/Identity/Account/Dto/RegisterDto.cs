namespace Identity.Account.Dto;

public record RegisterDto(
    string Email,
    string Name,
    string Password,
    string ConfirmPassword
    );
