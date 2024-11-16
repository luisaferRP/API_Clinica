using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//usar
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApi.Models
{
    
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("rol")]
        public string Rol { get; set; }
    }
}