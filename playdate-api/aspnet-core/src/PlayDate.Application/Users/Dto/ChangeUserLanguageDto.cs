using System.ComponentModel.DataAnnotations;

namespace PlayDate.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}