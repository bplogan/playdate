using Abp.Zero.EntityFrameworkCore;
using PlayDate.Authorization.Roles;
using PlayDate.Authorization.Users;
using PlayDate.MultiTenancy;
using System.Threading.Tasks;
using PlayDate.Players;
using PlayDate.Events;
using Microsoft.EntityFrameworkCore;


namespace PlayDate.EntityFrameworkCore;

public class PlayDateDbContext : AbpZeroDbContext<Tenant, Role, User, PlayDateDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Player> Players { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<PlayerAllergy> PlayerAllergies { get; set; }
    public DbSet<PlayerInstruction> PlayerInstructions { get; set; }
    public DbSet<PlayerRestriction> PlayerRestrictions { get; set; }
    public DbSet<PlayerEvent> PlayerEvents { get; set; }
    public DbSet<EventNote> EventNotes { get; set; }
    public DbSet<PlayerFriend> PlayerFriends { get; set; }
    public PlayDateDbContext(DbContextOptions<PlayDateDbContext> options)
        : base(options)
    {
    }
}
