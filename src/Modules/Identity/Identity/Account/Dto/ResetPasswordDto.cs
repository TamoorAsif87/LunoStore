namespace Identity.Account.Dto;

public record ResetPasswordDto(
    string Email,
    string Token,
    string NewPassword
);

