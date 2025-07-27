namespace Identity.Account.Dto;

public record ChangePasswordDto(
    string Email,
    string CurrentPassword,
    string NewPassword
);
