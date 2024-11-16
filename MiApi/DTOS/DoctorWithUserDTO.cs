using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApi.DTOS
{
    public class DoctorWithUserDTO
    {
        public int DoctorId { get; set; }
        public string Specialization { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public UserDTO User { get; set; }
    }
}