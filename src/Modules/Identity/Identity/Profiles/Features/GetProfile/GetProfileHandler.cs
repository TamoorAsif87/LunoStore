using Identity.Account.Dto;
using Identity.Account.Models;

namespace Identity.Profiles.Features.GetProfile;

public record GetProfileQuery : IQuery<ProfileDto>;

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, ProfileDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProfileQueryHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(email))
            throw new UnauthorizedAccessException("User is not authenticated.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new UserNotFoundException("User not found.");

        return new ProfileDto(
            user.Name,
            user.UserName!,
            user.Email!,
            user.Address,
            user.Phone,
            user.City,
            user.Country,
            user.ProfileImage
        );
    }
}
