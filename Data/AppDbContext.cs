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
            
        modelBuilder.Entity<Ticket>()
            .HasOne(ticket => ticket.ResponsibleUser)
            .WithMany(user => user.ResponsibleTickets)
            .HasForeignKey(ticket => ticket.ResponsibleUserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Status>().HasData(
            new Status
            {
                Id = 1,
                Title = "Aberto"
            },
            new Status
            {
                Id = 2,
                Title = "Em andamento"
            },
            new Status
            {
                Id = 3,
                Title = "Fechado"
            }
        );

        modelBuilder.Entity<Priority>().HasData(
            new Priority
            {
                Id = 1,
                Title = "Não definida"
            },
            new Priority
            {
                Id = 2,
                Title = "Baixa"
            },
            new Priority
            {
                Id = 3,
                Title = "Média"
            },
            new Priority
            {
                Id = 4,
                Title = "Alta"
            }
        );

        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                Name = "IT"
            }
        );

        modelBuilder.Entity<Person>().HasData(
            new Person
            {
                Id = 1,
                Name = "Administrator"
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "",
                PersonId = 1,
                DepartmentId = 1
            }
        );

    }
}