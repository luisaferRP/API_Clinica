using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MiApi.Repositories;
using MiApi.Models;

namespace MiApi.Controllers.v1.Doctor
{
    [ApiController]
    [Route("api/v1/doctors")]
    [Tags("doctors")]
    public class DoctorCreateControllers(IDoctorRepositories doctorRepositories) : DoctorControllers(doctorRepositories)
    {
        [HttpPost("/doctors/create")]
        [SwaggerOperation(
            Summary = "Create a new doctor",
            Description = "This endpoint is for create doctor."
        )]
        [SwaggerResponse(201,"users created successfully")]
        [SwaggerResponse(400,"An error occurred")]
        public async Task<ActionResult<Models.Doctor>> Create(Models.Doctor doctor)
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
                await doctorRepositories.Add(doctor);

                return Ok(new ResponseAPI<Models.Doctor>
                {
                    EsCorrecto = true,
                    Valor = doctor,
                    Mensaje = "Usuario creado correctamente"

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