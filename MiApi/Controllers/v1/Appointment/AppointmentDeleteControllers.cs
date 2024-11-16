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
    public class AppointmentDeleteControllers(IAppointmentRepositories appointmentRepositories) : AppointmentControllers(appointmentRepositories)
    {
        [HttpDelete("/appointments/delete/{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Delete appointments by id",
            Description ="This endpoint is for delete appointments by id"
        )]
        [SwaggerResponse(200,"appointments deleted successfully")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"appointments not found")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var appointmentFind = await appointmentRepositories.DeleteById(id);
                if (appointmentFind == false)
                {

                    return Ok(new ResponseAPI<bool>
                    {
                        EsCorrecto = false,
                        Valor = false,
                        Mensaje = "No se encontró la cita con ese id"
                    });
                    
                } 
                return Ok(new ResponseAPI<bool>
                    {
                        EsCorrecto = true,
                        Valor = true,
                        Mensaje = "Cita eliminada correctamente."
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