using System.Text.Json.Serialization;

namespace CB17.Models;

public class Certification
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Authority { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialId { get; set; }

    // We ignore the back-reference to avoid JSON cycles
    [JsonIgnore]
    public Candidate? Candidate { get; set; }
}
