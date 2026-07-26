namespace Helpdesk_Api.Models;
public class Ticket
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int StatusId { get; set; } = 0;
    public int PriorityId { get; set; } = 0;
    public int RequesterUserId { get; set; }
    public User RequesterUser { get; set; } = null!;

    public int? ResponsibleUserId { get; set; }
    public User? ResponsibleUser { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

}