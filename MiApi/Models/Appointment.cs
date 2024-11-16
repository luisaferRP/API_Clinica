using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiApi.Models
{

     public enum AppointmentStatus
    {
        Pendiente = 0,
        Cancelado =1,
        Completado =2,
    }

    [Table("appointments")]
    public class Appointment
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("reason")]
        public string Reason { get; set; }

        [Column("status")]
        public AppointmentStatus Status { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor doctor { get; set;}
        
    }
}