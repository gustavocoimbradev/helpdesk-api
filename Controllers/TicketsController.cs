using Helpdesk_Api.Data;
using Helpdesk_Api.DTOs;
using Helpdesk_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Helpdesk_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;
    public TicketsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAll()
    {
        var Tickets =  await _context.Tickets.ToListAsync();

        return Ok(Tickets);
    }

    [HttpPost]
    [Route("new")]
    public async Task<ActionResult> Create(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            RequesterUserId = request.RequesterUserId
        };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return Ok(ticket);
    }

}