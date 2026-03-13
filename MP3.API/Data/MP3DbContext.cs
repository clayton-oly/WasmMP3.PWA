using Microsoft.EntityFrameworkCore;
using MP3.API.Models;

namespace MP3.API.Data
{
    public class MP3DbContext : DbContext
    {
        public MP3DbContext(DbContextOptions<MP3DbContext> options) : base(options) { }

        public DbSet<UserPushModel> UserPush { get; set; }

    }
}


