using MiApi.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using MiApi.Models;


namespace MiApi.Controllers.v1.Appointment
{
    [ApiController]
    [Route("api/v1/appointments")]
    [Tags("appointments")]
    public class AppointmentGetControllers(IAppointmentRepositories appointmentRepositories) : AppointmentControllers(appointmentRepositories)
    {

        [HttpGet("/appoitment/get")]
        [SwaggerOperation(
            Summary = "Get all appoitments ",
            Description ="This endpoint is for get all appoitments"
        )]
        [SwaggerResponse(200,"Get all appoitments")]
        [SwaggerResponse(500,"An error occurred")]
        [SwaggerResponse(404,"data not found")]
         public async Task<ActionResult<ResponseAPI<List<Models.Appointment>>>> GetAll()
        {
            try
            {
                var appoitmentsAll = await appointmentRepositories.GetAllAsync();

                if (appoitmentsAll.Any())
                {
                    return Ok(new ResponseAPI<List<Models.Appointment>>
                    {
                        EsCorrecto = true,
                        Valor = (List<Models.Appointment>)appoitmentsAll,
                        Mensaje = "Historiales obtenidos correctamente."
                    });
                }

                return NotFound(new ResponseAPI<List<Models.Appointment>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "No se encontraron historiales."
                });    
            }
            catch (Exception ex)
            {
                
                 return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<List<Models.Appointment>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error: {ex.Message}"
                });
            }
        }


        [HttpGet("/appoitment/find/{id}")]
        [SwaggerOperation(
            Summary = "Get for id appointment",
            Description ="This endpoint is for get for id appointment"
        )]
        [SwaggerResponse(200,"Get appointment by id is successful")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"appointment not found")]
        public async Task<ActionResult<Models.Appointment>> GetById(int id)
        {
            try
            {
                var fondAppoitment = await appointmentRepositories.FindById(id);

                if (fondAppoitment == null ) 
                {
                    return NotFound("el historial con el ID ingresado no existe.");
                }
                 return Ok(new ResponseAPI<Models.Appointment>
                    {
                        EsCorrecto = true,
                        Valor = fondAppoitment,
                        Mensaje = "Historial obtenido correctamente."
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<Models.Appointment>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error:  {ex.Message}"
                });
            }
        }



    }
}