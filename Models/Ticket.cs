namespace Helpdesk_Api.Models;
public class Ticket
{
    // Columns
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int StatusId { get; set; } = 1;
    public int PriorityId { get; set; } = 1;
    public int RequesterUserId { get; set; }
    public int? ResponsibleUserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    // Navigation properties
    public User RequesterUser { get; set; } = null!;
    public User? ResponsibleUser { get; set; }

}