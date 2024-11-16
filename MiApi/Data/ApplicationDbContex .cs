using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//usar
using Microsoft.EntityFrameworkCore;
using MiApi.Models;
using MiApi.Seeders;

namespace MiApi.Data
{
    public class ApplicationDbContex(DbContextOptions options) : DbContext(options)
    {

        public DbSet<User> Users{ get; set; }
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Appointment> Appointments{ get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     UserSeeder.Seed(modelBuilder);
        
        // }
        
    }
}