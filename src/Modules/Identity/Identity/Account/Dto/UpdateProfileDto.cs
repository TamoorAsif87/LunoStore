using Microsoft.AspNetCore.Mvc;

namespace Identity.Account.Dto;
public class UpdateProfileDto
{
    public string Name { get; set; } 

    public string? Address { get; set; } 

    public string? Phone { get; set; } 

    public string? City { get; set; } 

    public string? Country { get; set; } 

    public IFormFile? ProfileImageFile { get; set; }
}