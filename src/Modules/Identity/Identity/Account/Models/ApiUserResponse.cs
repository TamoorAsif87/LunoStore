using Identity.Account.Dto;

namespace Identity.Account.Models;

public record ApiUserResponse(
    UserDto? User,
    string accessToken,
    string refreshToken,
    string role = "User"
    );
