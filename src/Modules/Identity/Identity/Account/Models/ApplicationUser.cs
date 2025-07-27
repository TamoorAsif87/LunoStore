namespace Identity.Account.Models;

public class ApplicationUser:AggregateIdentityUser
{
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string ProfileImage { get; private set; } = default!;

    private const string LoginProvider = "AdminProvider";
    private const string TokenName = "RefreshToken";


    private ApplicationUser(string email,string name, string address, string phone, string country,string city,string profileImage)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Address = address;
        Phone = phone;
        Country = country;
        City = city;
        ProfileImage = profileImage;
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        NormalizedUserName = email.ToUpperInvariant();
        EmailConfirmed = true;
        UserName = email;

    }

    public static ApplicationUser Create(string email,string name, string address, string phone, string country, string city, string profileImage)
    {
        return new ApplicationUser(email,name, address, phone, country, city, profileImage);
    }

    public string GenerateToken(IConfiguration configuration, List<Claim> roleClaims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, Email!),
            new Claim(JwtRegisteredClaimNames.Email, Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", Id!),
        }.Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToInt32(configuration["Jwt:ExpireHours"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<string> GenerateRefreshToken(UserManager<ApplicationUser> userManager)
    {
        await userManager.RemoveAuthenticationTokenAsync(this,LoginProvider,TokenName);

        var refreshToken = await userManager.GenerateUserTokenAsync(this, LoginProvider,TokenName);

        var result = await userManager.SetAuthenticationTokenAsync(this,LoginProvider,TokenName,refreshToken);

        return refreshToken;
    }

    internal void UpdateProfile(string name, string address, string phone, string city, string country, string? imageUrl)
    {
        Name = name;
        Address = address;
        Phone = phone;
        City = city;
        Country = country;
        if (!string.IsNullOrEmpty(imageUrl))
        {
            ProfileImage = imageUrl;
        }
    }

    internal async Task<bool> VerifyRefreshToken(UserManager<ApplicationUser> userManager,string refreshToken)
    {
        return await userManager.VerifyUserTokenAsync(this,LoginProvider,TokenName,refreshToken);
    }
}

