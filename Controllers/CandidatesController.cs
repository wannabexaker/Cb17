using CB17.Data;
using CB17.DTOs;
using CB17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CB17.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CandidatesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // GET: api/candidates
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Candidates.ToListAsync();

        // Convert EF entities → DTOs
        var result = _mapper.Map<List<CandidateDto>>(list);

        return Ok(result);
    }

    // GET: api/candidates/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var item = await _db.Candidates.FindAsync(id);
        if (item == null)
            return NotFound();

        var dto = _mapper.Map<CandidateDto>(item);
        return Ok(dto);
    }

    // POST: api/candidates
    [HttpPost]
    public async Task<IActionResult> Create(CandidateCreateDto dto)
    {
        // Convert DTO → EF entity
        var entity = _mapper.Map<Candidate>(dto);

        _db.Candidates.Add(entity);
        await _db.SaveChangesAsync();

        var result = _mapper.Map<CandidateDto>(entity);
        return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, result);
    }

    // PUT: api/candidates/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CandidateUpdateDto dto)
    {
        var entity = await _db.Candidates.FindAsync(id);
        if (entity == null)
            return NotFound();

        // Apply DTO → entity updates
        _mapper.Map(dto, entity);

        await _db.SaveChangesAsync();

        var result = _mapper.Map<CandidateDto>(entity);
        return Ok(result);
    }

    // DELETE: api/candidates/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Candidates.FindAsync(id);
        if (entity == null)
            return NotFound();

        _db.Candidates.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
