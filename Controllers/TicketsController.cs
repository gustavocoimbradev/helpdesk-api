using Helpdesk_Api.Data;
using Helpdesk_Api.DTOs;
using Helpdesk_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAll(int? statusId, int? priorityId, int? requesterUserId, int? responsibleUserId)
    {
        var query = _context.Tickets.AsQueryable();
        if (statusId is not null)
        {
            query = query.Where(ticket =>  ticket.StatusId == statusId);
        }
        if (priorityId is not null)
        {
            query = query.Where(ticket =>  ticket.PriorityId == priorityId);
        }
        if (requesterUserId is not null)
        {
            query = query.Where(ticket =>  ticket.RequesterUserId == requesterUserId);
        }
        if (responsibleUserId is not null)
        {
            query = query.Where(ticket =>  ticket.ResponsibleUserId == responsibleUserId);
        }
        var tickets = await query.ToListAsync();
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
    [HttpGet]
    [Route("{ticketId}")]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetSingle(int ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        if (ticket is null)
        {
            return NotFound(new{
                Message = "Ticket not found"
            });
        }
        var response = new TicketResponse
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            RequesterUserId = ticket.RequesterUserId,
            ResponsibleUserId = ticket.ResponsibleUserId,
            Status = ticket.StatusId,
            CreatedAt = ticket.CreatedAt
        };
        return Ok(response);

    }
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> Create(CreateTicketRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.RequesterUserId);

        if (user is null)
        {
            return BadRequest(new{
                Message = "Requester user not found"
            });
        }

        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            RequesterUserId = request.RequesterUserId
        };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetSingle),
            new { ticketId = ticket.Id },
            new
            {
                Message = "Ticket successfully created!",
                TicketId = ticket.Id
            }

        );
    }
    [HttpPatch]
    [Route("{ticketId}")]
    public async Task<ActionResult> Update(int ticketId, UpdateTicketRequest request)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        if (ticket is null)
        {
            return NotFound(new{
                Message = "Ticket not found"
            });
        }

        if (request.ResponsibleUserId != ticket.ResponsibleUserId) {
            var responsible = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.ResponsibleUserId);
            if (responsible is null)
            {
                return BadRequest(new{
                    Message = "Responsible user not found"
                });
            }
        }

        if (request.StatusId != ticket.StatusId) {
            var status = await _context.Statuses.FirstOrDefaultAsync(status => status.Id == request.StatusId);
            if (status is null)
            {
                return BadRequest(new{
                    Message = "Status not found"
                });
            }
        }

         if (request.PriorityId != ticket.PriorityId) {
            var priority = await _context.Priorities.FirstOrDefaultAsync(priority => priority.Id == request.PriorityId);
            if (priority is null)
            {
                return BadRequest(new{
                    Message = "Priority not found"
                });
            }
        }

        ticket.ResponsibleUserId = request.ResponsibleUserId ?? ticket.ResponsibleUserId;
        ticket.StatusId = request.StatusId ?? ticket.StatusId;
        ticket.PriorityId = request.PriorityId ?? ticket.PriorityId;

        await _context.SaveChangesAsync();
        return Ok(
            new
            {
                Message = "Ticket updated successfully!",
                TicketId = ticket.Id
            }

        );
    }
    [HttpDelete]
    [Route("{ticketId}")]
    public async Task<ActionResult> Delete(int ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        if (ticket is null)
        {
            return NotFound(new{
                Message = "Ticket not found"
            });
        }
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return Ok(new
        {
            Message = "Ticket deleted successfully!",
            TicketId = ticket.Id
        });
    }
}