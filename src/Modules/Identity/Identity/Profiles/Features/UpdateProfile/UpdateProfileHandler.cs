using Identity.Account.Dto;
using Identity.Account.Models;
using Shared.Services.Contracts;

namespace Identity.Profiles.Features.UpdateProfile;

public record UpdateProfileCommand(UpdateProfileDto Profile) : ICommand<ProfileDto>;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.Profile.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

        RuleFor(x => x.Profile.Address)
            .MaximumLength(200).WithMessage("Address must be less than 200 characters.");

        RuleFor(x => x.Profile.Phone)
            .MaximumLength(20).WithMessage("Phone must be less than 20 characters.");

        RuleFor(x => x.Profile.City)
            .MaximumLength(100).WithMessage("City must be less than 100 characters.");

        RuleFor(x => x.Profile.Country)
            .MaximumLength(100).WithMessage("Country must be less than 100 characters.");

        RuleFor(x => x.Profile.ProfileImageFile)
            .Must(BeAValidImageFile)
            .When(x => x.Profile.ProfileImageFile != null)
            .WithMessage("Only image files (JPEG, PNG, JPG) under 5MB are allowed.");
    }

    private bool BeAValidImageFile(IFormFile? file)
    {
        if (file == null) return true;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var maxSizeInBytes = 5 * 1024 * 1024;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension) && file.Length <= maxSizeInBytes;
    }
}


public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, ProfileDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileUpload _fileUpload;

    public UpdateProfileCommandHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IFileUpload fileUpload)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _fileUpload = fileUpload;
    }

    public async Task<ProfileDto> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(email))
            throw new UnauthorizedAccessException("User not authenticated.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new UserNotFoundException("User not found.");


        var dto = command.Profile;

        string? imageUrl = null;
        if (dto.ProfileImageFile is { Length: > 0 })
        {
            imageUrl = await _fileUpload.UploadImageAsync(dto.ProfileImageFile);
        }

        
        user.UpdateProfile(dto.Name, dto.Address, dto.Phone, dto.City, dto.Country, imageUrl);
        await _userManager.UpdateAsync(user);

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