using LEH.Models;
using Microsoft.EntityFrameworkCore;

namespace LEH;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<UserBadge> UserBadges { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<UserAction> Actions { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация EventRegistration
        modelBuilder.Entity<EventRegistration>()
            .HasKey(er => er.RegistrationId);

        // Конфигурация связей многие-ко-многим
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
            
        modelBuilder.Entity<UserBadge>()
            .HasKey(ub => new { ub.UserId, ub.BadgeId });

        // ДОБАВЬТЕ ЭТУ КОНФИГУРАЦИЮ ДЛЯ NOTIFICATION
        modelBuilder.Entity<Notification>()
            .HasKey(n => n.NotificationId);
            
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конфигурация строковых полей с ограничениями длины
        modelBuilder.Entity<Notification>()
            .Property(n => n.Type)
            .HasMaxLength(50)
            .IsRequired();
            
        modelBuilder.Entity<Notification>()
            .Property(n => n.Message)
            .HasMaxLength(1000)
            .IsRequired();
            
        modelBuilder.Entity<Notification>()
            .Property(n => n.Status)
            .HasMaxLength(20)
            .HasDefaultValue("unread");
            
        modelBuilder.Entity<Notification>()
            .Property(n => n.RelatedOperation)
            .HasMaxLength(100);
            
        modelBuilder.Entity<Notification>()
            .Property(n => n.Comments)
            .HasMaxLength(500);

        // Конфигурация для Event
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventDate).IsRequired();
            entity.HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}