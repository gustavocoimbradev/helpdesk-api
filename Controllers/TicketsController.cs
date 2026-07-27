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
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAll()
    {
        var tickets = await _context.Tickets.ToListAsync();
        var response = tickets.Select(ticket => new TicketResponse
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            RequesterUserId = ticket.RequesterUserId,
            ResponsibleUserId = ticket.ResponsibleUserId,
            Status = ticket.StatusId,
            CreatedAt = ticket.CreatedAt
        });
        return Ok(response);
    }
    [HttpPost]
    [Route("create")]
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