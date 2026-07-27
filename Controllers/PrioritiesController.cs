using Helpdesk_Api.Data;
using Helpdesk_Api.DTOs;
using Helpdesk_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Helpdesk_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PrioritiesController : ControllerBase
{
    private readonly AppDbContext _context;
    public PrioritiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Priority>>> GetAll()
    {
        var priorities = await _context.Priorities.ToListAsync();
        var response = priorities.Select(priority => new PriorityResponse
        {
            Id = priority.Id,
            Title = priority.Title
        });
        return Ok(response);
    }
    
}