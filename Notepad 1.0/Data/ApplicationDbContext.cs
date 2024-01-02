using Microsoft.EntityFrameworkCore;
using Notepad_1._0.Models;

namespace Notepad_1._0.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

    }
}
