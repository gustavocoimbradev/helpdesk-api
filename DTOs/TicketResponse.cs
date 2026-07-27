namespace Helpdesk_Api.DTOs;

public class TicketResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int RequesterUserId { get; set; }
    public int? ResponsibleUserId { get; set; }
    public int Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; } 
}