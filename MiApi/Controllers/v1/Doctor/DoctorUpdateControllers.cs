using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MiApi.Repositories;
using MiApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiApi.Controllers.v1.Doctor
{
    [ApiController]
    [Route("api/v1/doctors")]
    [Tags("doctors")]
    public class DoctorUpdateControllers(IDoctorRepositories doctorRepositories) : DoctorControllers(doctorRepositories)
    {
        [HttpPut("/doctors/update/{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Updating a doctor",
            Description = "Update a doctor's data"
        )]
        [SwaggerResponse(200,"Product type updated successfully")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"Product type not found")]
        public async Task<ActionResult<Models.Doctor>> Update(Models.Doctor doctor)
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
               var dcotorUpdate = await doctorRepositories.Update(doctor);
               if (dcotorUpdate == null)
               {
                    return NotFound(new ResponseAPI<Models.Doctor>{
                        EsCorrecto = false,
                        Valor = null,
                        Mensaje = "El doctor no se pudo actualizar,posibles datos invalidos"
                    });
               }
               return Ok(new ResponseAPI<Models.Doctor>{
                        EsCorrecto = true,
                        Valor = dcotorUpdate,
                        Mensaje = "Doctor actualizado con exito"
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