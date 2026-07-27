using Helpdesk_Api.Data;
using Helpdesk_Api.DTOs;
using Helpdesk_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Helpdesk_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusesController : ControllerBase
{
    private readonly AppDbContext _context;
    public StatusesController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Status>>> GetAll()
    {
        var statuses = await _context.Statuses.ToListAsync();
        var response = statuses.Select(status => new StatusResponse
        {
            Id = status.Id,
            Title = status.Title
        });
        return Ok(response);
    }

}