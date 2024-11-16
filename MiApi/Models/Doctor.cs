using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MiApi.Models
{
    [Table("Doctors")]
    public class Doctor
    {

        [Key] 
        [Column("id")]
        public int Id { get; set; }

        [Column("specialization")]
        public required string Specialization { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User user { get; set;}

    }
}