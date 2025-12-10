using System;
using System.Linq;
using CB17.Models;

namespace CB17.Data
{
    // Simple data seeding for initial demo records
    public static class SeedData
    {
        // This method will insert data only if the tables are empty
        public static void Initialize(AppDbContext context)
        {
            // If there are already candidates, do nothing
            if (context.Candidates.Any())
                return;

            // Create sample candidates
            var candidate1 = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "6900000001"
            };

            var candidate2 = new Candidate
            {
                FirstName = "Maria",
                LastName = "Papadopoulou",
                Email = "maria.pap@example.com",
                Phone = "6900000002"
            };

            context.Candidates.AddRange(candidate1, candidate2);
            context.SaveChanges(); // After this, Id values are generated

            // Create sample certifications linked to the candidates above
            var certs = new[]
            {
                new Certification
                {
                    CandidateId = candidate1.Id,
                    Title = "C# Fundamentals",
                    Authority = "Microsoft",
                    IssueDate = DateTime.UtcNow.Date,
                    CredentialId = "MS12345"
                },
                new Certification
                {
                    CandidateId = candidate1.Id,
                    Title = "Database Design",
                    Authority = "Oracle",
                    IssueDate = DateTime.UtcNow.Date.AddMonths(-6),
                    CredentialId = "ORC56789"
                },
                new Certification
                {
                    CandidateId = candidate2.Id,
                    Title = "Web Developer",
                    Authority = "PeopleCert",
                    IssueDate = DateTime.UtcNow.Date.AddMonths(-3),
                    CredentialId = "PC99887"
                }
            };

            context.Certifications.AddRange(certs);
            context.SaveChanges();
        }
    }
}
