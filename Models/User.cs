namespace Helpdesk_Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public int PersonId { get; set; }
    public int DepartmentId { get; set; }
}