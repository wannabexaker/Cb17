using CB17.Data;
using CB17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CB17.Controllers;

// Controller for managing Certification records (CRUD)
[ApiController]
[Route("api/[controller]")]
public class CertificationsController : ControllerBase
{
    private readonly AppDbContext _db;

    public CertificationsController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/certifications
    // Returns all certifications with their candidate info
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Certifications
            .Include(x => x.Candidate)
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/certifications/{id}
    // Returns a single certification
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var cert = await _db.Certifications
            .Include(x => x.Candidate)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cert == null)
            return NotFound();

        return Ok(cert);
    }

    // POST: api/certifications
    // Creates a new certification
    [HttpPost]
    public async Task<IActionResult> Create(Certification model)
    {
        // Optional: check if candidate exists
        var exists = await _db.Candidates.AnyAsync(c => c.Id == model.CandidateId);
        if (!exists)
            return BadRequest($"Candidate with id {model.CandidateId} does not exist.");

        _db.Certifications.Add(model);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOne), new { id = model.Id }, model);
    }

    // PUT: api/certifications/{id}
    // Updates an existing certification
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Certification model)
    {
        var dbItem = await _db.Certifications.FindAsync(id);
        if (dbItem == null)
            return NotFound();

        dbItem.Title = model.Title;
        dbItem.Authority = model.Authority;
        dbItem.IssueDate = model.IssueDate;
        dbItem.ExpirationDate = model.ExpirationDate;
        dbItem.CredentialId = model.CredentialId;
        dbItem.CandidateId = model.CandidateId;

        await _db.SaveChangesAsync();
        return Ok(dbItem);
    }

    // DELETE: api/certifications/{id}
    // Deletes a certification
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cert = await _db.Certifications.FindAsync(id);
        if (cert == null)
            return NotFound();

        _db.Certifications.Remove(cert);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
