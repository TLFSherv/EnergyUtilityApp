using System.ComponentModel.DataAnnotations;

public record SendMessageRequest
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "Message")]
    public string? Message { get; set; }
}