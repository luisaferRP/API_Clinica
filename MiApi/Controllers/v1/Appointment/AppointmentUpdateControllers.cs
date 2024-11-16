using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApi.Models;
using MiApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MiApi.Controllers.v1.Appointment
{
    [ApiController]
    [Route("api/v1/appointments")]
    [Tags("appointments")]
    public class AppointmentUpdateControllers(IAppointmentRepositories appointmentRepositories) : AppointmentControllers(appointmentRepositories)
    {
        [HttpPut("/appointment/update/{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "uptade appointment", 
            Description = "Update the data of an appointment")]
        [SwaggerResponse(200,"appointment type updated successfully")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"appointment type not found")]
        public async Task<ActionResult<Models.Appointment>> Update(int id,[FromBody] Models.Appointment appointment)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "Los datos de la cita no son válidos."
                });
            }

            //check if it exists
            var existingAppointment = await appointmentRepositories.FindById(id);
            if (existingAppointment == null)
            {
                return NotFound(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "Cita no encontrada."
                });
            }
            try
            {
                //update
                existingAppointment.DoctorId = appointment.DoctorId;
                existingAppointment.Reason = appointment.Reason;
                existingAppointment.Status = appointment.Status;
                existingAppointment.Date = appointment.Date;
                existingAppointment.Time = appointment.Time;

                await appointmentRepositories.Update(existingAppointment);

                 return Ok(new ResponseAPI<Models.Appointment>
                {
                    EsCorrecto = true,
                    Valor = existingAppointment,
                    Mensaje = "Cita actualizada correctamente."
                });
                
            }
            catch (Exception ex)
            {
                
                 return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"¡Ups! Ocurrió un error al actualizar la cita: {ex.Message}"
                });
            }

        }
    }
}