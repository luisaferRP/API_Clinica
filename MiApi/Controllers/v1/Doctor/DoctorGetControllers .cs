using MiApi.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using MiApi.Models;
using MiApi.DTOS;

namespace MiApi.Controllers.v1.Doctor
{
    [ApiController]
    [Route("api/v1/doctors")]
    [Tags("doctors")]
    public class DoctorGetControllers(IDoctorRepositories doctorRepositories) : DoctorControllers(doctorRepositories)
    {

        [HttpGet("/doctors/get")]
        [SwaggerOperation(
            Summary = "Get all doctors ",
            Description ="This endpoint is for get all doctors"
        )]
        //respuestas de estado
        [SwaggerResponse(200,"Get all doctors")]
        [SwaggerResponse(500,"An error occurred")]
        [SwaggerResponse(404,"data not found")]
        public async Task<ActionResult<ResponseAPI<List<Models.Doctor>>>> GetAll()
        {
            try
            {
                var doctorsAll = await doctorRepositories.GetAll();

                if (doctorsAll.Any())
                {
                    return Ok(new ResponseAPI<List<Models.Doctor>>
                    {
                        EsCorrecto = true,
                        Valor = (List<Models.Doctor>)doctorsAll,
                        Mensaje = "Doctores obtenidos correctamente."
                    });
                }
                return NotFound(new ResponseAPI<List<Models.Doctor>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "No se encontraron doctores."
                });    
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<List<Models.User>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error al obtener el doctor: {ex.Message}"
                });
            }
        }


        [HttpGet("/doctors/search/{id}")]
        [SwaggerOperation(
            Summary = "Get doctor by id ",
            Description ="This endpoint is forbring doctor's data by user"
        )]
        [SwaggerResponse(200,"Get doctor by id is successful")]
        [SwaggerResponse(500,"An error occurred")]
        [SwaggerResponse(404,"doctor not found")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var foundDoctorById = await doctorRepositories.FindByIdUser(id);

                if (foundDoctorById == null ) 
                {
                    return NotFound("el doctor con el ID ingresado no existe.");
                }
                 return Ok(new ResponseAPI<object>
                    {
                        EsCorrecto = true,
                        Valor = foundDoctorById,
                        Mensaje = "Doctor obtenido correctamente."
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<Models.Doctor>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error al obtener al doctor: {ex.Message}"
                });
            }
        }



        [HttpGet("/doctor/specialty/{specialty}")]
        [SwaggerOperation(
            Summary = "Get doctor by the specialty",
            Description ="This endpoint is get doctor by specialty"
        )]
        [SwaggerResponse(200,"Get doctor by specialty is successful")]
        [SwaggerResponse(500,"An error occurred")]
        [SwaggerResponse(404,"doctor not found")]
        
        public async Task<ActionResult<Models.Doctor>> FindBySpecialty(string specialty)
        {
            try
            {
                var doctor = await doctorRepositories.FindSpecialty(specialty);

                if (doctor == null)
                {
                    return NotFound(new ResponseAPI<string>
                    {
                        EsCorrecto = false,
                        Valor = null,
                        Mensaje = "No se encontró un doctor con esa especialización."
                    });
                }

                return Ok(new ResponseAPI<Models.Doctor>
                {
                    EsCorrecto = true,
                    Valor = doctor,
                    Mensaje = "Doctor encontrado."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"¡Ups! Ocurrió un error: {ex.Message}"
                });
            }
        }





    }
}