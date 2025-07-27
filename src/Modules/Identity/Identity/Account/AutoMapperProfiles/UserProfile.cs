using Identity.Account.Dto;
using Identity.Account.Models;

namespace Identity.Account.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ReverseMap()
            .ConstructUsing(dto =>
                ApplicationUser.Create(
                    dto.Email,
                    dto.Name,
                    dto.Address,
                    dto.Phone,
                    dto.Country,
                    dto.City,
                    dto.ProfileImage
                )
            );
    }
}
