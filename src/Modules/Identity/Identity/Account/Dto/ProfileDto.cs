namespace Identity.Account.Dto;

public record ProfileDto(
    string Name,
    string UserName,
    string Email,
    string Address,
    string Phone,
    string City,
    string Country,
    string ProfileImage
    );

