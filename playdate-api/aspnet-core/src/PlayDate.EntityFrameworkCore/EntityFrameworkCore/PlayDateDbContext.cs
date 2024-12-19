using Abp.Zero.EntityFrameworkCore;
using PlayDate.Authorization.Roles;
using PlayDate.Authorization.Users;
using PlayDate.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace PlayDate.EntityFrameworkCore;

public class PlayDateDbContext : AbpZeroDbContext<Tenant, Role, User, PlayDateDbContext>
{
    /* Define a DbSet for each entity of the application */

    public PlayDateDbContext(DbContextOptions<PlayDateDbContext> options)
        : base(options)
    {
    }
}
