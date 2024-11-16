using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MiApi.Repositories;
using MiApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiApi.Controllers.v1.User
{
    [ApiController]
    [Route("api/v1/users")]
    [Tags("users")]
    public class UserUpdateControllers(IUserRepositories userRepositories) : UserControllers(userRepositories)
    {
      
        [HttpPut("/users/update/{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Update a user", 
            Description = "This endpoint is for update data users")]
        [SwaggerResponse(200,"Product type updated successfully")]
        [SwaggerResponse(400,"An error occurred")]
        [SwaggerResponse(404,"Product type not found")]
        public async Task<ActionResult<Models.User>> Update(Models.User user)
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
               var userUpdate = await userRepositories.Update(user);
               if (userUpdate == null)
               {
                    return NotFound(new ResponseAPI<Models.User>{
                        EsCorrecto = false,
                        Valor = null,
                        Mensaje = "El usuario no se pudo actualizar,posibles datos invalidos"
                    });
               }
               return Ok(new ResponseAPI<Models.User>{
                        EsCorrecto = true,
                        Valor = userUpdate,
                        Mensaje = "Usuario actualizado con exito"
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