using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PlayDate.EntityFrameworkCore;

public static class PlayDateDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<PlayDateDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<PlayDateDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
