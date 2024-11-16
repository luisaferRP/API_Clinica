using MiApi.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using MiApi.Models;


namespace MiApi.Controllers.v1.User
{
    [ApiController]
    [Route("api/v1/users")]
    [Tags("users")]
    public class UserGetControllers(IUserRepositories userRepositories ): UserControllers(userRepositories)
    {

        [HttpGet("/users/get")]
        [SwaggerOperation(
            Summary = "Get all user ",
            Description ="This endpoint is for get all user"
        )]
        [SwaggerResponse(200,"Get all user")]
        [SwaggerResponse(500,"An error occurred")]
        [SwaggerResponse(404,"data not found")]
        public async Task<ActionResult<ResponseAPI<List<Models.User>>>> GetAll()
        {
            try
            {
                var userAll = await userRepositories.GetAll();

                if (userAll.Any())
                {
                    return Ok(new ResponseAPI<List<Models.User>>
                    {
                        EsCorrecto = true,
                        Valor = (List<Models.User>)userAll,
                        Mensaje = "Usuarios obtenidos correctamente."
                    });
                }

                return NotFound(new ResponseAPI<List<Models.User>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "No se encontraron usuarios."
                });    
            }
            catch (Exception ex)
            {
                
                 return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<List<Models.User>>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error al obtener usuarios: {ex.Message}"
                });
            }
        }


        [HttpGet("/user/find/{id}")]
        [SwaggerOperation(
            Summary = "Get for id user",
            Description ="This endpoint is for get for id user"
        )]
        [SwaggerResponse(200,"Get user by id is successful")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"User not found")]
        public async Task<ActionResult<Models.User>> GetById(int id)
        {
            try
            {
                var foundUserById = await userRepositories.FindById(id);

                if (foundUserById == null ) 
                {
                    return NotFound(new ResponseAPI<List<Models.User>>
                    {
                        EsCorrecto = false,
                        Valor = null,
                        Mensaje = "No se encontraron usuarios."
                    });   
                }
                 return Ok(new ResponseAPI<Models.User>
                    {
                        EsCorrecto = true,
                        Valor = foundUserById,
                        Mensaje = "Usuario obtenidos correctamente."
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<Models.User>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error al obtener al usuario: {ex.Message}"
                });
            }
        }


        //specific user
        [HttpGet("/user/patients")]
        [SwaggerOperation(
            Summary = "Get for patients",
            Description ="This endpoint is for get for patients"
        )]
        [SwaggerResponse(200,"Get patients is successful")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"patients not found")]
        public async Task<ActionResult<ResponseAPI<List<Models.User>>>> GetPatientS()
        {
            try
            {
                var patienteAll = await userRepositories.GetPatientAll();

                if (patienteAll == null)
                {
                    return NotFound(new ResponseAPI<List<Models.User>>
                    {
                        EsCorrecto = false,
                        Valor = null,
                        Mensaje = "No se encontraron pacientes."
                    });                     
                }

                return Ok(new ResponseAPI<List<Models.User>>
                {
                    EsCorrecto = true,
                    Valor =(List<Models.User>)patienteAll,
                    Mensaje = "pacientes obtenidos correctamente."
                });  
                
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<Models.User>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Error al obtener al usuario: {ex.Message}"
                });
            }

        }

    }
}