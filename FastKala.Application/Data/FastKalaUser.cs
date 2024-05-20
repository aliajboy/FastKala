using Microsoft.AspNetCore.Identity;

namespace FastKala.Application.Data;

public class FastKalaUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Town { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? NationalCode { get; set; }
}