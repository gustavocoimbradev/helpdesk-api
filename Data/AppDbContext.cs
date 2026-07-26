using Helpdesk_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk_Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>()
            .HasOne(ticket => ticket.RequesterUser)
            .WithMany(user => user.RequestedTickets)
            .HasForeignKey(ticket => ticket.RequesterUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // TODO: Montar os outros relacionamentos
    }
}