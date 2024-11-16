using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApi.DTOS;
using MiApi.Models;
using MiApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MiApi.Controllers.v1.Appointment
{
    [ApiController]
    [Route("api/v1/appointments")]
    [Tags("appointments")]
    public class AppointmentCreateControllers(IAppointmentRepositories appointmentRepositories) : AppointmentControllers(appointmentRepositories)
    {
        [HttpPost("/appointment/create")]
        [SwaggerOperation(
            Summary = "create a new appointment",
            Description = "This endpoint is for create appointment."
        )]
        [SwaggerResponse(201,"appointment created successfully")]
        [SwaggerResponse(400,"An error occurred")]
        public async Task<ActionResult<Models.Appointment>> Create(AppiomentDTO appointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "Los datos del doctor son requeridos"
                });
            }
            try
            {
                var newAppointment = new Models.Appointment
                {
                    Date = appointmentDTO.Date,
                    Time = appointmentDTO.Time,
                    Reason = appointmentDTO.Reason,  
                    Status = (AppointmentStatus)appointmentDTO.Status , 
                    DoctorId = appointmentDTO.DoctorId,
                };

                await appointmentRepositories.Add(newAppointment);

    
                return Ok(new ResponseAPI<Models.Appointment>
                {
                    EsCorrecto = true,
                    Valor = newAppointment, 
                    Mensaje = "Cita creada correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Ocurrió un error: {ex.Message}"
                });
            }
        }
    }
}