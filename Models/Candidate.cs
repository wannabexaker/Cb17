using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CB17.Models;

// Represents a single candidate record in the database
public class Candidate
{
    public int Id { get; set; }

    // Candidate first name (required, max length 50)
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    // Candidate last name (required, max length 50)
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    // Candidate email (required, unique in database)
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Optional phone number
    [Phone]
    public string? Phone { get; set; }

    // Navigation property for related certifications
    public ICollection<Certification>? Certifications { get; set; }
}
