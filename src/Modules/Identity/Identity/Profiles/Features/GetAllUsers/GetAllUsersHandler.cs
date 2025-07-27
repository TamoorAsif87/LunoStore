namespace Identity.Profiles.Features.GetAllUsers;

public record GetAllUsersQuery():IQuery<List<UserDto>>;

internal class GetAllUsersQueryHandler(
    IdentityContext dbContext,
    IMapper mapper
) : IQueryHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await dbContext.ApplicationUsers.ToListAsync(cancellationToken);

        return mapper.Map<List<UserDto>>(users);
    }
}
