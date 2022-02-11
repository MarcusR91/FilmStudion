using FilmStudion.Models.Film;
using FilmStudion.Models.FilmStudio;
using FilmStudion.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudion.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "Marcus",
                Password = "Marcus",
                isAdmin = true,
                Role = "admin",
                Token = ""

            });
            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                StudioId = 1,
                Name = "FilmStudio",
                Password = "FilmStudio",
                Role = "filmstudio",
                Token = ""

            });
            modelBuilder.Entity<Film>().HasData(new Film
            {
                FilmId = 1,
                Name = "The Revenant",
                Country = "sweden",
                Director = "Marcus",
                FilmCopies = null

            });



        }
    }
}
