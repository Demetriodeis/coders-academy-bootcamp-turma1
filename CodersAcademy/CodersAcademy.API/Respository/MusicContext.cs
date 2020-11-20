using CodersAcademy.API.Model;
using CodersAcademy.API.Respository.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Respository
{
    public class MusicContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public MusicContext(DbContextOptions<MusicContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumMapping());
            modelBuilder.ApplyConfiguration(new MusicMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
