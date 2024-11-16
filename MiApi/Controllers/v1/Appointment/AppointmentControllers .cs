using MiApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers.v1.Appointment
{
    public class AppointmentControllers(IAppointmentRepositories appointmentRepositories) : ControllerBase 
    {
       private readonly IAppointmentRepositories _appointmentRepositories; 
    }
}