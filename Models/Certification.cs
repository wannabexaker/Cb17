using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CB17.Models;

// Represents a certification that belongs to a candidate
public class Certification
{
    public int Id { get; set; }

    // Foreign key to Candidate
    [Required]
    public int CandidateId { get; set; }

    // Certification title (required, max length 100)
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    // Issuing authority (optional, max length 100)
    [MaxLength(100)]
    public string? Authority { get; set; }

    // When the certification was issued
    public DateTime? IssueDate { get; set; }

    // When (if ever) the certification expires
    public DateTime? ExpirationDate { get; set; }

    // External credential id or code
    public string? CredentialId { get; set; }

    // Back reference to candidate is ignored in JSON to avoid cycles
    [JsonIgnore]
    public Candidate? Candidate { get; set; }
}
