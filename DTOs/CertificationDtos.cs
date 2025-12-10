namespace CB17.DTOs;

// Used when returning certification data to the client
public class CertificationDto
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Authority { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialId { get; set; }
}

// Used when creating a new certification (POST)
public class CertificationCreateDto
{
    public int CandidateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Authority { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialId { get; set; }
}

// Used when updating an existing certification (PUT)
public class CertificationUpdateDto
{
    public int CandidateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Authority { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialId { get; set; }
}
