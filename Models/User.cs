namespace Helpdesk_Api.Models;

public class User
{
    // Columns 
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public int PersonId { get; set; }
    public int DepartmentId { get; set; }
    // Collections 
    public ICollection<Ticket> RequestedTickets { get; set; } = [];
    public ICollection<Ticket> ResponsibleTickets { get; set; } = [];
}