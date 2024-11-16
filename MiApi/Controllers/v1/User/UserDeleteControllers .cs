using MiApi.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using  MiApi.Models;
using Microsoft.AspNetCore.Authorization;


namespace MiApi.Controllers.v1.User
{
    [ApiController]
    [Route("api/v1/users")]
    [Tags("users")]
    public class UserDeleteControllers(IUserRepositories userRepositories) : UserControllers(userRepositories)
    {

       
        [HttpDelete("/user/eliminar/{id}")]
        // [Authorize]
        [SwaggerOperation(
            Summary = "Delete user by id",
            Description ="This endpoint is for delete user by id"
        )]
        [SwaggerResponse(200,"user deleted successfully")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"user not found")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var userFind = await userRepositories.DeleteById(id);
                if (userFind == false)
                {

                    return Ok(new ResponseAPI<bool>
                    {
                        EsCorrecto = false,
                        Valor = false,
                        Mensaje = "No se encontró el usuario con ese id"
                    });
                    
                } 
                return Ok(new ResponseAPI<bool>
                    {
                        EsCorrecto = true,
                        Valor = true,
                        Mensaje = "Usuario eliminado correctamente."
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