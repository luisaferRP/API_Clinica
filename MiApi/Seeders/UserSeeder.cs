using MiApi.Models;
using Microsoft.EntityFrameworkCore;


namespace MiApi.Seeders
{
    public class UserSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Luisa",
                    Email = "luisa@gmail.com",
                    Password = "123",
                    Rol = "Admin"
                },
                new User
                {
                    Id = 2,
                    Name = "Juan",
                    Email = "JuanC@gmail.com",
                    Password = "456",
                    Rol = "User"
                }
            );
        }
        
    }
}