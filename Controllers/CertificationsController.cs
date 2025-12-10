using CB17.Data;
using CB17.DTOs;
using CB17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CB17.Controllers;

// Controller for managing Certification records (CRUD)
[ApiController]
[Route("api/[controller]")]
public class CertificationsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CertificationsController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // GET: api/certifications
    // Returns all certifications
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Certifications.ToListAsync();

        // Entity list → DTO list
        var result = _mapper.Map<List<CertificationDto>>(list);

        return Ok(result);
    }

    // GET: api/certifications/{id}
    // Returns a single certification
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var cert = await _db.Certifications.FindAsync(id);
        if (cert == null)
            return NotFound();

        var dto = _mapper.Map<CertificationDto>(cert);
        return Ok(dto);
    }

    // POST: api/certifications
    // Creates a new certification
    [HttpPost]
    public async Task<IActionResult> Create(CertificationCreateDto dto)
    {
        // Optional: validate candidate existence
        var candidateExists = await _db.Candidates.AnyAsync(c => c.Id == dto.CandidateId);
        if (!candidateExists)
            return BadRequest($"Candidate with id {dto.CandidateId} does not exist.");

        // DTO → Entity
        var entity = _mapper.Map<Certification>(dto);

        _db.Certifications.Add(entity);
        await _db.SaveChangesAsync();

        var result = _mapper.Map<CertificationDto>(entity);
        return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, result);
    }

    // PUT: api/certifications/{id}
    // Updates an existing certification
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CertificationUpdateDto dto)
    {
        var entity = await _db.Certifications.FindAsync(id);
        if (entity == null)
            return NotFound();

        // Optional: validate new candidate id
        var candidateExists = await _db.Candidates.AnyAsync(c => c.Id == dto.CandidateId);
        if (!candidateExists)
            return BadRequest($"Candidate with id {dto.CandidateId} does not exist.");

        // Apply DTO values → entity
        _mapper.Map(dto, entity);

        await _db.SaveChangesAsync();

        var result = _mapper.Map<CertificationDto>(entity);
        return Ok(result);
    }

    // DELETE: api/certifications/{id}
    // Deletes a certification
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Certifications.FindAsync(id);
        if (entity == null)
            return NotFound();

        _db.Certifications.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
