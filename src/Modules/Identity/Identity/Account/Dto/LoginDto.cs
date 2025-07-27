namespace Identity.Account.Dto;

public record LoginDto(
        string Email,
        string Password,
        bool RememberMe = false
    );

