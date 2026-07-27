namespace Helpdesk_Api.DTOs;

public class UpdateTicketRequest
{
    public int? ResponsibleUserId { get; set; }
    public int? StatusId { get; set; }
    public int? PriorityId { get; set; }
}