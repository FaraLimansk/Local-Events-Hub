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
            .HasKey(er => er.RegistrationId); // Явное указание первичного ключа

        // Конфигурация связей многие-ко-многим
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
            
        modelBuilder.Entity<UserBadge>()
            .HasKey(ub => new { ub.UserId, ub.BadgeId });

        // Опционально: настройка связей
        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.User)
            .WithMany(u => u.EventRegistrations)
            .HasForeignKey(er => er.UserId);

        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.Event)
            .WithMany(e => e.EventRegistrations)
            .HasForeignKey(er => er.EventId);
    }
}