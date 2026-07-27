namespace Helpdesk_Api.DTOs;

public class CreateTicketRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int RequesterUserId { get; set; }
    
}