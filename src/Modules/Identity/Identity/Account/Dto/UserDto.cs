namespace Identity.Account.Dto;

public record UserDto(
    string Id,
    string Name,
    string UserName,
    string Email,
    string Address,
    string Phone,
    string City,
    string Country,
    string ProfileImage
    );
