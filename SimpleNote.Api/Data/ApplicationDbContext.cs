using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNote.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasData(
                new Note() { Id = 1, Title = "Sample Note 1", Content = "Lorem ipsum dolor sit amet." },
                new Note() { Id = 2, Title = "Sample Note 2", Content = "Lorem ipsum dolor sit amet." }
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Note> Notes { get; set; }
    }
}
