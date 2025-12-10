using CB17.Data;
using CB17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CB17.Controllers;

// Controller for managing Candidate records (CRUD)
[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly AppDbContext _db;

    public CandidatesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/candidates
    // Returns all candidates with their certifications
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Candidates
                            .Include(x => x.Certifications)
                            .ToListAsync();

        return Ok(list);
    }

    // GET: api/candidates/{id}
    // Returns a single candidate
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var candidate = await _db.Candidates
                                 .Include(x => x.Certifications)
                                 .FirstOrDefaultAsync(x => x.Id == id);

        if (candidate == null)
            return NotFound();

        return Ok(candidate);
    }

    // POST: api/candidates
    // Creates a new candidate
    [HttpPost]
    public async Task<IActionResult> Create(Candidate model)
    {
        _db.Candidates.Add(model);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOne), new { id = model.Id }, model);
    }

    // PUT: api/candidates/{id}
    // Updates an existing candidate
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Candidate model)
    {
        var dbItem = await _db.Candidates.FindAsync(id);
        if (dbItem == null)
            return NotFound();

        dbItem.FirstName = model.FirstName;
        dbItem.LastName = model.LastName;
        dbItem.Email = model.Email;
        dbItem.Phone = model.Phone;

        await _db.SaveChangesAsync();
        return Ok(dbItem);
    }

    // DELETE: api/candidates/{id}
    // Deletes a candidate + all certifications (cascade)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _db.Candidates.FindAsync(id);
        if (candidate == null)
            return NotFound();

        _db.Candidates.Remove(candidate);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
