using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeechToText.models;

namespace SpeechToText.Models
{
    public class SpeechToTextContext : DbContext
    {
        public SpeechToTextContext (DbContextOptions<SpeechToTextContext> options)
            : base(options)
        {
        }

        public DbSet<SpeechToText.models.Speech> Speech { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speech>()
                .HasIndex(s => s.RequestText);
        }
    }
}
