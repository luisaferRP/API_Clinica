using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.DTOS
{
    public class AppiomentDTO
    {
        [Required]
        public required string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reason { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set;}
        
    }
}